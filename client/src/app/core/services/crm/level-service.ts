import { InjectionToken } from '@angular/core';
import { ICrudService   } from '../admin/crud-service';
import { LevelModel } from '@/core/models/crm/level.model';

export interface ILevelService extends ICrudService<LevelModel> {}

export const LEVEL_SERVICE = new InjectionToken<ILevelService>('LevelService');
