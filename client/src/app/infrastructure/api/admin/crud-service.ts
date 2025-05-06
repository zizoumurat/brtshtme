import { HttpClient, HttpParams } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { PaginationResponseModel } from '../../../core/models/admin/paginationResponseModel';
import { PaginationFilterModel } from '../../../core/models/admin/paginationFilterModel';
import { ICrudService } from '@/core/services/admin/crud-service';
import { SelectListItem } from '@/core/models/select-list-item.model';

export abstract class CrudService<T extends HasId> implements ICrudService<T> {
    protected constructor(protected http: HttpClient, protected apiUrl: string) { }

    getAll(filter: PaginationFilterModel, search: any | null = null): Promise<PaginationResponseModel<T>> {
        let params = new HttpParams()
            .set('page', filter.page.toString())
            .set('pageSize', filter.pageSize.toString());

        // sortByMultiName parametrelerini ekleyelim
        filter.sortByMultiName.forEach((name, index) => {
            params = params.append(`sortByMultiName[${index}]`, name);
        });

        // sortByMultiOrder parametrelerini ekleyelim
        filter.sortByMultiOrder.forEach((order, index) => {
            params = params.append(`sortByMultiOrder[${index}]`, order.toString());
        });

        // Eğer search objesi varsa, parametreleri ekleyelim
        if (search) {
            Object.keys(search).forEach(key => {
                let value = search[key];

                // Eğer değer bir tarih ise, uygun formatta düzenleyelim
                if (value instanceof Date) {
                    value = this.formatDates(value);
                }

                // Eğer değer bir dizi (array) ise, her bir elemanı parametre olarak ekleyelim
                if (Array.isArray(value)) {
                    value.forEach((item, index) => {
                        // Array öğelerini farklı parametreler olarak ekliyoruz
                        params = params.append(`${key}[${index}]`, item.toString());
                    });
                } else if (value !== null && value !== undefined && value !== "" && value !== 0) {
                    params = params.set(key, value.toString());
                }
            });
        }

        return firstValueFrom(
            this.http.get<PaginationResponseModel<T>>(`${this.apiUrl}`, { params })
        ).then(response => response);
    }

    getSelectList(...params: (string | number)[]): Promise<SelectListItem[]> {
        const routeParams = params?.length ? '/' + params.join('/') : '';
        return firstValueFrom(this.http.get<SelectListItem[]>(`${this.apiUrl}/select-list${routeParams}`));
    }

    create(item: T): Promise<any> {
        return firstValueFrom(this.http.post<any>(`${this.apiUrl}`, this.formatDates(item)));
    }

    update(item: T): Promise<void> {
        return firstValueFrom(this.http.put<void>(`${this.apiUrl}`, this.formatDates(item)));
    }

    delete(ref: number): Promise<void> {
        return firstValueFrom(this.http.delete<void>(`${this.apiUrl}/${ref}`));
    }

    protected formatDates(data: any) {
        Object.keys(data).forEach(key => {
            if (data[key] instanceof Date) {
                data[key] = data[key].toLocaleDateString('en-CA'); // YYYY-MM-DD formatı için
            }
        });
        return data;
    }

}
