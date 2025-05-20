import { Directive, AfterViewInit, ViewChild } from '@angular/core';
import { DatePicker } from 'primeng/datepicker';

@Directive({
  selector: 'p-datepicker'
})
export class PDatepickerFormatDirective implements AfterViewInit {
  @ViewChild(DatePicker) datepicker!: DatePicker;

  ngAfterViewInit() {
    if (this.datepicker) {
      this.datepicker.dateFormat = 'dd.mm.yy';
      this.datepicker.updateInputfield(); // Girişi güncellemek için
    }
  }
}
