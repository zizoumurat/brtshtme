import { Routes } from '@angular/router';
import { AppLayout } from '@/presentation/layout/components/app.layout';
import { LandingLayout } from '@/presentation/layout/components/app.landinglayout';
import { AuthGuard } from '@/core/guards/auth.guard';


export const appRoutes: Routes = [
    {
        path: '',
        component: AppLayout,
        canActivate: [AuthGuard],
        children: [
            {
                path: '',
                loadChildren: () =>
                    import('@/presentation/admin/home/home.routes').then(
                        (m) => m.HomeRoutes
                    ),
            },
            {
                path: 'crm',
                loadChildren: () =>
                    import('@/presentation/crm/crm.routes').then(
                        (m) => m.CrmRoutes
                    ),
            },
            {
                path: 'srm',
                loadChildren: () =>
                    import('@/presentation/srm/srm.routes').then(
                        (m) => m.SrmRoutes
                    ),
            },
            {
                path: 'settings',
                loadChildren: () =>
                    import('@/presentation/settings/settings.routes').then(
                        (m) => m.SettingsRoutes
                    ),
            },
            {
                path: 'calendar',
                loadChildren: () =>
                    import('@/presentation/admin/calendar/calendar.routes').then(
                        (m) => m.CalendarRoutes
                    ),
            }
        ],
    },
    {
        path: 'auth',
        component: LandingLayout,
        loadChildren: () => import('@/presentation/auth/auth.routes').then(m => m.AuthRoutes)
    },
];
