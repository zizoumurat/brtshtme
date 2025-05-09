import { ChangeDetectorRef, Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { CrmRecordActionModel } from '@/core/models/crm/crmrecordaction.model';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { CrmDataSource } from '@/core/enums/crmDataSource';
import { CrmStatus } from '@/core/enums/crmStatus';
import { CrmActionType } from '@/core/enums/crmActionType';
import { forkJoin } from 'rxjs';
import { AUTH_SERVICE } from '@/core/services/admin/auth-token';
import { CRMRECORD_SERVICE } from '@/core/services/crm/crmrecord-service';
import { CRMRECORDACTION_SERVICE } from '@/core/services/crm/crmrecordaction-service';
import { UtilsHelper } from '@/core/helpers/utils.hlper';
import { CrmRecordModel } from '@/core/models/crm/crmrecord.model';
import { EMPLOYEE_SERVICE } from '@/core/services/crm/employee-service';
import { REGION_SERVICE } from '@/core/services/crm/region-service';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-formmodal',
  standalone: true,
  imports: [
    SharedComponentModule,
  ],
  templateUrl: './formmodal.component.html'
})
export class FormModalComponent {
  @Input() displayModal: boolean = false;
  @Input() id: string | undefined = undefined;
  @Output() onCloseModal = new EventEmitter();

  authService = inject(AUTH_SERVICE);
  crmRecordService = inject(CRMRECORD_SERVICE);
  crmRecordActionService = inject(CRMRECORDACTION_SERVICE);
  regionService = inject(REGION_SERVICE);
  employeeService = inject(EMPLOYEE_SERVICE);
  utilsHelper = inject(UtilsHelper);

  CrmActionType = CrmActionType;

  pageForm!: FormGroup;
  actionForm!: FormGroup;
  smsForm!: FormGroup;

  regionOptions: SelectListItem[] = []
  dataSourceOptions: SelectListItem[] = [];
  dataProviderOptions: SelectListItem[] = [];
  crmStatusOptions: SelectListItem[] = [];
  actionTypeOptions: SelectListItem[] = [];
  allowedActionTypes: SelectListItem[] = [];

  currentDate: Date = new Date();
  currentUser: string = '';

  crmActions: CrmRecordActionModel[] = [];

  selectedPhoneOption: 'phone1' | 'phone2' | 'manual' = 'phone1'

  saleList: any[] = [];

  constructor(private fb: FormBuilder, private confirmationService: ConfirmationService) {
  }


  ngOnInit() {
    this.initForm();
    this.getOptions();
    this.initActionForm();
    this.initSmsForm();
    this.getCrmRecord();

    this.currentUser = this.authService.getUser()?.Name || '';

    this.saleList = [
      { employeeName: 'Murat Dere', date: new Date(), amount: 5000 },
      { employeeName: 'Murat Dere', date: new Date(), amount: 15000 },
    ]
  }

  initForm(): void {
    this.pageForm = this.fb.group({
      id: [null],
      branchId: [null, Validators.required],
      phone: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      secondPhone: [''],
      email: [''],
      dataProviderId: ['', Validators.required],
      salesRepresentativeId: [''],
      regionId: [''],
      dataSource: [0, Validators.required],
      excludeFromCommission: [false],
      description: [''],
      date: [new Date(), Validators.required],
      status: [{ value: CrmStatus.NewData, disabled: true }]
    });

    this.pageForm.get('branchId')?.valueChanges.subscribe(async id => {
      if (id) {
        this.dataProviderOptions = await this.employeeService.getSelectList(id);
      }
    });

  }

  initActionForm(): void {
    this.actionForm = this.fb.group({
      crmRecordId: [null, Validators.required],
      targetDate: [{ value: null, disabled: true }],
      actionType: ['', Validators.required],
      description: ['']
    });

    this.actionForm.get('actionType')?.valueChanges.subscribe(value => {
      const targetDateControl = this.actionForm.get('targetDate');

      if (value === CrmActionType.CallBack || value === CrmActionType.Appointment) {
        targetDateControl?.setValidators([Validators.required]);
        targetDateControl?.enable();
      } else {
        targetDateControl?.clearValidators();
        targetDateControl?.disable();
        targetDateControl?.setValue(null);
      }

      targetDateControl?.updateValueAndValidity();
    });
  }

