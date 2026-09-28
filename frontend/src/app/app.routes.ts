import { Routes } from '@angular/router';
import { MainLayoutComponent } from './core/layouts/main-layout/main-layout';
import { AuthCallbackComponent } from './core/components/auth-callback/auth-callback';

export const routes: Routes = [
  {
    path: 'auth/callback',
    component: AuthCallbackComponent
  },
  {
    path: '',
    component: MainLayoutComponent
  }
];