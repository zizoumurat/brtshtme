import { InjectionToken } from '@angular/core';
import { Calendar } from 'primeng/calendar';

export interface ICalendarService {
  getEvents(): Promise<Calendar[]>;
}

export const CALENDAR_SERVICE = new InjectionToken<ICalendarService>('CalendarService');