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
    { label: 'crm.settings.branches', routerLink: 'branches'},
    { label: 'crm.settings.employees', routerLink: 'employees' },
    { label: 'crm.settings.users', routerLink: 'users' },
    { label: 'crm.settings.regionDefinitions', routerLink: 'regions' },
    { label: 'crm.settings.classrooms', routerLink: 'classrooms' },
    { label: 'crm.settings.levels', routerLink: 'levels' },
    { label: 'crm.settings.bonusSettings.bonusSettings', routerLink: 'bonus-settings' },
    { label: 'crm.settings.classScheduleDefinitions', routerLink: 'schedule-settings' },
    { label: 'crm.settings.pricingParameters.label', routerLink: 'pricing-parameters' }
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

