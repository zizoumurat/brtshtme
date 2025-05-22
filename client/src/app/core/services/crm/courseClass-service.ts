import { InjectionToken } from '@angular/core';
import { ICrudService } from '../admin/crud-service';
import { CourseClassModel } from '@/core/models/crm/courseClass.model';

export interface ICourseClassService extends ICrudService<CourseClassModel> {
    calculateEndDate(request: { startDate: Date, lessonScheduleId: string }): Promise<Date | null>;
    createLessonSession(payload: any): Promise<void>;
    getSessionLesson(courseClassId: string): Promise<any[]>;
    getSessionLessonByTeacher(employeeId: string): Promise<any[]>;
}

export const COURSECLASS_SERVICE = new InjectionToken<ICourseClassService>('CourseClassService');
