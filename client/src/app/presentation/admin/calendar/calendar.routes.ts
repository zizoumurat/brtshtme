import { Routes } from '@angular/router';
import { CalendarComponent } from './calendar.component';

export const CalendarRoutes: Routes = [
  { path: '', data: { breadcrumb: 'Takvim' }, component: CalendarComponent },
];
