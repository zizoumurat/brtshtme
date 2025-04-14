import { Routes } from '@angular/router';
import { CrmComponent } from './crm.component';
import { TasksComponent } from './tasks/tasks.component';


export const CrmRoutes: Routes = [
  {
    path: '',
    component: CrmComponent,
    children: [
      { path: '', redirectTo: 'tasks', pathMatch: 'full' },
      { path: 'tasks', component: TasksComponent },
      {
        path: 'settings',
        loadChildren: () =>
          import('@/presentation/crm/settings/settings.routes').then(
            (m) => m.SettingsRoutes
          ),
      },
    ]
  }
];


