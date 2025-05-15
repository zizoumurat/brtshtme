export interface CalculatePaymentResponseModel {
    totalAmount: number;
    installmentAmount: number;
    financedAmount: number;
    installments: any[];
}