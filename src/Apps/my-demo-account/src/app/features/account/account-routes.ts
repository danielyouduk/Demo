import { Routes } from '@angular/router';
import {AccountSettingsComponent} from './pages/account-settings/account-settings.component';
import {AccountHomeComponent} from './pages/account-home/account-home.component';

export const accountRoutes: Routes = [
  {
    path: '',
    component: AccountHomeComponent,
    title: 'Account'
  },
  {
    path: 'settings',
    component: AccountSettingsComponent,
    title: 'Account Settings'
  }
]
