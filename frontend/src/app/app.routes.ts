import { Routes } from '@angular/router';
import { MainLayoutComponent } from './core/layouts/main-layout/main-layout';
import { AuthCallbackComponent } from './core/components/auth-callback/auth-callback';
import { TracksComponent } from './features/tracks/components/tracks.component/tracks.component';

export const routes: Routes = [
  {
    path: 'auth/callback',
    component: AuthCallbackComponent
  },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'tracks',
        component: TracksComponent
      }
    ]
  }
];