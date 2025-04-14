import { Routes } from '@angular/router';
import { SettingsComponent } from './settings.component';
import { BranchesComponent } from './branches/branches.component';
import { PersonelsComponent } from './personel/personel.component';
import { RegionComponent } from './regions/regions.component';
import { ScheduleSettingsComponent } from './scheduleSettings/scheduleSettings.component';

export const SettingsRoutes: Routes = [
    {
      path: '',
      component: SettingsComponent,
      children: [
        { path: '', redirectTo: 'branches', pathMatch: 'full' },
        { path: 'branches', component: BranchesComponent },
        {path: 'personnel', component: PersonelsComponent},
        {path: 'regions', component: RegionComponent},
        {path: 'schedule-settings', component: ScheduleSettingsComponent},
        {
          path: 'bonus-settings',
          loadChildren: () =>
            import('@/presentation/crm/settings/bonusSettings/bonusSettings.routes').then(
              (m) => m.BonusSettingsRoutes
            ),
        },
      ]
    }
  ];
  

