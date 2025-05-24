import { Routes } from '@angular/router';
import {AccountSettingsComponent} from './pages/account-settings/account-settings.component';

export const accountRoutes: Routes = [
  {
    path: '',
    component: AccountSettingsComponent,
    title: 'Account'
  },
  {
    path: 'settings',
    component: AccountSettingsComponent,
    title: 'Account Settings'
  }
]
