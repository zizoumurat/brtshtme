import { Component, inject, OnInit } from '@angular/core';
import { SharedComponentModule } from '../shared/shared-components.module';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import timeGridPlugin from '@fullcalendar/timegrid';
import { CalendarOptions } from '@fullcalendar/core/index.js';
import loTR from '@fullcalendar/core/locales/tr';
import { CALENDAR_SERVICE, ICalendarService } from '@/core/services/admin/calendar-service';

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [
    SharedComponentModule
  ],
  templateUrl: './calendar.component.html'
})

export class CalendarComponent implements OnInit {
  private calendarService = inject<ICalendarService>(CALENDAR_SERVICE);

  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
    locales: [loTR],
    locale: 'tr',
    firstDay: 1,
    buttonText: {
      today: 'Bugün',
      month: 'Ay',
      week: 'Hafta',
      day: 'Gün',
      list: 'Liste'
    },
    allDayText: 'Tüm Gün',
    moreLinkText: 'Daha Fazla',
    noEventsText: 'Gösterilecek etkinlik yok'
  };


  events: any[] = [];

  today: string = '';

  showDialog: boolean = false;

  clickedEvent: any = null;

  dateClicked: boolean = false;

  edit: boolean = false;

  tags: any[] = [];

  view: string = '';

  changedEvent: any;

  constructor() { }

  ngOnInit(): void {
    this.today = new Date().toISOString().split('T')[0];


    this.calendarService.getEvents().then(events => {
      this.events = events;
      this.calendarOptions = { ...this.calendarOptions, ...{ events: events } };
      this.tags = this.events.map(item => item.tag);
    });

    this.calendarOptions = {
      plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
      height: 720,
      initialDate: this.today,
      locales: [loTR],
      locale: 'tr',
      headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
      },
      editable: true,
      selectable: true,
      selectMirror: true,
      dayMaxEvents: true,
      eventClick: this.onEventClick.bind(this)
    };
  }

  onEventClick(e: any) {
    this.clickedEvent = e.event;
    let plainEvent = e.event.toPlainObject({ collapseExtendedProps: true, collapseColor: true });
    this.view = 'display';
    this.showDialog = true;

    this.changedEvent = { ...plainEvent, ...this.clickedEvent };
    this.changedEvent.start = this.clickedEvent.start;
    this.changedEvent.end = this.clickedEvent.end ? this.clickedEvent.end : this.clickedEvent.start;
  }

  handleSave() {
    if (!this.validate()) {
      return;
    }
    else {
      this.showDialog = false;
      this.clickedEvent = { ...this.changedEvent, backgroundColor: this.changedEvent.tag.color, borderColor: this.changedEvent.tag.color, textColor: '#212121' };

      if (this.clickedEvent.hasOwnProperty('id')) {
        this.events = this.events.map(i => i.id.toString() === this.clickedEvent.id.toString() ? i = this.clickedEvent : i);
      } else {
        this.events = [...this.events, { ...this.clickedEvent, id: Math.floor(Math.random() * 10000) }];
      }
      this.calendarOptions = { ...this.calendarOptions, ...{ events: this.events } };
      this.clickedEvent = null;
    }

  }

  onEditClick() {
    this.view = 'edit';
  }

  delete() {
    this.events = this.events.filter(i => i.id.toString() !== this.clickedEvent.id.toString());
    this.calendarOptions = { ...this.calendarOptions, ...{ events: this.events } };
    this.showDialog = false;
  }

  validate() {
    let { start, end } = this.changedEvent;
    return start && end;
  }

}
