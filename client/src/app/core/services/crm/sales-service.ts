import { CalculatePaymentModel } from '@/core/models/calculatePayment.model';
import { CalculatePaymentResponseModel } from '@/core/models/calculatePaymentResponse.model';
import { InjectionToken } from '@angular/core';

export interface ISalesService {
    calculateSalesAmount(model: CalculatePaymentModel): Promise<CalculatePaymentResponseModel>;
}

export const SALES_SERVICE = new InjectionToken<ISalesService>('SalesService');
