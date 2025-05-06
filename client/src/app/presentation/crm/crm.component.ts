import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { SharedComponentModule } from '../admin/shared/shared-components.module';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { REGION_SERVICE } from '@/core/services/crm/region-service';
import { forkJoin } from 'rxjs';
import { UtilsHelper } from '@/core/helpers/utils.hlper';
import { CrmDataSource } from '@/core/enums/crmDataSource';
import { EMPLOYEE_SERVICE } from '@/core/services/crm/employee-service';
import { CrmStatus } from '@/core/enums/crmStatus';
import { CRMRECORD_SERVICE } from '@/core/services/crm/crmrecord-service';
import { ConfirmationService } from 'primeng/api';
import { CrmRecordModel } from '@/core/models/crm/crmrecord.model';
import { AUTH_SERVICE } from '@/core/services/admin/auth-token';
import { CRMRECORDACTION_SERVICE } from '@/core/services/crm/crmrecordaction-service';
import { CrmRecordActionModel } from '@/core/models/crm/crmrecordaction.model';
import { CrmActionType } from '@/core/enums/crmActionType';

@Component({
    selector: 'app-crm-layout',
    standalone: true,
    imports: [RouterModule, SharedComponentModule],
    templateUrl: './crm.component.html'
})
export class CrmComponent {
    authService = inject(AUTH_SERVICE);
    crmRecordService = inject(CRMRECORD_SERVICE);
    crmRecordActionService = inject(CRMRECORDACTION_SERVICE);
    regionService = inject(REGION_SERVICE);
    employeeService = inject(EMPLOYEE_SERVICE);
    utilsHelper = inject(UtilsHelper);

    CrmActionType = CrmActionType;

    constructor(private router: Router, private fb: FormBuilder, private confirmationService: ConfirmationService) { }

    tabs = [
        { route: '/crm/tasks', label: 'crm.tabs.myTasks', icon: 'pi pi-check-square' },
        { route: '/crm/my-data', label: 'crm.tabs.myData', icon: 'pi pi-database' },
        { route: '/crm/transactions', label: 'crm.tabs.myTransactions', icon: 'pi pi-briefcase' },
        { route: '/crm/field-special', label: 'crm.tabs.fieldSpecial', icon: 'pi pi-map-marker' },
        { route: '/crm/reports', label: 'crm.tabs.reports', icon: 'pi pi-chart-bar' },
        { route: '/crm/settings', label: 'crm.tabs.settings', icon: 'pi pi-cog' }
    ];

    activeTab: string = '';
    displayModal: boolean = false;
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

    ngOnInit(): void {
        this.router.events.subscribe(() => {
            this.setActiveTab();
        });

        this.getOptions();

        this.setActiveTab();
        this.initForm();
        this.initActionForm();
        this.initSmsForm();

        this.currentUser = this.authService.getUser()?.Name || '';
    }

    get currentRoute(): string {
        return location.pathname;
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

        this.pageForm.get('phone')?.setValue('(537) 026 01 30');

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

    setActiveTab(): void {
        const currentRoute = this.router.url;
        this.activeTab = this.tabs.find(tab => currentRoute.startsWith(tab.route))?.route || '';
    }

    navigate(path: string) {
        this.router.navigate(['/crm', path]);
    }

    isActive(path: string): boolean {
        return this.router.url.includes(path);
    }

    openModal() {
        this.displayModal = true;
    }

    closeModal() {
        this.displayModal = false;
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
                console.log(this.actionForm.invalid, 'invalid mi')
                // this.actionForm.disable();
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

    isFieldInvalid(controlName: string): boolean {
        const control = this.pageForm.get(controlName);
        return control?.invalid && control?.touched ? true : false;
    }
}
