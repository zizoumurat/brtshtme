import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { firstValueFrom } from 'rxjs';
import { IBranchPricingSettingsService } from '@/core/services/crm/branchPricingSettings-service';
import { BranchPricingSettingsModel } from '@/core/models/crm/branchPricingSettings.model';


@Injectable({
    providedIn: 'root',
})
export class BranchPricingSettingsService implements IBranchPricingSettingsService {

    apiUrl: string = `${BASE_URL}/branchPricingSettings`;

    constructor(protected http: HttpClient) {}

    getSettingsByBranchId(branchId: string): Promise<BranchPricingSettingsModel[]> {
        return firstValueFrom(this.http.get<BranchPricingSettingsModel[]>(`${this.apiUrl}?branchId=${branchId}`));
    }

    addOrUpdate(item: BranchPricingSettingsModel): void {
        firstValueFrom(this.http.post<void>(`${this.apiUrl}`, item));
    }
}
