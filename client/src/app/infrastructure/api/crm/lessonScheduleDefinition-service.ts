import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { LessonScheduleDefinitionModel } from '@/core/models/crm/lessonScheduleDefinition.model';
import { ILessonScheduleDefinitionService } from '@/core/services/crm/lessonScheduleDefinition-service';


@Injectable({
    providedIn: 'root',
})
export class LessonScheduleDefinitionService extends CrudService<LessonScheduleDefinitionModel> implements ILessonScheduleDefinitionService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/LessonScheduleDefinitions`);
    }
}
