import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeShort',
  standalone: true
})
export class TimeShortPipe implements PipeTransform {
  transform(value: string): string {
    if (!value) return '';
    return value.substring(0, 5); 
  }
}
