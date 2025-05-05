import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-crm-settings',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './settings.component.html'
})
export class SettingsComponent {

  constructor(private translate: TranslateService, private router: Router) { }
  items: MenuItem[] | undefined;

  menuItems = [
    { label: 'crm.settings.branches', routerLink: '/crm/settings/branches'},
    { label: 'crm.settings.employees', routerLink: '/crm/settings/employees' },
    { label: 'crm.settings.regionDefinitions', routerLink: '/crm/settings/regions' },
    { label: 'crm.settings.users', routerLink: '/crm/settings/users' },
    { label: 'crm.settings.bonusSettings.bonusSettings', routerLink: '/crm/settings/bonus-settings' },
    { label: 'crm.settings.classScheduleDefinitions', routerLink: '/crm/settings/schedule-settings' },
    { label: 'crm.settings.pricingParameters.label', routerLink: '/crm/settings/pricing-parameters' }
  ];

  ngOnInit(): void {
    this.translateMenuLabels();
  }

  translateMenuLabels() {
    this.menuItems.forEach(item => {
      this.translate.get(item.label).subscribe((translatedLabel: string) => {
        item.label = translatedLabel; 
      });
    });
  }
}

