import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dayShortNames',
  standalone: true
})
export class DayShortNamesPipe implements PipeTransform {
  private readonly dayNamesShort = ['Paz', 'Pts', 'Sal', 'Çar', 'Per', 'Cum', 'Cmt'];

  transform(days: number[]): string {
    if (!Array.isArray(days)) return '';
    return days.map(day => this.dayNamesShort[day % 7]).join(', ');
  }
}
