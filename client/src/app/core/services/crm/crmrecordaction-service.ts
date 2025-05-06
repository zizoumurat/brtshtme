import { InjectionToken } from '@angular/core';
import { ICrudService } from '../admin/crud-service';
import { CrmRecordActionModel } from '@/core/models/crm/crmrecordaction.model';

export interface ICrmRecordActionService extends ICrudService<CrmRecordActionModel> {
    getListByCrmRecord(crmRecordId: string): Promise<CrmRecordActionModel[]>;
    getAppointments(date: Date): Promise<CrmRecordActionModel[]>;
    getCalls(date: Date): Promise<CrmRecordActionModel[]>;
}

export const CRMRECORDACTION_SERVICE = new InjectionToken<ICrmRecordActionService>('CrmRecordActionService');
