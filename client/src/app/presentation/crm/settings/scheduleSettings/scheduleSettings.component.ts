import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { LessonScheduleDefinitionModel } from '@/core/models/crm/lessonScheduleDefinition.model';
import { ILessonScheduleDefinitionService, LESSONSCHEDULEDEFINITION_SERVICE } from '@/core/services/crm/lessonScheduleDefinition-service';
import { BRANCH_SERVICE } from '@/core/services/crm/branch-service';
import { SelectListItem } from '@/core/models/admin/select-list-item';
import { DayOfWeek } from '@/core/enums/dayOfWeek';
import { StudentType } from '@/core/enums/studentType';
import { EducationType } from '@/core/enums/educationType';
import { ScheduleCategory } from '@/core/enums/scheduleCategory';
import { DefaultSelectOptionDirective } from '@/core/directives/default-select-options.directive';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-schedule-settings',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    DefaultSelectOptionDirective,
    SharedComponentModule
  ],
  templateUrl: './scheduleSettings.component.html'
})
export class ScheduleSettingsComponent extends AppBaseComponent<LessonScheduleDefinitionModel, ILessonScheduleDefinitionService> {
  branchService = inject(BRANCH_SERVICE);

  branchOptions: SelectListItem[] = [];
  studentTypeOptions: SelectListItem[] = [];
  educationTypeOptions: SelectListItem[] = [];
  scheduleCategoryOptions: SelectListItem[] = [];
  dayOfWeekOptions: SelectListItem[] = [];

  constructor(private fb: FormBuilder, private cdr: ChangeDetectorRef) {
    super(LESSONSCHEDULEDEFINITION_SERVICE);
    this.pageTitle = 'Ders Programı Tanımları';
  }

  override ngOnInit() {
    super.ngOnInit();
    this.initForm();
    this.getOptions();
  }

  initForm(): void {
    this.pageForm = this.fb.group({
      id: [null],
      studentType: [null, Validators.required],
      educationType: [null, Validators.required],
      scheduleCode: ['', Validators.required],
      scheduleCategory: [null, Validators.required],
      days: [[], Validators.required],
      startTime: [null, Validators.required],
      branchId: [null, Validators.required],
      discount: [0],
    });
  }

  getOptions() {
    this.getBranchList();
    forkJoin([
      this.enumToSelectOptionsAsync(StudentType, 'StudentType'),
      this.enumToSelectOptionsAsync(EducationType, 'EducationType'),
      this.enumToSelectOptionsAsync(ScheduleCategory, 'ScheduleCategory'),
      this.enumToSelectOptionsAsync(DayOfWeek, 'DayOfWeek')
    ]).subscribe(([studentTypes, educationTypes, scheduleCategories, dayOfWeeks]) => {
      this.studentTypeOptions = studentTypes;
      this.educationTypeOptions = educationTypes;
      this.scheduleCategoryOptions = scheduleCategories;
      this.dayOfWeekOptions = dayOfWeeks;
    });
  }

  async getBranchList() {
    this.branchOptions = await this.branchService.getUserBranchList();
  }

  override openModal(): void {
    super.openModal();
    if (this.branchOptions.length == 1) {
      this.pageForm.patchValue({ branchId: this.branchOptions[0].id });
    }
  }
}

