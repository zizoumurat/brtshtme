import { Routes } from '@angular/router';
import { ReferenceComponent } from './reference.component';
import { BasicParametersComponent } from './basicParameters/basicParameters.component';
import { CampaignsComponent } from './campaigns/campaigns.component';
import { DiscountsComponent } from './discounts/discounts.component';

export const ReferenceRoutes: Routes = [
  {
    path: '',
    component: ReferenceComponent,
    children: [
      { path: '', redirectTo: 'basic-parameters', pathMatch: 'full' },
      { path: 'basic-parameters', data: { breadcrumb: 'Temel Parametreler' }, component: BasicParametersComponent },
      { path: 'campaigns', data: { breadcrumb: 'Kampanyalar' }, component: CampaignsComponent },
      { path: 'discounts', data: { breadcrumb: 'Gerekçeli İndirimler' }, component: DiscountsComponent },
    ]
  }
];


