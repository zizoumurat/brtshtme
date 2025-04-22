
export interface DiscountModel extends HasId {
    definition: string;
    discountRate: number;
    isActive: boolean;
    branchId: string;
}
