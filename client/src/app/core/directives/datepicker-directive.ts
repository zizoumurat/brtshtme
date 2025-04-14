import { Directive, AfterViewInit, ViewChild } from '@angular/core';
import { Calendar } from 'primeng/calendar';

@Directive({
  selector: 'p-datepicker'
})
export class PDatepickerFormatDirective implements AfterViewInit {
  @ViewChild(Calendar) datepicker!: Calendar;

  ngAfterViewInit() {
    if (this.datepicker) {
      this.datepicker.dateFormat = 'dd.mm.yy';
      this.datepicker.updateInputfield(); // Girişi güncellemek için
    }
  }
}
