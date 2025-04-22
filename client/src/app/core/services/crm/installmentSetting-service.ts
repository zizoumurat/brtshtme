import { InjectionToken } from '@angular/core';
import { InstallmentSettingModel } from '@/core/models/crm/installmentSetting.model';
import { ICrudService   } from '../admin/crud-service';

export interface IInstallmentSettingService extends ICrudService<InstallmentSettingModel> {}

export const INSTALLMENTSETTING_SERVICE = new InjectionToken<IInstallmentSettingService>('InstallmentSettingService');
