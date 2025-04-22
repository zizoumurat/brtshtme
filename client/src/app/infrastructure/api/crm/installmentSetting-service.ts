import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { InstallmentSettingModel } from '@/core/models/crm/installmentSetting.model';
import { IInstallmentSettingService } from '@/core/services/crm/installmentSetting-service';


@Injectable({
    providedIn: 'root',
})
export class InstallmentSettingService extends CrudService<InstallmentSettingModel> implements IInstallmentSettingService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/InstallmentSettings`);
    }
}
