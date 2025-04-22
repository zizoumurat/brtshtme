import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Pipe({
    name: 'enumTranslate',
    standalone: true
  })
  export class EnumTranslatePipe implements PipeTransform {
    constructor(private translate: TranslateService) {}
  
    transform(value: number | string, enumType: any, enumName: string): string {
      const enumKey = enumType[value]; 
      return this.translate.instant(`enums.${enumName}.${enumKey}`);
    }
  }
  