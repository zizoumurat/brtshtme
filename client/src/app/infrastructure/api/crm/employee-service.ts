import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { EmployeeModel } from '@/core/models/crm/employee.model';
import { IEmployeeService } from '@/core/services/crm/employee-service';
import { firstValueFrom } from 'rxjs';
import { SelectListItem } from '@/core/models/select-list-item.model';


@Injectable({
    providedIn: 'root',
})
export class EmployeeService extends CrudService<EmployeeModel> implements IEmployeeService {
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/Employees`);
    }

    getUnassignedEmployees(branchId: string): Promise<SelectListItem[]> {
        return firstValueFrom(this.http.get<SelectListItem[]>(`${this.apiUrl}/unassigned-employees?branchId=${branchId}`));
    }
}
