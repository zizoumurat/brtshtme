import { Component, inject } from '@angular/core';
import { SharedComponentModule } from '../shared/shared-components.module';
import { TableLazyLoadEvent } from 'primeng/table';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AUTH_SERVICE } from '@/core/services/admin/auth-token';
import { TeacherHomeComponent } from './teacher/teacher.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    SharedComponentModule,
    TeacherHomeComponent
  ],
  templateUrl: './home.component.html'
})
export class HomeComponent {

  totalRecords: number = 0;
  maxPageRow: number = 20;
  loading: boolean = true;
  displayModal: boolean = false;

  lastLazyLoadEvent: TableLazyLoadEvent | undefined;

  pageForm!: FormGroup;

  authService = inject(AUTH_SERVICE);

  contactTypes = [
    { name: "Aday Müşteri", value: 1 },
    { name: "Tedarikçi", value: 2 },
    { name: "Nakliyeci", value: 3 },
    { name: "Fırsat", value: 4 },
    { name: "Personel", value: 5 },
    { name: "Diğer", value: 9 }
  ];

  constructor(private fb: FormBuilder) { }

  get isTeacher() {
    return this.authService.isTeacher();
  }

  ngOnInit() {
  }
}
