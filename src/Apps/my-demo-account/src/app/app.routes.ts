import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./features/home/home-routes')
      .then(m => m.homeRoutes)
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./features/dashboard/dashboard-routes')
      .then(m => m.dashboardRoutes)
  },
  {
    path: 'account',
    loadChildren: () => import('./features/account/account-routes')
      .then(m => m.accountRoutes)
  },
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth-routes')
      .then(m => m.authRoutes)
  }
];
