import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ILocationService } from '@/core/services/crm/locationService';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { lastValueFrom } from 'rxjs';

interface LocationResponse {
    data: any[];
}

@Injectable({
    providedIn: 'root',
})
export class LocationService implements ILocationService {
    private locationsData: any[] = [];
    private dataLoaded = false;

    constructor(private http: HttpClient) {

    }

    private async loadLocationsData(): Promise<void> {
        if (!this.dataLoaded) {
            try {
                const { data } = await lastValueFrom(this.http.get<LocationResponse>('assets/location/location.json'));
                this.locationsData = data;
                this.dataLoaded = true;
            } catch (error) {
                console.error('Error loading locations data:', error);
                throw error;
            }
        }
    }

    private async ensureDataLoaded(): Promise<void> {
        if (!this.dataLoaded) {
            await this.loadLocationsData();
        }
    }

    async getCityList(): Promise<SelectListItem[]> {
        await this.ensureDataLoaded();

        return this.locationsData
            .map(city => ({
                id: Number(city.plaka_kodu.trim()).toString(),
                name: city.il_adi.toUpperCase()
            } as SelectListItem))
            .sort((a, b) => a.name.localeCompare(b.name));
    }
    
    async getDistrictList(cityId: number | string): Promise<SelectListItem[]> {
        await this.ensureDataLoaded();

        const city = this.locationsData.find(c => Number(c.plaka_kodu.trim()).toString() === cityId.toString());

        const ilceler = city.ilceler.map((district: any) => ({
            id: Number(district.ilce_kodu).toString(),
            name: district.ilce_adi
        }) as SelectListItem);

        if (!city) {
            return [];
        }

        return city.ilceler.map((district: any) => ({
            id: Number(district.ilce_kodu).toString(),
            name: district.ilce_adi
        }) as SelectListItem);
    }
}
