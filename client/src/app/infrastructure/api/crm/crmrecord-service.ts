import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { CrmRecordModel } from '@/core/models/crm/crmrecord.model';
import { ICrmRecordService } from '@/core/services/crm/crmrecord-service';
import { firstValueFrom } from 'rxjs';


@Injectable({
    providedIn: 'root',
})
export class CrmRecordService extends CrudService<CrmRecordModel> implements ICrmRecordService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/CrmRecords`);
    }

    checkPhone(phone: string): Promise<CrmRecordModel | null> {
        let params = new HttpParams()
        .set('phone', phone);

        return firstValueFrom(this.http.get<CrmRecordModel | null>(`${this.apiUrl}/check-phone`, { params }));
    }

    override create(item: CrmRecordModel): Promise<number> {
        return firstValueFrom(this.http.post<number>(`${this.apiUrl}`, super.formatDates(item)));
    }
}
