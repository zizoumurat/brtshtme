import { Routes } from '@angular/router';
import { SrmComponent } from './srm.component';
import { StudentsComponent } from './students/students.component';
import { ClassesComponent } from './classes/classes.component';

export const SrmRoutes: Routes = [
  {
    path: '',
    data: { breadcrumb: 'SRM' },
    component: SrmComponent,
    children: [
      { path: '', redirectTo: 'students', pathMatch: 'full' },
      { path: 'students', component: StudentsComponent, data: { breadcrumb: 'Öğrenci İşlemleri' } },
      {
        path: 'classes',
        data: { breadcrumb: 'Sınıf İşlemleri' },
        loadChildren: () =>
          import('@/presentation/srm/classes/classes.routes').then(
            (m) => m.ClassesRoutes
          ),
      },
    ]
  }
];


