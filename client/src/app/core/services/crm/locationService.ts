import { SelectListItem } from '@/core/models/select-list-item.model';
import { InjectionToken } from '@angular/core';

export interface ILocationService {
    getCityList(): Promise<SelectListItem[]>;
    getDistrictList(cityId: number): Promise<SelectListItem[]>;
}

export const LOCATION_SERVICE = new InjectionToken<ILocationService>('LocationService');
