import { Component, inject, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { CourseClassModel } from '@/core/models/crm/courseClass.model';
import { CourseClassService } from '@/infrastructure/api/crm/courseClass-service';
import { COURSECLASS_SERVICE } from '@/core/services/crm/courseClass-service';
import { ParticipantType } from '@/core/enums/participantType';
import { ClassType, EducationType } from '@/core/enums/educationType';
import { ScheduleType } from '@/core/enums/studentType';
import { connect, forkJoin, Subject, takeUntil } from 'rxjs';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { PaginationFilterModel } from '@/core/models/admin/paginationFilterModel';
import { LESSONSCHEDULEDEFINITION_SERVICE } from '@/core/services/crm/lessonScheduleDefinition-service';
import { LessonScheduleDefinitionModel } from '@/core/models/crm/lessonScheduleDefinition.model';
import { LEVEL_SERVICE } from '@/core/services/crm/level-service';
import { MessageService } from 'primeng/api';
import { CLASSROOM_SERVICE } from '@/core/services/crm/classroom-service';
import { ClassRoomModel } from '@/core/models/crm/classRoom.model';
import { MinTodayDirective } from '@/core/directives/minToday-directive';
import { EMPLOYEE_SERVICE } from '@/core/services/crm/employee-service';
import { EmployeeModel } from '@/core/models/crm/employee.model';
import { CalendarComponent } from '@/presentation/admin/calendar/calendar.component';
import { FullCalendarComponent } from '@fullcalendar/angular';

@Component({
  selector: 'app-class-list',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule,
    MinTodayDirective,
    CalendarComponent
  ],
  templateUrl: './list.component.html'
})
export class ClassListComponent extends AppBaseComponent<CourseClassModel, CourseClassService> {
  @ViewChild(CalendarComponent) calendarRef!: CalendarComponent;

  constructor(private fb: FormBuilder) {
    super(COURSECLASS_SERVICE);
    this.pageTitle = 'Sınıf';
  }

  private destroy$ = new Subject<void>();

  lessonScheduleDefinitionService = inject(LESSONSCHEDULEDEFINITION_SERVICE);
  levelService = inject(LEVEL_SERVICE);
  classRoomService = inject(CLASSROOM_SERVICE);
  employeeService = inject(EMPLOYEE_SERVICE);
  messageService = inject(MessageService);

  classTypeOptions: SelectListItem[] = [];
  educationTypeOptions: SelectListItem[] = [];
  scheduleTypeOptions: SelectListItem[] = [];

  lessonScheduleList: LessonScheduleDefinitionModel[] = [];
  lessonScheduleOptions: LessonScheduleDefinitionModel[] = [];
  levelOptions: SelectListItem[] = [];
  classRoomOptions: ClassRoomModel[] = [];

  teacherList: SelectListItem[] = [];

  lessonSessionForm!: FormGroup;

  lessonSessionlist: any[] = [];
  showCalendar: boolean = false;

  override ngOnInit() {
    super.ngOnInit();
    this.getOptions();
    this.initForm();
    this.getLessonscheduleList();
    this.getLevels();
    this.getClasssRooms();
    this.getTeachers();
  }

