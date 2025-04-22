import { Routes } from '@angular/router';
import { BalanceComponent } from './balance.component';
import { CourseSaleSettingsComponent } from './courseSaleSettings/courseSaleSettings.component';
import { InstallmentSettingsComponent } from './installmentSettings/installmentSettings.component';

export const BalanceRoutes: Routes = [
  {
    path: '',
    component: BalanceComponent,
    children: [
      { path: '', redirectTo: 'course-sale-settings', pathMatch: 'full' },
      { path: 'course-sale-settings', data: { breadcrumb: 'Kur Satış Ayarları' }, component: CourseSaleSettingsComponent },
      { path: 'installment-setting', data: { breadcrumb: 'Eğitim Süresi / Taksit Sayısı' }, component: InstallmentSettingsComponent },
    ]
  }
];


