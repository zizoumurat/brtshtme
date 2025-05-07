import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { ICrmRecordActionService } from '@/core/services/crm/crmrecordaction-service';
import { CrmRecordActionModel } from '@/core/models/crm/crmrecordaction.model';
import { firstValueFrom } from 'rxjs';


@Injectable({
    providedIn: 'root',
})
export class CrmRecordActionService extends CrudService<CrmRecordActionModel> implements ICrmRecordActionService {
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/CrmRecordActions`);
    }
    
    getCalls(date: Date): Promise<CrmRecordActionModel[]> {
        const isoDate = date.toLocaleDateString('en-CA');
        const params = new HttpParams().set('date', isoDate);
    
        return firstValueFrom(this.http.get<CrmRecordActionModel[]>(`${this.apiUrl}/Calls`, { params }));
    }

    getOpenCalls(): Promise<CrmRecordActionModel[]> {
        return firstValueFrom(this.http.get<CrmRecordActionModel[]>(`${this.apiUrl}/OpenCalls`));
    }

    getAppointments(date: Date): Promise<CrmRecordActionModel[]> {
        const isoDate = date.toLocaleDateString('en-CA');
        const params = new HttpParams().set('date', isoDate);
    
        return firstValueFrom(this.http.get<CrmRecordActionModel[]>(`${this.apiUrl}/Appointments`, { params }));
    }

    getOpenAppointments(): Promise<CrmRecordActionModel[]> {
        return firstValueFrom(this.http.get<CrmRecordActionModel[]>(`${this.apiUrl}/OpenAppointments`));
    }
    
    getListByCrmRecord(crmRecordId: string): Promise<CrmRecordActionModel[]> {
        let params = new HttpParams()
            .set('crmRecordId', crmRecordId);

        return firstValueFrom(this.http.get<CrmRecordActionModel[]>(`${this.apiUrl}/get-by-crm`, { params }));
    }
}
