import { InjectionToken } from '@angular/core';
import { MenuItemModel } from '../../models/admin/menu-item';

export interface IMenuItemService {
  getAll(): Promise<MenuItemModel[]>;
}

export const MENUITEM_SERVICE = new InjectionToken<IMenuItemService>('MenuItemService');
