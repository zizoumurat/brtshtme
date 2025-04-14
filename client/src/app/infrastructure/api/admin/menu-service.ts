import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { BASE_URL } from '../../../environments/environment';
import { MenuItemModel } from '../../../core/models/admin/menu-item';
import { IMenuItemService } from '@/core/services/admin/menuitem-service';

@Injectable({
    providedIn: 'root',
})
export class AppMenuService implements IMenuItemService {
    private apiUrl = `${BASE_URL}/menu`;

    constructor(private http: HttpClient) { }

    getAll(): Promise<MenuItemModel[]> {
        return firstValueFrom(
            this.http.get<{ data: MenuItemModel[] }>(`${this.apiUrl}/getAll`)
        ).then(response => response.data);
    }

}
