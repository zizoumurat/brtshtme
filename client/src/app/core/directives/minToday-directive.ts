import { Directive, OnInit } from '@angular/core';
import { DatePicker } from 'primeng/datepicker';

@Directive({
  selector: 'p-datepicker[minToday]',
  standalone: true,
})
export class MinTodayDirective implements OnInit {
  constructor(private host: DatePicker) {}

  ngOnInit() {
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    this.host.minDate = today;
  }
}
