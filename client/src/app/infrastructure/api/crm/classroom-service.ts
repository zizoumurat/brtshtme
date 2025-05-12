import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { ClassRoomModel } from '@/core/models/crm/classRoom.model';
import { IClassRoomService } from '@/core/services/crm/classroom-service';


@Injectable({
    providedIn: 'root',
})
export class ClassRoomService extends CrudService<ClassRoomModel> implements IClassRoomService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/ClassRooms`);
    }
}