  initForm(): void {
    this.pageForm = this.fb.group({
      id: [null],
      branchId: [null, [Validators.required]],
      name: [null, [Validators.required]],
      classType: [ClassType.Official, [Validators.required]],
      educationType: [EducationType.Online, [Validators.required]],
      scheduleType: [null, [Validators.required]],
      lessonScheduleDefinitionId: [null, [Validators.required]],
      levelId: [null, [Validators.required]],
      startDate: [null, [Validators.required]],
      description: [null],
      note: [null],
      endDate: [{ value: null, disabled: true }, Validators.required],
      capacity: [null, [Validators.required]],
      unit: [null, [Validators.required]],
      classroomId: [null, [Validators.required]],
    });

    this.pageForm.get('branchId')?.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.getLessonscheduleOptions();
        this.pageForm.get('lessonScheduleDefinitionId')?.setValue(null);
        this.pageForm.get('name')?.setValue(null);
      });

    this.pageForm.get('classType')?.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.getLessonscheduleOptions();
        this.pageForm.get('lessonScheduleDefinitionId')?.setValue(null);
        this.pageForm.get('name')?.setValue(null);
      });

    this.pageForm.get('scheduleType')?.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.getLessonscheduleOptions();
        this.pageForm.get('lessonScheduleDefinitionId')?.setValue(null);
        this.pageForm.get('name')?.setValue(null);
      });

    this.pageForm.get('educationType')?.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.getLessonscheduleOptions();
        this.pageForm.get('lessonScheduleDefinitionId')?.setValue(null);
        this.pageForm.get('name')?.setValue(null);
      });

    this.pageForm.get('classroomId')?.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        const capacityControl = this.pageForm.get('capacity');
        const selectedClassRoom = this.classRoomOptions.find(x => x.id == this.pageForm.get('classroomId')?.value);
        if (selectedClassRoom) {
          capacityControl?.setValue(selectedClassRoom.capacity);
          capacityControl?.setValidators([
            Validators.required,
            Validators.max(selectedClassRoom.capacity)
          ]);
          capacityControl?.updateValueAndValidity();
        }
      });

    this.pageForm.get('startDate')?.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.calculateEndDate();
      });
  }

  initLessonSessionForm(): void {
    var lessonScheduleDefinition = this.lessonScheduleOptions.find(x => x.id == this.pageForm.get('lessonScheduleDefinitionId')?.value);

    if (!lessonScheduleDefinition) {
      return;
    }


    this.lessonSessionForm = this.fb.group({
      id: [null],
      entries: this.fb.array(lessonScheduleDefinition?.days.map(day => this.fb.group({
        day: [day],
        teacherId: [null, [Validators.required]],
      })))
    });

    console.log(this.lessonSessionForm);
  }

  get entries(): FormArray {
    return this.lessonSessionForm.get('entries') as FormArray;
  }

  getOptions() {

    forkJoin([
      this.enumToSelectOptionsAsync(ClassType, 'ClassType'),
      this.enumToSelectOptionsAsync(EducationType, 'EducationType'),
      this.enumToSelectOptionsAsync(ScheduleType, 'ScheduleType'),
    ]).subscribe(([classTypes, educationTypes, scheduleTypes]) => {
      this.classTypeOptions = classTypes;
      this.educationTypeOptions = educationTypes;
      this.scheduleTypeOptions = scheduleTypes;
    });

  }

  override handleEdit(id: string): void {
    super.handleEdit(id);
    this.generateCode();
    this.initLessonSessionForm();
    this.getLessonSession();
  }

  async getLessonscheduleList() {
    var paginationFilter = new PaginationFilterModel();
    paginationFilter.pageSize = 100;
    paginationFilter.sortByMultiName = ['schedule'];
    var result = await this.lessonScheduleDefinitionService.getAll(paginationFilter);
    this.lessonScheduleList = result.items;
  }

  async getTeachers() {
    var paginationFilter = new PaginationFilterModel();
    paginationFilter.pageSize = 100;
    paginationFilter.sortByMultiName = ['firstName'];
    var result = await this.employeeService.getAll(paginationFilter);
    this.teacherList = result.items.map(x => ({ id: x.id, name: x.firstName + ' ' + x.lastName }));
  }

  async getLessonSession() {
    const lessonSession = await this.recordService.getSessionLesson(this.pageForm.get('id')?.value);

    this.lessonSessionlist = lessonSession;
    this.showCalendar = true;
  }

  getLessonscheduleOptions() {
    const educationType = this.pageForm.get('educationType')?.value;
    const studentType = this.pageForm.get('scheduleType')?.value;
    const branchId = this.pageForm.get('branchId')?.value;

    if (!branchId || studentType == null || educationType == null) {
      this.lessonScheduleOptions = [];
      return;
    }

    this.lessonScheduleOptions = this.lessonScheduleList.filter(x => x.branchId == branchId && x.studentType == studentType && x.educationType == educationType);

  }

  async getLevels() {
    var paginationFilter = new PaginationFilterModel();
    paginationFilter.pageSize = 100;
    paginationFilter.sortByMultiName = ['name'];
    var result = await this.levelService.getAll(paginationFilter);
    this.levelOptions = result.items;
  }

  async calculateEndDate() {
    const startDate = this.pageForm.get('startDate')?.value;
    const lessonScheduleDefinitionId = this.pageForm.get('lessonScheduleDefinitionId')?.value;
    this.pageForm.get('endDate')?.setValue(null);

    if (startDate == null || lessonScheduleDefinitionId == null) {
      this.pageForm.get('endDate')?.setValue(null);
      return;
    }

    const endDate = await this.recordService.calculateEndDate({ startDate: startDate, lessonScheduleId: lessonScheduleDefinitionId });

    if (endDate == null) {
      this.pageForm.get('endDate')?.setValue(null);
    } else {
      this.pageForm.get('endDate')?.setValue(new Date(endDate));
    }
  }

  async getClasssRooms() {
    var paginationFilter = new PaginationFilterModel();
    paginationFilter.pageSize = 100;
    paginationFilter.sortByMultiName = ['name'];
    var result = await this.classRoomService.getAll(paginationFilter, { 'branchId': this.pageForm.get('branchId')?.value });
    this.classRoomOptions = result.items;
  }

  generateCode() {
    const classType = this.pageForm.get('classType')?.value;
    const educationType = this.pageForm.get('educationType')?.value;
    const scheduleType = this.pageForm.get('scheduleType')?.value;
    const lessonScheduleDefinitionId = this.pageForm.get('lessonScheduleDefinitionId')?.value;
    const levelId = this.pageForm.get('levelId')?.value;
    const note = this.pageForm.get('note')?.value;

    this.pageForm.get('name')?.setValue(null);

    if (classType == null || educationType == null || scheduleType == null || lessonScheduleDefinitionId == null || levelId == null) {
      this.showError('Lütfen tüm alanları doldurun.');
      return;
    }

    if (classType == ClassType.Official && note == null) {
      this.showError('Asıl sınıflarda ek açıklama kısmı boş bırakılamaz.');
      return;
    }

    let code = '';

    code += classType == ClassType.Candidate ? 'A-' : '';
    code += this.lessonScheduleOptions.find(x => x.id == lessonScheduleDefinitionId)?.schedule || '';
    code += '-' + this.levelOptions.find(x => x.id == levelId)?.name || '';
    code += note ? '-' + note : '';

    this.pageForm.get('name')?.setValue(code);
  }

  showError(message: string) {
    this.messageService.add({
      severity: 'error',
      summary: 'Hata',
      detail: message
    });
  }

  async submitLessonSession() {
    if (this.lessonSessionForm.invalid) return;

    const programDaysWithTeachers: { [key: number]: string } = {};

    this.entries.value.forEach((entry: any) => {
      if (entry.teacherId) {
        programDaysWithTeachers[entry.day] = entry.teacherId;
      }
    });

    const payload = {
      classId: this.pageForm.get('id')?.value,
      programDaysWithTeachers
    };

    await this.recordService.createLessonSession(payload);
  }
}

