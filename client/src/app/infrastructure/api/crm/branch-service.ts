import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { BranchModel } from '@/core/models/crm/branch.model';
import { CrudService } from '../admin/crud-service';
import { IBranchService } from '@/core/services/crm/branch-service';
import { firstValueFrom } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class BranchService extends CrudService<BranchModel> implements IBranchService {
    private branchCache: BranchModel[] | null = null;

    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/branches`);
    }

    async getUserBranchList(): Promise<BranchModel[]> {
        if (this.branchCache) {
            return Promise.resolve(this.branchCache);
        }
    
        return firstValueFrom(
            this.http.get<BranchModel[]>(`${this.apiUrl}/user-branch-list`)
        ).then(result => {
            this.branchCache = result ?? [];
            return this.branchCache;
        });
    }

    clearBranchCache(): void {
        this.branchCache = null;
    }
}
