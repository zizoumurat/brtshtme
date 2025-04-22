import { Component } from '@angular/core';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { Router } from '@angular/router';

@Component({
  selector: 'app-bonus-settings',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './bonusSettings.component.html'
})
export class BonusSettingsComponent {

  constructor(private router: Router) { }

  tabs = [
    { route: 'sales-representative', label: 'crm.settings.bonusSettings.salesRepresentative', icon: 'pi pi-check-square' },
    { route: 'data-provider', label: 'crm.settings.bonusSettings.dataProvider', icon: 'pi pi-check-square' },
  ];

  activeTab: string = '';

  ngOnInit(): void {
    this.setActiveTab();
  }

  setActiveTab(): void {
    const currentRoute = this.router.url;
    this.activeTab = this.tabs.find(tab => currentRoute.endsWith(tab.route))?.route || '';
  }

  get currentRoute(): string {
    return location.pathname;
  }

  navigate(path: string) {
    this.router.navigate(['/crm/settings/bonus-settings/', path]);
    this.activeTab = path;
  }
}

