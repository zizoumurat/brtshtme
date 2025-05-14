import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { ISalesService } from '@/core/services/crm/sales-service';
import { CalculatePaymentModel } from '@/core/models/calculatePayment.model';
import { CalculatePaymentResponseModel } from '@/core/models/calculatePaymentResponse.model';
import { firstValueFrom } from 'rxjs';


@Injectable({
    providedIn: 'root',
})
export class SalesService implements ISalesService{

    constructor(private http: HttpClient) {
    }

    calculateSalesAmount(model: CalculatePaymentModel): Promise<CalculatePaymentResponseModel> {
        return firstValueFrom(this.http.post<any>(`${BASE_URL}/Sales/Calculater`, model));
    }
}
