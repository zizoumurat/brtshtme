import { Routes } from '@angular/router';
import { PricingParametersComponent } from './pricingParameters.component';
import { ReferenceComponent } from './reference/reference.component';


export const PricingParametersRoutes: Routes = [
  {
    path: '',
    component: PricingParametersComponent,
    children: [
      { path: '', redirectTo: 'reference', pathMatch: 'full' },
      {
        path: 'reference',
        data: { breadcrumb: 'Referans Değerler' },
        loadChildren: () =>
          import('@/presentation/crm/settings/pricingParameters/reference/reference.routes').then(
            (m) => m.ReferenceRoutes
          ),
      },
      {
        path: 'balance',
        data: { breadcrumb: 'Denge Ayarları' },
        loadChildren: () =>
          import('@/presentation/crm/settings/pricingParameters/balance/balance.routes').then(
            (m) => m.BalanceRoutes
          ),
      },
    ]
  }
];


