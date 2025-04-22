import { Component } from '@angular/core';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pricing-parameters',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './pricingParameters.component.html'
})
export class PricingParametersComponent {

  constructor(private router: Router) { }

  tabs = [
    { route: 'reference', label: 'crm.settings.pricingParameters.reference.label', icon: 'pi pi-check-square' },
    { route: 'balance', label: 'crm.settings.pricingParameters.balance.label', icon: 'pi pi-check-square' },
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
    this.router.navigate(['/crm/settings/bonus-settings/', path]);
    this.activeTab = path;
  }
}

