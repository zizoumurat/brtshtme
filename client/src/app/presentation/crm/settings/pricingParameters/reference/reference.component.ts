import { Component } from '@angular/core';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reference',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './reference.component.html'
})
export class ReferenceComponent {

  constructor(private router: Router) { }

  tabs = [
    { route: 'basic-parameters', label: 'crm.settings.pricingParameters.reference.basicParameters', icon: 'pi pi-check-square' },
    { route: 'campaigns', label: 'crm.settings.pricingParameters.reference.campaigns', icon: 'pi pi-check-square' },
    { route: 'discounts', label: 'crm.settings.pricingParameters.reference.justifiedDiscounts', icon: 'pi pi-check-square' },
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
    this.router.navigate(['/crm/settings/pricing-parameters/reference', path]);
    this.activeTab = path;
  }
}

