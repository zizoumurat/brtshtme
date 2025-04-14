import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  private menuData = new BehaviorSubject<any[]>([]);
  menuData$ = this.menuData.asObservable();

  setMenuData(menus: any[]) {
    this.menuData.next(menus);
  }
}