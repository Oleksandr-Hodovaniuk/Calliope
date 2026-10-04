import { Component, inject } from '@angular/core';
import { SHARED_IMPORTS } from '../../../shared/shared.imports';
import { KeycloakService } from '../../services/keycloak.service';


@Component({
  imports: [SHARED_IMPORTS],
  selector: 'app-header',
  styleUrl: './header.css',
  templateUrl: './header.html',
})
export class HeaderComponent {

  private readonly keycloakService = inject(KeycloakService);
  readonly isAuthenticated = this.keycloakService.isAuthenticated;

  async login(): Promise<void> {
    await this.keycloakService.login();
  }

  async logout(): Promise<void> {
    await this.keycloakService.logout();
  }

  async refresh(): Promise<void> {
    const accessToken =
      await this.keycloakService.refreshAccessToken();
  }
}
