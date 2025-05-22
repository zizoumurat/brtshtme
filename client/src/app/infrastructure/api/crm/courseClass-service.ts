import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { CourseClassModel } from '@/core/models/crm/courseClass.model';
import { ICourseClassService } from '@/core/services/crm/courseClass-service';
import { firstValueFrom } from 'rxjs';


@Injectable({
    providedIn: 'root',
})
export class CourseClassService extends CrudService<CourseClassModel> implements ICourseClassService {
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/CourseClasses`);
    }

    getSessionLessonByTeacher(employeeId: string): Promise<any[]> {
         return firstValueFrom(this.http.get<any[]>(`${this.apiUrl}/get-lesson-session-by-teacher/${employeeId}`));
    }

    getSessionLesson(courseClassId: string): Promise<any[]> {
        return firstValueFrom(this.http.get<any[]>(`${this.apiUrl}/get-lesson-session/${courseClassId}`));
    }

    createLessonSession(payload: any): Promise<void> {
        return firstValueFrom(this.http.post<void>(`${this.apiUrl}/create-lesson-session`, payload));
    }

    calculateEndDate(request: { startDate: Date; lessonScheduleId: string; }): Promise<Date | null> {
        return firstValueFrom(this.http.post<Date | null>(`${this.apiUrl}/calculate-end-date`, this.formatDates(request)));
    }
}
