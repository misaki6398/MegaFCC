import { Component, AfterViewInit } from '@angular/core';
import { Navbar } from './classes/navbar';
interface LanguageMapping {
  [key: string]: string;
}
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements AfterViewInit{
  switchNav = true;
  title = 'MegaEcmFrontEnd';

  displayLanguage = 'en';
  languageList = [
    { code: 'en', name: $localize`:@@English:English` },
    { code: 'tw', name: $localize`:@@Traditional-Chinese:Traditional-Chinese` },
  ];

  get i18nLang(): string {
    const mapping: LanguageMapping = {
      en: 'US',
      tw: 'ZH',
    };
    return mapping[this.displayLanguage];
  }

  openNav = true;

  navbars: Navbar[] = [{
    title: $localize`:@@TransactionFilter:Transaction Filter`,
    menuItems: [{
      title: $localize`:@@Dashboard:Dashboard`,
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
      title: $localize`:@@CaseManagement:Case Management`,
      url: 'TransationFilter/CaseManagement',
      active: false,
      icon: 'fa fa-search-dollar',
      subItems: []
    }, {
      title: $localize`:@@Report:Report`,
      url: '#',
      active: false,
      icon: 'fa fa-chart-bar',
      subItems: []
    }]
  }, {
    title: $localize`:@@CutomerScreening:Cutomer Screening`,
    menuItems: [{
      title: $localize`:@@Dashboard:Dashboard`,
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
      title: $localize`:@@CaseManagement:Case Management`,
      url: 'TransationFilter/CaseManagement',
      active: false,
      icon: 'fa fa-search-dollar',
      subItems: []
    }, {
      title: $localize`:@@Report:Report`,
      url: '#',
      active: false,
      icon: 'fa fa-chart-bar',
      subItems: []
    }]
  }];

  constructor() {
  }

  ngAfterViewInit(): void {
    this.displayLanguage = this.getCurrentLanguage();
  }

  buttonCloseNav(): void {
    this.openNav = !this.openNav;
  }

  clickMenu(navIndex: number, menuIndex: number): void {
    this.navbars[navIndex].menuItems[menuIndex].active = !this.navbars[navIndex].menuItems[menuIndex].active;
  }

  onSelectionChange($event): void {
    this.redirectTo($event);
  }

  private getCurrentLanguage(): string {
    const lang = ['en', 'tw'];
    const currentLang = lang.find(l => new RegExp(`/${l}/`).test(window.location.pathname));
    if (!currentLang) {
      return 'en';
    }
    return currentLang;
  }

  private redirectTo(redirectLang: string): void {
    const redirectPathName = window.location.pathname.replace(`/${this.displayLanguage}/`, `/${redirectLang}/`);
    window.location.pathname = redirectPathName;
  }
}
