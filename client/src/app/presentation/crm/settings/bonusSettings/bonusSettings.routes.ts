import { Routes } from '@angular/router';
import { BonusSettingsComponent } from './bonusSettings.component';
import { SalesRepresentativeComponent } from './salesRepresentative/salesRepresentative.component';
import { DataProviderComponent } from './dataProvider/dataProvider.component';


export const BonusSettingsRoutes: Routes = [
    {
      path: '',
      component: BonusSettingsComponent,
      children: [
        { path: '', redirectTo: 'sales-representative', pathMatch: 'full' },
        { path: 'sales-representative', component: SalesRepresentativeComponent },
        { path: 'data-provider', component: DataProviderComponent },
      ]
    }
  ];
  

