import { Routes } from '@angular/router';
import { HomeComponent } from './home.component';

export const HomeRoutes: Routes = [
  { path: '',  data: { breadcrumb: 'Anasayfa' }, component: HomeComponent },
];
