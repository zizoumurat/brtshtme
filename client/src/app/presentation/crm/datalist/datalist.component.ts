import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { CrmRecordModel } from '@/core/models/crm/crmrecord.model';
import { CRMRECORD_SERVICE, ICrmRecordService } from '@/core/services/crm/crmrecord-service';
import { CrmStatus } from '@/core/enums/crmStatus';
import { FormModalComponent } from '../formmodal/formmodal.component';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { EMPLOYEE_SERVICE } from '@/core/services/crm/employee-service';
import { REGION_SERVICE } from '@/core/services/crm/region-service';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { CrmActionType } from '@/core/enums/crmActionType';
import { CrmDataSource } from '@/core/enums/crmDataSource';
import { UtilsHelper } from '@/core/helpers/utils.hlper';
import { IAuthService } from '@/core/services/admin/auth-service';
import { AUTH_SERVICE } from '@/core/services/admin/auth-token';
import { EventService } from '@/core/services/event.service';

@Component({
  selector: 'app-data-list',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule,
    FormModalComponent
  ],
  templateUrl: './datalist.component.html'
})
export class DataListComponent extends AppBaseComponent<CrmRecordModel, ICrmRecordService> {
  private destroy$ = new Subject<void>();

  regionService = inject(REGION_SERVICE);
  employeeService = inject(EMPLOYEE_SERVICE);
  utilsHelper = inject(UtilsHelper);
  authService = inject<IAuthService>(AUTH_SERVICE);
  eventService = inject(EventService);

  CrmStatus = CrmStatus;

  crmRecordId: string | undefined = undefined;

  regionOptions: SelectListItem[] = []
  dataSourceOptions: SelectListItem[] = [];
  dataProviderOptions: SelectListItem[] = [];
  crmStatusOptions: SelectListItem[] = [];

  constructor(private fb: FormBuilder) {
    super(CRMRECORD_SERVICE);
    this.pageTitle = 'Data';
  }

  override ngOnInit() {
    super.ngOnInit();
    this.initForm();
    this.getOptions();

    this.eventService.on('crmRefresh')
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.loadData();
      });

    this.eventService.on('crmActionRefresh')
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.loadData();
      });

  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  initForm(): void {
    this.pageForm = this.fb.group({
      id: [null],
      name: ['', Validators.required],
      description: [''],
      address: [''],
      phoneNumber: [''],
      email: ['', [Validators.required, Validators.email]],
      lessonDurationInMinutes: [null, Validators.required],
      breakDurationInMinutes: [null, Validators.required],
      levelDurationInHours: [null, Validators.required]
    });
  }

  async getRegionList() {
    this.regionOptions = await this.regionService.getSelectList();
  }

  async getEmployeeList() {
    var user = this.authService.getUser();
    this.dataProviderOptions = await this.employeeService.getSelectList(user?.BranchId || '');
  }

  getOptions() {
    this.getRegionList();
    this.getEmployeeList();

    forkJoin([
      this.utilsHelper.enumToSelectOptionsAsync(CrmDataSource, 'CrmDataSource'),
      this.utilsHelper.enumToSelectOptionsAsync(CrmStatus, 'CrmStatus'),
    ]).subscribe(([crmDataSources, crmStatuses]) => {
      this.dataSourceOptions = crmDataSources;
      this.crmStatusOptions = crmStatuses;
    });
  }

  showCrm(crmRecordId: string) {
    this.crmRecordId = crmRecordId;
    this.displayModal = true;
  }
}


