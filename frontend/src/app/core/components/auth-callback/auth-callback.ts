import { Component, inject } from '@angular/core';
import { KeycloakService } from '../../services/keycloak';
import { Router } from '@angular/router';

@Component({
  imports: [],
  selector: 'app-auth-callback',
  styleUrl: './auth-callback.css',
  templateUrl: './auth-callback.html',
})
export class AuthCallbackComponent  {
  private readonly keycloakService = inject(KeycloakService);
  private readonly router = inject(Router);
  
  async ngOnInit(): Promise<void> {
    await this.keycloakService.handleCallback();

    await this.router.navigate(['/']);
  }
}