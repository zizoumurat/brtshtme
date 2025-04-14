import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { IncentiveSettingModel } from '@/core/models/crm/incentiveSetting.model';
import { IIncentiveSettingService } from '@/core/services/crm/incentiveSetting-service';


@Injectable({
    providedIn: 'root',
})
export class IncentiveSettingService extends CrudService<IncentiveSettingModel> implements IIncentiveSettingService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/IncentiveSettings`);
    }
}
