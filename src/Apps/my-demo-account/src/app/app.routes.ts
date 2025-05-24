import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./features/home/home-routes')
      .then(m => m.homeRoutes)
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./features/dashboard/dashboard.module')
      .then(m => m.DashboardModule)
  },
  {
    path: 'account',
    loadChildren: () => import('./features/account/account.module')
      .then(m => m.AccountModule)
  },
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.module')
      .then(m => m.AuthModule)
  },
  {
    path: 'vehicles',
    loadChildren: () => import('./features/vehicles/vehicles.module')
      .then(m => m.VehiclesModule)
  }
];
