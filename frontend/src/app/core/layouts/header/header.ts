import { Component, inject } from '@angular/core';
import { SHARED_IMPORTS } from '../../../shared/shared.imports';
import { HttpClient } from '@angular/common/http';


@Component({
  imports: [SHARED_IMPORTS],
  selector: 'app-header',
  styleUrl: './header.css',
  templateUrl: './header.html',
})
export class HeaderComponent {
  private readonly http = inject(HttpClient);

  async ngOnInit(): Promise<void> {
  const params = new URLSearchParams(window.location.search);

  const code = params.get('code');
  const codeVerifier = sessionStorage.getItem('pkce_code_verifier');

  console.log('Authorization code:', code);
  console.log('Code verifier:', codeVerifier);

  if (code && codeVerifier) {
    await this.exchangeCodeForToken(code, codeVerifier);
  }
}

  private generateCodeVerifier(): string {
  const array = new Uint8Array(32);
  crypto.getRandomValues(array);

  return btoa(String.fromCharCode(...array))
    .replace(/\+/g, '-')
    .replace(/\//g, '_')
    .replace(/=/g, '');
}

private async generateCodeChallenge(codeVerifier: string): Promise<string> {
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

  const params = new URLSearchParams({
    client_id: 'Calliope-Frontend',
    redirect_uri: 'http://localhost:4200/',
    response_type: 'code',
    scope: 'openid',
    code_challenge: codeChallenge,
    code_challenge_method: 'S256'
  });

  const url =
    `http://localhost:8080/realms/Calliope/protocol/openid-connect/auth?${params.toString()}`;

  console.log('KEYCLOAK URL:', url);

  window.location.href = url;
}
private async exchangeCodeForToken(
  code: string,
  codeVerifier: string
): Promise<void> {

  const body = new URLSearchParams({
    grant_type: 'authorization_code',
    client_id: 'Calliope-Frontend',
    code: code,
    redirect_uri: 'http://localhost:4200/',
    code_verifier: codeVerifier
  });

  const response = await fetch(
    'http://localhost:8080/realms/Calliope/protocol/openid-connect/token',
    {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      body: body.toString()
    }
  );

  const data = await response.json();

  console.log('Token response:', data);
}
}
