import { InjectionToken } from '@angular/core';
import { ICrudService   } from '../admin/crud-service';
import { ClassRoomModel } from '@/core/models/crm/classRoom.model';

export interface IClassRoomService extends ICrudService<ClassRoomModel> {}

export const CLASSROOM_SERVICE = new InjectionToken<IClassRoomService>('ClassRoomService');
