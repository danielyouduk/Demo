import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {VehicleListComponent} from './pages/vehicle-list/vehicle-list.component';

const routes: Routes = [
  {
    path: '',
    component: VehicleListComponent,
    title: 'Vehicles'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VehiclesRoutingModule { }
