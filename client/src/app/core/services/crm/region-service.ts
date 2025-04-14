import { InjectionToken } from '@angular/core';
import { RegionModel } from '@/core/models/crm/region.model';
import { ICrudService   } from '../admin/crud-service';

export interface IRegionService extends ICrudService<RegionModel> {}

export const REGION_SERVICE = new InjectionToken<IRegionService>('RegionService');
