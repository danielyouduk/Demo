import {RouterModule, Routes} from '@angular/router';
import {DashboardHomeComponent} from './pages/dashboard-home/dashboard-home.component';
import {NgModule} from '@angular/core';

const routes: Routes = [
  {
    path: '',
    component: DashboardHomeComponent,
    title: 'Dashboard'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutesModule { }
