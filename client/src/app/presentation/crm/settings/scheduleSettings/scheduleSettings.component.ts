import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { LessonScheduleDefinitionModel } from '@/core/models/crm/lessonScheduleDefinition.model';
import { ILessonScheduleDefinitionService, LESSONSCHEDULEDEFINITION_SERVICE } from '@/core/services/crm/lessonScheduleDefinition-service';

@Component({
  selector: 'app-schedule-settings',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './scheduleSettings.component.html'
})
export class ScheduleSettingsComponent extends AppBaseComponent<LessonScheduleDefinitionModel, ILessonScheduleDefinitionService> {

  constructor(private fb: FormBuilder) {
    super(LESSONSCHEDULEDEFINITION_SERVICE);
    this.pageTitle = 'Ders Programı Tanımları';
  }

  override ngOnInit() {
    super.ngOnInit();
    this.initForm();
  }

  initForm(): void {
    this.pageForm = this.fb.group({
      id: [null],
      studentType: [null, Validators.required],
      educationType: [null, Validators.required], 
      scheduleCode: ['', Validators.required],
      dayCount: [null, Validators.required],
      dayHour: [null, Validators.required],
      days: [[], Validators.required], 
      startTime: [null, Validators.required],
      endTime: [null, Validators.required],
      discount: [0, [Validators.required, Validators.min(0)]],
      scheduleCategory: [null, Validators.required],
      branchId: [null, Validators.required],
    });
  }
}

