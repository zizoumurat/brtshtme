import { InjectionToken } from '@angular/core';
import { ICrudService } from '../admin/crud-service';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { AppUserModel } from '@/core/models/crm/appuser.model';

export interface IAppUserService extends ICrudService<AppUserModel> {
    getUnassignedEmployees(branchId: string): Promise<SelectListItem[]>;
}

export const APPUSER_SERVICE = new InjectionToken<IAppUserService>('AppUserService');
