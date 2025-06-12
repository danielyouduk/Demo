import { Component } from '@angular/core';
import {SidebarComponent} from '../../../../shared/components/sidebar/sidebar.component';

@Component({
  selector: 'app-dashboard-pages',
  imports: [
    SidebarComponent
  ],
  templateUrl: './dashboard-home.component.html',
  styleUrl: './dashboard-home.component.css'
})
export class DashboardHomeComponent {

}
