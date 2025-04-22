
export interface BranchPricingSettingsModel {
    id?: string;
    branchId: string;
    hourlyRate: number;
    discountForPrepayment: number;
    cashPrepaymentDiscount: number;
    creditCardInstallmentDiscount: number;
    installmentRate: number;
    collectionRateForBonus: number;
}
