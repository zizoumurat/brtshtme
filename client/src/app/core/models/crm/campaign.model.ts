
export interface CampaignModel extends HasId {
    definition: string;
    discountRate: number;
    isActive: boolean;
    branchId: string;
}
