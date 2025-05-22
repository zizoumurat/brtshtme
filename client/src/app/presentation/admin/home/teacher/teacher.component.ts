import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { SharedComponentModule } from '../../shared/shared-components.module';
import { COURSECLASS_SERVICE } from '@/core/services/crm/courseClass-service';
import { AUTH_SERVICE } from '@/core/services/admin/auth-token';
import { CalendarComponent } from '../../calendar/calendar.component';

@Component({
    selector: 'app-teacher-home',
    standalone: true,
    imports: [RouterModule, SharedComponentModule, CalendarComponent],
    templateUrl: './teacher.component.html'
})
export class TeacherHomeComponent {

    classService = inject(COURSECLASS_SERVICE);
    authService = inject(AUTH_SERVICE);

    events: any[] | undefined = undefined;

    constructor() { }

    ngOnInit(): void {
        this.getTeacherCalendar();
    }

    async getTeacherCalendar() {
        const user = this.authService.getUser();
        this.events = await this.classService.getSessionLessonByTeacher(user?.Id || '');
    }
}
