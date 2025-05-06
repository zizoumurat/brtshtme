import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { CRMRECORDACTION_SERVICE } from '@/core/services/crm/crmrecordaction-service';
import { CrmRecordActionModel } from '@/core/models/crm/crmrecordaction.model';
import { FormModalComponent } from '../formmodal/formmodal.component';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [
    SharedComponentModule,
    FormModalComponent
  ],
  templateUrl: './tasks.component.html'
})
export class TasksComponent {
  crmRecordActionService = inject(CRMRECORDACTION_SERVICE);

  constructor(private fb: FormBuilder) {
  }

  products: any[] = [];
  date: Date = new Date();

  appointments: CrmRecordActionModel[] = [];
  openAppointments: CrmRecordActionModel[] = [];
  calls: CrmRecordActionModel[] = [];
  openCalls: CrmRecordActionModel[] = [];

  displayModal: boolean = false;
  crmRecordId: string | undefined;

  ngOnInit() {
    this.getAppointments();
    this.getOpenAppointments();
    this.getCalls();
    this.getOpenCalls();
  }

  async getAppointments() {
    var result = await this.crmRecordActionService.getAppointments(this.date);

    this.appointments = result;
  }

  async getOpenAppointments() {
    var result = await this.crmRecordActionService.getOpenAppointments();

    this.openAppointments = result;
  }

  async getCalls() {
    var result = await this.crmRecordActionService.getCalls(this.date);

    this.calls = result;
  }

  async getOpenCalls() {
    var result = await this.crmRecordActionService.getOpenCalls();

    this.openCalls = result;
  }

  onDateChange(event: Date) {
    this.getAppointments();
    this.getCalls();
  }

  showCrm(crmRecordId: string) {
    this.crmRecordId = crmRecordId;
    this.displayModal = true;
    console.log('geldi', crmRecordId)
  }

  closeModal() {
    this.displayModal = false;
  }
}

