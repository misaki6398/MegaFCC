import { Component } from '@angular/core';
import { Navbar } from './classes/navbar';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  switchNav = true;
  title = 'MegaEcmFrontEnd';


  openNav = true;

  navbars: Navbar[] = [{
    title: 'Transaction Filter',
    menuItems: [{
      title: 'Dashboard',
      url: '#',
      active: false,
      icon: 'fa fa-tachometer-alt',
      subItems: [{
        title: 'Dashboard 1',
        url: '#',
        active: false
      }, {
        title: 'Dashboard 2',
        url: '#',
        active: false
      }]
    }, {
      title: 'Case Management',
      url: 'TransationFilter/CaseManagement',
      active: false,
      icon: 'fa fa-search-dollar',
      subItems: []
    }, {
      title: 'Report',
      url: '#',
      active: false,
      icon: 'fa fa-chart-bar',
      subItems: []
    }]
  }, {
    title: 'Cutomer Screening',
    menuItems: [{
      title: 'Dashboard',
      url: '#',
      active: false,
      icon: 'fa fa-tachometer-alt',
      subItems: [{
        title: 'Dashboard 1',
        url: '#',
        active: false
      }, {
        title: 'Dashboard 2',
        url: '#',
        active: false
      }]
    }, {
      title: 'Case Management',
      url: '#',
      active: false,
      icon: 'fa fa-search-dollar',
      subItems: []
    }
      , {
      title: 'Report',
      url: '#',
      active: false,
      icon: 'fa fa-chart-bar',
      subItems: []
    }]
  }];

  constructor() { }


  buttonCloseNav(): void {
    this.openNav = !this.openNav;
  }

  clickMenu(navIndex: number, menuIndex: number): void {
    this.navbars[navIndex].menuItems[menuIndex].active = !this.navbars[navIndex].menuItems[menuIndex].active;
  }
}
