import { Routes } from '@angular/router';
import { SettingsComponent } from './settings.component';
import { BranchesComponent } from './branches/branches.component';
import { EmployeesComponent } from './employees/employees.component';
import { RegionComponent } from './regions/regions.component';
import { ScheduleSettingsComponent } from './scheduleSettings/scheduleSettings.component';
import { UsersComponent } from './users/users.component';

export const SettingsRoutes: Routes = [
  {
    path: '',
    component: SettingsComponent,
    data: { breadcrumb: 'Ayarlar' },
    children: [
      { path: '', redirectTo: 'branches', pathMatch: 'full' },
      { path: 'branches', data: { breadcrumb: 'Şubeler' }, component: BranchesComponent },
      { path: 'employees', data: { breadcrumb: 'Personeller' }, component: EmployeesComponent },
      { path: 'users', data: { breadcrumb: 'Kullanıcılar' }, component: UsersComponent },
      { path: 'regions', data: { breadcrumb: 'Bölge Tanımları' }, component: RegionComponent },
      { path: 'schedule-settings', data: { breadcrumb: 'Ders Programı Tanımları' }, component: ScheduleSettingsComponent },
      {
        path: 'bonus-settings',
        data: { breadcrumb: 'Primlendirme Ayarları' },
        loadChildren: () =>
          import('@/presentation/crm/settings/bonusSettings/bonusSettings.routes').then(
            (m) => m.BonusSettingsRoutes
          ),
      },
      {
        path: 'pricing-parameters',
        data: { breadcrumb: 'Fiyatlandırma Parametreleri' },
        loadChildren: () =>
          import('@/presentation/crm/settings/pricingParameters/pricingParameters.routes').then(
            (m) => m.PricingParametersRoutes
          ),
      },
    ]
  }
];


