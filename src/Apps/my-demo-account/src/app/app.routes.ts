import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'dashboard',
    loadChildren: () => import('./features/dashboard/dashboard-routes')
      .then(m => m.dashboardRoutes)
  }
];
