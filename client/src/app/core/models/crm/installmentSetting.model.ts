
export interface InstallmentSettingModel extends HasId {
    level: number;
    maxBond: number;
    maxCardInstallment: number;
    branchId: string;
}
