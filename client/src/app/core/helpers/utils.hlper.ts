import { inject, Injectable } from '@angular/core';
import { forkJoin, map, Observable } from 'rxjs';
import { SelectListItem } from '../models/select-list-item.model';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root' // tüm app içinde erişilebilir
})
export class UtilsHelper {
    translateService: TranslateService = inject(TranslateService);

    enumToSelectOptionsAsync<T extends object>(enumObj: T, prefix: string): Observable<SelectListItem[]> {
        const keys = Object.keys(enumObj).filter(key => isNaN(Number(key)));
        const translations$ = keys.map(key =>
          this.translateService.get(`enums.${prefix}.${key}`)
        );
      
        return forkJoin(translations$).pipe(
          map(translations => {
            return keys.map((key, index) => ({
              name: translations[index],
              id: enumObj[key as keyof T]
            }) as SelectListItem);
          })
        );
    }
}
