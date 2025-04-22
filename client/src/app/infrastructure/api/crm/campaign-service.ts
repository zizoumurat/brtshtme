import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { CampaignModel } from '@/core/models/crm/campaign.model';
import { ICampaignService } from '@/core/services/crm/campaign-service';


@Injectable({
    providedIn: 'root',
})
export class CampaignService extends CrudService<CampaignModel> implements ICampaignService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/Campaigns`);
    }
}
