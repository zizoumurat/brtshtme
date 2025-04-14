import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { RegionModel } from '@/core/models/crm/region.model';
import { IRegionService } from '@/core/services/crm/region-service';


@Injectable({
    providedIn: 'root',
})
export class RegionService extends CrudService<RegionModel> implements IRegionService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/regions`);
    }
}
