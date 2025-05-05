import { InjectionToken } from '@angular/core';
import { CrmRecordModel } from '@/core/models/crm/crmrecord.model';
import { ICrudService } from '../admin/crud-service';

export interface ICrmRecordService extends ICrudService<CrmRecordModel> {
    checkPhone(phone: string): Promise<CrmRecordModel | null>;
}

export const CRMRECORD_SERVICE = new InjectionToken<ICrmRecordService>('CrmRecordService');
