import { Routes } from '@angular/router';
import { ClassesComponent } from './classes.component';
import { ClassListComponent } from './list/list.component';

export const ClassesRoutes: Routes = [
    {
      path: '',
      component: ClassesComponent,
      children: [
        { path: '', redirectTo: 'list', pathMatch: 'full' },
        { path: 'list', data: { breadcrumb: 'Sınıf Listesi' }, component: ClassListComponent },
        { path: 'reports', data: { breadcrumb: 'Raporlar' }, component: ClassListComponent },
      ]
    }
  ];
  

