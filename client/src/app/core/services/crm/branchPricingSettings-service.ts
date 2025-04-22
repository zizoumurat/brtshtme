import { InjectionToken } from '@angular/core';
import { BranchPricingSettingsModel } from '@/core/models/crm/branchPricingSettings.model';


export interface IBranchPricingSettingsService{
    getSettingsByBranchId(branchId: string): Promise<BranchPricingSettingsModel[]>;
    addOrUpdate(item: BranchPricingSettingsModel): void;
}

export const BRANCHPRICINGSETTINGS_SERVICE = new InjectionToken<IBranchPricingSettingsService>('BranchPricingSettingsService');
