import { InjectionToken } from '@angular/core';
import { EmployeeModel } from '@/core/models/crm/employee.model';
import { ICrudService } from '../admin/crud-service';
import { SelectListItem } from '@/core/models/select-list-item.model';

export interface IEmployeeService extends ICrudService<EmployeeModel> {
    getUnassignedEmployees(branchId: string): Promise<SelectListItem[]>;
}

export const EMPLOYEE_SERVICE = new InjectionToken<IEmployeeService>('EmployeeService');
