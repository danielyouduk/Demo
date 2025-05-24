import {RouterModule, Routes} from '@angular/router';
import {AccountSettingsComponent} from './pages/account-settings/account-settings.component';
import {AccountHomeComponent} from './pages/account-home/account-home.component';
import {NgModule} from '@angular/core';

const accountRoutingModule: Routes = [
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

@NgModule({
  imports: [RouterModule.forChild(accountRoutingModule)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
