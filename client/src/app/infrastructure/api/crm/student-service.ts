import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { StudentModel } from '@/core/models/crm/student.model';
import { IStudentService } from '@/core/services/crm/student-service';
import { firstValueFrom } from 'rxjs';


@Injectable({
    providedIn: 'root',
})
export class StudentService extends CrudService<StudentModel> implements IStudentService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/Students`);
    }

    override create(request: any): Promise<number> {
        return firstValueFrom(this.http.post<number>(`${this.apiUrl}`, super.formatDates(request)));
    }
}