  initSmsForm(): void {
    this.smsForm = this.fb.group({
      crmRecordId: [null, Validators.required],
      phone: ['', Validators.required],
      phoneOption: ['phone1', Validators.required],
      message: ['', Validators.required, Validators.maxLength(250)],
    });

    this.smsForm.get('phoneOption')?.valueChanges.subscribe(async selected => {
      if (selected === 'phone1') {
        this.smsForm.get('phone')?.setValue(this.pageForm.controls['phone'].value);
        this.smsForm.controls['phone'].disable();
      }

      if (selected === 'phone2') {
        this.smsForm.get('phone')?.setValue(this.pageForm.controls['secondPhone'].value);
        this.smsForm.controls['phone'].disable();
      }

      if (selected === 'manual') {
        this.smsForm.get('phone')?.setValue('');
        this.smsForm.controls['phone'].enable();
      }
    });
  }

  async getCrmRecord() {
    if (this.id) {
      var crmRecord = await this.crmRecordService.getById(this.id);
      this.pageForm.patchValue(crmRecord);
      this.getActionList();
    }
  }

  closeModal() {
    this.onCloseModal.emit();
  }

  visibleChange(value: boolean) {
    if (!value) {
      this.closeModal();
    }
  }

  async getRegionList() {
    this.regionOptions = await this.regionService.getSelectList();
  }

  async getActionList() {
    var list = await this.crmRecordActionService.getListByCrmRecord(this.pageForm.controls['id'].value) as CrmRecordActionModel[];
    this.crmActions = list.sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());
    this.checkActionPermission();
  }

  checkActionPermission() {
    const lastAction = [...this.crmActions]
      .filter(x => x.actionType !== CrmActionType.Other)
      .sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime())[0];

    if (lastAction.actionType == CrmActionType.Sale) {
      const otherActionExists = this.crmActions.some(a => a.actionType === CrmActionType.Other);

      if (otherActionExists) {
        this.actionForm.reset();
      } else {
        this.actionForm.enable();
        this.allowedActionTypes = this.actionTypeOptions.filter(option => option.id === (CrmActionType.Other).toString());
        this.actionForm.get('actionType')?.setValue(CrmActionType.Other);
        this.actionForm.get('actionType')?.disable();
      }
    } else {
      this.actionForm.enable();
      this.allowedActionTypes = this.actionTypeOptions;
      this.actionForm.get('actionType')?.enable();
    }
  }

  getOptions() {
    this.getRegionList();

    forkJoin([
      this.utilsHelper.enumToSelectOptionsAsync(CrmDataSource, 'CrmDataSource'),
      this.utilsHelper.enumToSelectOptionsAsync(CrmStatus, 'CrmStatus'),
      this.utilsHelper.enumToSelectOptionsAsync(CrmActionType, 'CrmActionType'),
    ]).subscribe(([crmDataSources, crmStatuses, crmActionTypes]) => {
      this.dataSourceOptions = crmDataSources;
      this.crmStatusOptions = crmStatuses;
      this.actionTypeOptions = crmActionTypes;
    });
  }

  async checkPhone() {
    if (this.pageForm.get('id')?.value)
      return;

    const phone = this.pageForm.controls['phone'].value;
    if (!phone) return;

    var existingCrm = await this.crmRecordService.checkPhone(phone);

    if (!existingCrm) {
      return;
    }

    this.pageForm.patchValue(existingCrm);
    this.actionForm.patchValue({ crmRecordId: existingCrm.id });
    this.smsForm.get('phone')?.setValue(this.pageForm.controls['phone'].value);
    this.smsForm.controls['phone'].disable();

    this.getActionList();

    this.confirmationService.confirm({});

    this.pageForm.controls['email'].enable();
    this.pageForm.controls['secondPhone'].enable();

    Object.keys(this.pageForm.controls).forEach(controlName => {
      if (controlName !== 'email' && controlName !== 'secondPhone') {
        this.pageForm.controls[controlName].disable();
      }
    });
  }

  async saveRecord() {
    if (this.pageForm.valid) {
      const formData = this.pageForm.getRawValue() as CrmRecordModel;
      if (formData.id) {
        await this.crmRecordService.update(formData);
      }
      else {
        const result = await this.crmRecordService.create(formData);
        this.pageForm.patchValue({ id: result.id });
      }

      this.displayModal = false;
    } else {
      this.pageForm.markAllAsTouched();
    }
  }

  async saveAction() {
    if (this.actionForm.valid) {
      const formData = this.actionForm.getRawValue() as CrmRecordActionModel;
      await this.crmRecordActionService.create(formData);
      this.actionForm.reset();
      this.actionForm.patchValue({ crmRecordId: this.pageForm.controls['id'].value });
      this.getActionList();
    } else {
      this.actionForm.markAllAsTouched();
    }
  }

  getTotalAmount(): number {
    if (!this.saleList || this.saleList.length === 0) return 0;
    return this.saleList.reduce((sum, item) => sum + (item.amount || 0), 0);
  }

  isFieldInvalid(controlName: string): boolean {
    const control = this.pageForm.get(controlName);
    return control?.invalid && control?.touched ? true : false;
  }

}

