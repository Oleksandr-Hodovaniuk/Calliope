import { HttpClient } from '@angular/common/http';
import { inject, Service, signal  } from '@angular/core';
import { firstValueFrom } from 'rxjs';

@Service()
export class KeycloakService {

  private readonly http = inject(HttpClient);
  isAuthenticated = signal(false);

  constructor() {
    this.isAuthenticated.set(
      this.isAccessTokenValid()
    );
  }
  
  private generateCodeVerifier(): string {

    const array = new Uint8Array(32);
    crypto.getRandomValues(array);

    return btoa(String.fromCharCode(...array))
      .replace(/\+/g, '-')
      .replace(/\//g, '_')
      .replace(/=/g, '');
  }

  async generateCodeChallenge(codeVerifier: string): Promise<string> {

    const data = new TextEncoder().encode(codeVerifier);
    const digest = await crypto.subtle.digest('SHA-256', data);

    return btoa(String.fromCharCode(...new Uint8Array(digest)))
      .replace(/\+/g, '-')
      .replace(/\//g, '_')
      .replace(/=/g, '');
  }

  async login(): Promise<void> {

    const codeVerifier = this.generateCodeVerifier();
    const codeChallenge = await this.generateCodeChallenge(codeVerifier);

    sessionStorage.setItem('pkce_code_verifier', codeVerifier);

    const requestParams = new URLSearchParams({
      client_id: 'Calliope-Frontend',
      redirect_uri: 'http://localhost:4200/auth/callback',
      response_type: 'code',
      scope: 'openid',
      code_challenge: codeChallenge,
      code_challenge_method: 'S256'
    });

    const authUrl = `http://localhost:8080/realms/Calliope/protocol/openid-connect/auth?${requestParams.toString()}`;
    window.location.href = authUrl;
  }

  async handleCallback(): Promise<void> {

    const params = new URLSearchParams(
      window.location.search
    );

    const code = params.get('code');

    if (!code) {
      return;
    }

    await this.exchangeCodeForToken(code);
  }

  async exchangeCodeForToken(code: string): Promise<void> {
    const codeVerifier =
      sessionStorage.getItem('pkce_code_verifier');

    if (!codeVerifier) {
      return;
    }

    const body = new URLSearchParams({
      grant_type: 'authorization_code',
      client_id: 'Calliope-Frontend',
      code: code,
      redirect_uri: 'http://localhost:4200/auth/callback',
      code_verifier: codeVerifier
    });

    const response = await firstValueFrom(
      this.http.post<TokenResponse>(
        'http://localhost:8080/realms/Calliope/protocol/openid-connect/token',
        body.toString(),
        {
          headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
          }
        }
      )
    );

    localStorage.setItem(
      'access_token',
      response.access_token
    );

    localStorage.setItem(
      'refresh_token',
      response.refresh_token
    );

    localStorage.setItem(
      'id_token',
      response.id_token
    );

    this.isAuthenticated.set(true);
  }

  getAccessToken(): string | null {

    return localStorage.getItem('access_token');
  }

  getRefreshToken(): string | null {

    return localStorage.getItem('refresh_token');
  }

  isLoggedIn(): boolean {
    return !!this.getAccessToken();
  }

  logout(): void {

    const idToken = localStorage.getItem('id_token');

    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('id_token');

    this.isAuthenticated.set(false);

    const params = new URLSearchParams({
      client_id: 'Calliope-Frontend',
      post_logout_redirect_uri: 'http://localhost:4200/',
       id_token_hint: idToken ?? ''
    });

    const logoutUrl =
      `http://localhost:8080/realms/Calliope/protocol/openid-connect/logout?${params.toString()}`;

    window.location.href = logoutUrl;
  }

  async refreshAccessToken(): Promise<string | null> {
    const refreshToken = this.getRefreshToken();

    if (!refreshToken) {
      return null;
    }

    const body = new URLSearchParams({
      grant_type: 'refresh_token',
      client_id: 'Calliope-Frontend',
      refresh_token: refreshToken
    });

    try {
      const response = await firstValueFrom(
        this.http.post<TokenResponse>(
          'http://localhost:8080/realms/Calliope/protocol/openid-connect/token',
          body.toString(),
          {
            headers: {
              'Content-Type': 'application/x-www-form-urlencoded'
            }
          }
        )
      );

      localStorage.setItem(
        'access_token',
        response.access_token
      );

      if (response.refresh_token) {
        localStorage.setItem(
          'refresh_token',
          response.refresh_token
        );
      }

      return response.access_token;
    } catch {
      return null;
    }
  }

  isAccessTokenValid(): boolean {
    const token = this.getAccessToken();

    if (!token) {
      return false;
    }

    try {
      const payload = JSON.parse(
        atob(token.split('.')[1])
      );

      const currentTime = Math.floor(Date.now() / 1000);

      return payload.exp > currentTime;
    } catch {
      return false;
    }
  }
}

interface TokenResponse {
  access_token: string;
  expires_in: number;
  refresh_expires_in: number;
  refresh_token: string;
  token_type: string;
  id_token: string;
  scope: string;
}