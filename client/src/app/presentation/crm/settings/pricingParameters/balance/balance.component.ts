import { Component } from '@angular/core';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { Router } from '@angular/router';

@Component({
  selector: 'app-balance',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './balance.component.html'
})
export class BalanceComponent {

  constructor(private router: Router) { }

  tabs = [
    { route: 'course-sale-settings', label: 'crm.settings.pricingParameters.balance.courseSaleSettings', icon: 'pi pi-check-square' },
    { route: 'installment-setting', label: 'crm.settings.pricingParameters.balance.installmentSetting', icon: 'pi pi-check-square' },
  ];

  activeTab: string = '';

  ngOnInit(): void {
    this.setActiveTab();
  }

  setActiveTab(): void {
    const currentRoute = this.router.url;
    this.activeTab = this.tabs.find(tab => currentRoute.includes(tab.route))?.route || '';
}

  get currentRoute(): string {
    return location.pathname;
  }

  navigate(path: string) {
    this.router.navigate(['/crm/settings/pricing-parameters/balance', path]);
    this.activeTab = path;
  }
}

