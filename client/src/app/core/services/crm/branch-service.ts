import { InjectionToken } from '@angular/core';
import { BranchModel } from '@/core/models/crm/branch.model';
import { ICrudService   } from '../admin/crud-service';

export interface IBranchService extends ICrudService<BranchModel> {
    getUserBranchList(): Promise<BranchModel[]>;
    clearBranchCache(): void;
}

export const BRANCH_SERVICE = new InjectionToken<IBranchService>('BranchService');
