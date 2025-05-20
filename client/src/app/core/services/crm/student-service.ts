import { InjectionToken } from '@angular/core';
import { StudentModel } from '@/core/models/crm/student.model';
import { ICrudService } from '../admin/crud-service';

export interface IStudentService extends ICrudService<StudentModel> {
    create(request: any): Promise<any>;
}

export const STUDENT_SERVICE = new InjectionToken<IStudentService>('StudentService');
