import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AppMenuitem } from './app.menuitem';
import { MenuItemModel } from '@/core/models/admin/menu-item';
import { IAuthService } from '@/core/services/admin/auth-service';
import { AUTH_SERVICE } from '@/core/services/admin/auth-token';
import { MenuService } from '@/core/services/menu.service';

@Component({
    selector: '[app-menu]',
    standalone: true,
    imports: [CommonModule, AppMenuitem, RouterModule],
    template: `
        <ul class="layout-menu">
            <ng-container *ngFor="let item of model; let i = index">
                <li app-menuitem *ngIf="!item.separator" [item]="item" [index]="i" [root]="true"></li>
                <li *ngIf="item.separator" class="menu-separator"></li>
            </ng-container>
        </ul>
    `
})
export class AppMenu {
    model: any[] = [
        {
            label: 'Menü',
            icon: 'pi pi-home',
            items: [
                {
                    label: 'Ana Sayfa',
                    icon: 'pi pi-home',
                    routerLink: ['/']
                },
                {
                    label: 'CRM',
                    icon: 'pi pi-th-large',
                    routerLink: ['/crm']
                },
                {
                    label: 'SRM',
                    icon: 'pi pi-th-large',
                    routerLink: ['/srm']
                },
                {
                    label: 'Muhasebe',
                    icon: 'pi pi-th-large',
                    routerLink: ['/srm']
                },
                {
                    label: 'Ayarlar',
                    icon: 'pi pi-conf',
                    routerLink: ['/srm']
                },
            ]
        }
    ];
}
