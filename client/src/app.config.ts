import { HttpClient, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter, withEnabledBlockingInitialNavigation, withInMemoryScrolling } from '@angular/router';
import Aura from '@primeng/themes/aura';
import { providePrimeNG } from 'primeng/config';
import { appRoutes } from './app.routes';
import { definePreset } from '@primeng/themes';
import { authInterceptor } from '@/core/interceptors/auth.interceptor';
import { deleteInterceptor } from '@/core/interceptors/delete.interceptor';
import { httpMessageInterceptor } from '@/core/interceptors/httpMessageInterceptor.interceptor';
import { ConfirmationService, MessageService } from 'primeng/api';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { dateConversionInterceptor } from '@/core/interceptors/dateConversionInterceptor';

import { LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localeTr from '@angular/common/locales/tr';

import { AUTH_SERVICE } from '@/core/services/admin/auth-token';
import { AuthService } from '@/infrastructure/api/admin/auth-service';
import { CALENDAR_SERVICE } from '@/core/services/admin/calendar-service';
import { CalendarService } from '@/infrastructure/api/admin/calendar-service';
import { BRANCH_SERVICE } from '@/core/services/crm/branch-service';
import { BranchService } from '@/infrastructure/api/crm/branch-service';
import { RegionService } from '@/infrastructure/api/crm/region-service';
import { REGION_SERVICE } from '@/core/services/crm/region-service';
import { INCENTIVESETTING_SERVICE } from '@/core/services/crm/incentiveSetting-service';
import { IncentiveSettingService } from '@/infrastructure/api/crm/incentiveSetting-service';
import { LESSONSCHEDULEDEFINITION_SERVICE } from '@/core/services/crm/lessonScheduleDefinition-service';
import { LessonScheduleDefinitionService } from '@/infrastructure/api/crm/lessonScheduleDefinition-service';
import { BRANCHPRICINGSETTINGS_SERVICE } from '@/core/services/crm/branchPricingSettings-service';
import { BranchPricingSettingsService } from '@/infrastructure/api/crm/branchPricingSettings-service';
import { CAMPAIGN_SERVICE } from '@/core/services/crm/campaign-service';
import { CampaignService } from '@/infrastructure/api/crm/campaign-service';
import { DISCOUNT_SERVICE } from '@/core/services/crm/discount-service';
import { DiscountService } from '@/infrastructure/api/crm/discount-service';
import { COURSESALESETTING_SERVICE } from '@/core/services/crm/courseSaleSetting-service';
import { CourseSaleSettingService } from '@/infrastructure/api/crm/courseSaleSetting-service';
import { INSTALLMENTSETTING_SERVICE } from '@/core/services/crm/installmentSetting-service';
import { InstallmentSettingService } from '@/infrastructure/api/crm/installmentSetting-service';
import { EMPLOYEE_SERVICE } from '@/core/services/crm/employee-service';
import { EmployeeService } from '@/infrastructure/api/crm/employee-service';
import { AppUserService } from '@/infrastructure/api/crm/appuser-service';
import { APPUSER_SERVICE } from '@/core/services/crm/appuser.service';
import { CRMRECORD_SERVICE } from '@/core/services/crm/crmrecord-service';
import { CrmRecordService } from '@/infrastructure/api/crm/crmrecord-service';
import { CRMRECORDACTION_SERVICE } from '@/core/services/crm/crmrecordaction-service';
import { CrmRecordActionService } from '@/infrastructure/api/crm/crmrecordaction-service';

export function HttpLoaderFactory(httpClient: HttpClient) {
    return new TranslateHttpLoader(httpClient);
}

const MyPreset = definePreset(Aura, {
    semantic: {
        primary: {
            50: '{blue.50}',
            100: '{blue.100}',
            200: '{blue.200}',
            300: '{blue.300}',
            400: '{blue.400}',
            500: '{blue.500}',
            600: '{blue.600}',
            700: '{blue.700}',
            800: '{blue.800}',
            900: '{blue.900}',
            950: '{blue.950}'
        },
        overlay: {
            modal: {
                borderRadius: '1.5rem'
            },
            popover: {
                borderRadius: '10px'
            }
        },
        colorScheme: {
            light: {
                surface: {
                    0: 'color-mix(in srgb, {primary.950}, white 100%)',
                    50: 'color-mix(in srgb, {primary.950}, white 95%)',
                    100: 'color-mix(in srgb, {primary.950}, white 90%)',
                    200: 'color-mix(in srgb, {primary.950}, white 80%)',
                    300: 'color-mix(in srgb, {primary.950}, white 70%)',
                    400: 'color-mix(in srgb, {primary.950}, white 60%)',
                    500: 'color-mix(in srgb, {primary.950}, white 50%)',
                    600: 'color-mix(in srgb, {primary.950}, white 40%)',
                    700: 'color-mix(in srgb, {primary.950}, white 30%)',
                    800: 'color-mix(in srgb, {primary.950}, white 20%)',
                    900: 'color-mix(in srgb, {primary.950}, white 10%)',
                    950: 'color-mix(in srgb, {primary.950}, white 5%)'
                }
            },
            dark: {
                surface: {
                    0: 'color-mix(in srgb, var(--surface-ground), white 100%)',
                    50: 'color-mix(in srgb, var(--surface-ground), white 95%)',
                    100: 'color-mix(in srgb, var(--surface-ground), white 90%)',
                    200: 'color-mix(in srgb, var(--surface-ground), white 80%)',
                    300: 'color-mix(in srgb, var(--surface-ground), white 70%)',
                    400: 'color-mix(in srgb, var(--surface-ground), white 60%)',
                    500: 'color-mix(in srgb, var(--surface-ground), white 50%)',
                    600: 'color-mix(in srgb, var(--surface-ground), white 40%)',
                    700: 'color-mix(in srgb, var(--surface-ground), white 30%)',
                    800: 'color-mix(in srgb, var(--surface-ground), white 20%)',
                    900: 'color-mix(in srgb, var(--surface-ground), white 10%)',
                    950: 'color-mix(in srgb, var(--surface-ground), white 5%)'
                }
            }
        }
    }
});

export const appConfig: ApplicationConfig = {
    providers: [
        provideRouter(
            appRoutes,
            withInMemoryScrolling({
                anchorScrolling: 'enabled',
                scrollPositionRestoration: 'top'
            }),
            withEnabledBlockingInitialNavigation()
        ),

        { provide: LOCALE_ID, useValue: 'tr-TR' },

        provideHttpClient(withFetch()),
        provideAnimationsAsync(),
        provideHttpClient(withInterceptors([authInterceptor, deleteInterceptor, httpMessageInterceptor, dateConversionInterceptor])),

        providePrimeNG({ theme: { preset: MyPreset, options: { darkModeSelector: '.app-dark' } } }),

        importProvidersFrom(
            TranslateModule.forRoot({
                defaultLanguage: 'tr',
                loader: {
                    provide: TranslateLoader,
                    useFactory: HttpLoaderFactory,
                    deps: [HttpClient]
                }
            })
        ),


        { provide: AUTH_SERVICE, useClass: AuthService },
        { provide: CALENDAR_SERVICE, useClass: CalendarService },
        { provide: CAMPAIGN_SERVICE, useClass: CampaignService },
        { provide: CRMRECORD_SERVICE, useClass: CrmRecordService },
        { provide: CRMRECORDACTION_SERVICE, useClass: CrmRecordActionService },
        { provide: COURSESALESETTING_SERVICE, useClass: CourseSaleSettingService },
        { provide: DISCOUNT_SERVICE, useClass: DiscountService },
        { provide: EMPLOYEE_SERVICE, useClass: EmployeeService },
        { provide: BRANCH_SERVICE, useClass: BranchService },
        { provide: BRANCHPRICINGSETTINGS_SERVICE, useClass: BranchPricingSettingsService },
        { provide: REGION_SERVICE, useClass: RegionService },
        { provide: INCENTIVESETTING_SERVICE, useClass: IncentiveSettingService },
        { provide: INSTALLMENTSETTING_SERVICE, useClass: InstallmentSettingService },
        { provide: LESSONSCHEDULEDEFINITION_SERVICE, useClass: LessonScheduleDefinitionService },
        { provide: APPUSER_SERVICE, useClass: AppUserService },

        ConfirmationService,
        MessageService
    ]
};
