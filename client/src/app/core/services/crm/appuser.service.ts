import { InjectionToken } from '@angular/core';
import { ICrudService } from '../admin/crud-service';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { AppUserModel } from '@/core/models/crm/appuser.model';
import { UnassignedEmployeeModel } from '@/core/models/crm/employee.model';

export interface IAppUserService extends ICrudService<AppUserModel> {
    getUnassignedEmployees(branchId: string): Promise<UnassignedEmployeeModel[]>;
}

export const APPUSER_SERVICE = new InjectionToken<IAppUserService>('AppUserService');
