import { Component, Input, ViewChild, AfterViewInit } from '@angular/core';
import { FullCalendarComponent } from '@fullcalendar/angular';
import { CalendarOptions, EventInput } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import trLocale from '@fullcalendar/core/locales/tr';
import { FullCalendarModule } from '@fullcalendar/angular';

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [FullCalendarModule],
  template: `<full-calendar #calendarRef [options]="calendarOptions"></full-calendar>`
})
export class CalendarComponent implements AfterViewInit {
  @ViewChild('calendarRef') calendarComponent!: FullCalendarComponent;

  private _events: EventInput[] = [];

  @Input() set events(value: EventInput[]) {
    this._events = value;
    this.calendarOptions.events = value;
    this.triggerResizeFix();
  }

  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
    plugins: [dayGridPlugin],
    locale: trLocale,
    events: [],
    headerToolbar: {
      left: 'prev,next today',
      center: 'title',
      right: 'dayGridMonth,dayGridWeek',
    },
    editable: false,
    selectable: false,
    eventTimeFormat: {
      hour: '2-digit',
      minute: '2-digit',
      hour12: false,
    },
  };

  ngAfterViewInit(): void {
    this.triggerResizeFix();
  }

  private triggerResizeFix() {
    setTimeout(() => {
      const calendarApi = this.calendarComponent?.getApi();
      if (calendarApi) {
        const currentView = calendarApi.view.type;
        calendarApi.changeView('dayGridWeek'); // geçici view
        calendarApi.changeView(currentView);   // eski view'e dön
      }
    }, 300);
  }
}
