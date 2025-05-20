import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { firstValueFrom } from 'rxjs';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { AppUserModel } from '@/core/models/crm/appuser.model';
import { IAppUserService } from '@/core/services/crm/appuser.service';
import { UnassignedEmployeeModel } from '@/core/models/crm/employee.model';


@Injectable({
    providedIn: 'root',
})
export class AppUserService extends CrudService<AppUserModel> implements IAppUserService {
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/Users`);
    }

    getUnassignedEmployees(branchId: string): Promise<UnassignedEmployeeModel[]> {
        return firstValueFrom(this.http.get<UnassignedEmployeeModel[]>(`${this.apiUrl}/unassigned-employees/${branchId}`));
    }
}
