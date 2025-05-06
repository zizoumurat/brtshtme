import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { CRMRECORDACTION_SERVICE } from '@/core/services/crm/crmrecordaction-service';
import { CrmRecordActionModel } from '@/core/models/crm/crmrecordaction.model';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [
    SharedComponentModule,
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
  calls: CrmRecordActionModel[] = [];

  ngOnInit() {
    this.getAppointments();
    this.getCalls();
  }

  async getAppointments() {
    var result = await this.crmRecordActionService.getAppointments(this.date);

    this.appointments = result;
  }

  async getCalls() {
    var result = await this.crmRecordActionService.getCalls(this.date);

    this.calls = result;
  }

  onDateChange(event: Date) {
    this.getAppointments();
    this.getCalls();
  }
}

