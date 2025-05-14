export interface CalculatePaymentModel {
    lessonScheduleId: string;
    branchId: string;
    levelCount: number;
    installmentCount: number;
    paymentMethod: number;
    campaignId: string | null;
    discountId: string | null;
}