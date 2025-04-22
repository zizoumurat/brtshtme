import { InjectionToken } from '@angular/core';
import { CourseSaleSettingModel } from '@/core/models/crm/courseSaleSetting.model';
import { ICrudService   } from '../admin/crud-service';

export interface ICourseSaleSettingService extends ICrudService<CourseSaleSettingModel> {}

export const COURSESALESETTING_SERVICE = new InjectionToken<ICourseSaleSettingService>('CourseSaleSettingService');
