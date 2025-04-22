import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { DiscountModel } from '@/core/models/crm/discount.model';
import { IDiscountService } from '@/core/services/crm/discount-service';


@Injectable({
    providedIn: 'root',
})
export class DiscountService extends CrudService<DiscountModel> implements IDiscountService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/Discounts`);
    }
}
