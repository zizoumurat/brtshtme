import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { CourseSaleSettingModel } from '@/core/models/crm/courseSaleSetting.model';
import { ICourseSaleSettingService } from '@/core/services/crm/courseSaleSetting-service';


@Injectable({
    providedIn: 'root',
})
export class CourseSaleSettingService extends CrudService<CourseSaleSettingModel> implements ICourseSaleSettingService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/CourseSaleSettings`);
    }
}
