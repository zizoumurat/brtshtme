import { InjectionToken } from '@angular/core';
import { LessonScheduleDefinitionModel } from '@/core/models/crm/lessonScheduleDefinition.model';
import { ICrudService   } from '../admin/crud-service';

export interface ILessonScheduleDefinitionService extends ICrudService<LessonScheduleDefinitionModel> {}

export const LESSONSCHEDULEDEFINITION_SERVICE  = new InjectionToken<ILessonScheduleDefinitionService>('LessonScheduleDefinitionService');
