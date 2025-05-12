import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { CrudService } from '../admin/crud-service';
import { LevelModel } from '@/core/models/crm/level.model';
import { ILevelService } from '@/core/services/crm/level-service';


@Injectable({
    providedIn: 'root',
})
export class LevelService extends CrudService<LevelModel> implements ILevelService{
    constructor(http: HttpClient) {
        super(http, `${BASE_URL}/Levels`);
    }
}
