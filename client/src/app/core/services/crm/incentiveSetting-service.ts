import { InjectionToken } from '@angular/core';
import { IncentiveSettingModel } from '@/core/models/crm/incentiveSetting.model';
import { ICrudService   } from '../admin/crud-service';

export interface IIncentiveSettingService extends ICrudService<IncentiveSettingModel> {}

export const INCENTIVESETTING_SERVICE = new InjectionToken<IIncentiveSettingService>('IncentiveSettingService');
