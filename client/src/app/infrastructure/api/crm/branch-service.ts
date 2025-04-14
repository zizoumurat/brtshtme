import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { BranchModel } from '@/core/models/crm/branch.model';
import { CrudService } from '../admin/crud-service';
import { IBranchService } from '@/core/services/crm/branch-service';

@Injectable({
    providedIn: 'root',
})
export class BranchService extends CrudService<BranchModel> implements IBranchService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/branches`);
    }
}
