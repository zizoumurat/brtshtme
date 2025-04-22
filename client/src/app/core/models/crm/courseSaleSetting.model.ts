
export interface CourseSaleSettingModel extends HasId {
    minLevel: number;
    maxLevel: number;
    rate: number;
    branchId: string;
}
