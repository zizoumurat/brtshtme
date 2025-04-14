import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from '../../../environments/environment';
import { ICalendarService } from '../../../core/services/admin/calendar-service';
import { Calendar } from 'primeng/calendar';
import { firstValueFrom } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class CalendarService implements ICalendarService {
    private apiUrl = `${BASE_URL}/contact`;

    constructor(private http: HttpClient) { }

    getEvents(): Promise<Calendar[]> {
        return firstValueFrom(
            this.http.get<{ data: Calendar[] }>(`demo/data/scheduleevents.json`)
        ).then(response => response.data);

    }
}
