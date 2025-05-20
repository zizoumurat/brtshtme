import { InjectionToken } from '@angular/core';
import { EmployeeModel, UnassignedEmployeeModel } from '@/core/models/crm/employee.model';
import { ICrudService } from '../admin/crud-service';

export interface IEmployeeService extends ICrudService<EmployeeModel> {
    getUnassignedEmployees(branchId: string): Promise<UnassignedEmployeeModel[]>;
}

export const EMPLOYEE_SERVICE = new InjectionToken<IEmployeeService>('EmployeeService');
