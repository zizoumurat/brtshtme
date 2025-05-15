import {
    ChangeDetectorRef,
    Component,
    EventEmitter,
    inject,
    Input,
    Output
} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { forkJoin } from 'rxjs';

import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { DefaultSelectOptionDirective } from '@/core/directives/default-select-options.directive';

import { SelectListItem } from '@/core/models/select-list-item.model';
import { CrmRecordModel } from '@/core/models/crm/crmrecord.model';

import { CAMPAIGN_SERVICE } from '@/core/services/crm/campaign-service';
import { DISCOUNT_SERVICE } from '@/core/services/crm/discount-service';

import { UtilsHelper } from '@/core/helpers/utils.hlper';
import { StudentType } from '@/core/enums/studentType';
import { LOCATION_SERVICE } from '@/core/services/crm/locationService';
import { LESSONSCHEDULEDEFINITION_SERVICE } from '@/core/services/crm/lessonScheduleDefinition-service';
import { PaginationFilterModel } from '@/core/models/admin/paginationFilterModel';
import { LessonScheduleDefinitionModel } from '@/core/models/crm/lessonScheduleDefinition.model';
import { INSTALLMENTSETTING_SERVICE } from '@/core/services/crm/installmentSetting-service';
import { InstallmentSettingModel } from '@/core/models/crm/installmentSetting.model';
import { PaymentMethod } from '@/core/enums/paymentMethod';
import { SALES_SERVICE } from '@/core/services/crm/sales-service';
import { CalculatePaymentModel } from '@/core/models/calculatePayment.model';
import { BRANCH_SERVICE } from '@/core/services/crm/branch-service';
import { ContractType } from '@/core/enums/contractType';
import { EducationType } from '@/core/enums/educationType';
import { MessageService } from 'primeng/api';

@Component({
    selector: 'app-salesmodal',
    standalone: true,
    imports: [SharedComponentModule, DefaultSelectOptionDirective],
    templateUrl: './salesmodal.component.html'
})
export class SalesModalComponent {
    // Inputs & Outputs
    @Input() displayModal = false;
    @Input() crmRecord: CrmRecordModel | undefined;
    @Output() onCloseModal = new EventEmitter();

    activeStep = 2;

    // Services (DI via inject)
    locationService = inject(LOCATION_SERVICE);
    lessonSchedulaDefinitionService = inject(LESSONSCHEDULEDEFINITION_SERVICE);
    campaignService = inject(CAMPAIGN_SERVICE);
    discountService = inject(DISCOUNT_SERVICE);
    installmentSettingService = inject(INSTALLMENTSETTING_SERVICE);
    salesService = inject(SALES_SERVICE);
    branchService = inject(BRANCH_SERVICE);
    messageService = inject(MessageService);


    utilsHelper = inject(UtilsHelper);

    constructor(private fb: FormBuilder) { }

    // Forms
    salesForm!: FormGroup;

    totalAmount: number | undefined;
    installmentAmount: number | undefined;
    installments: any[] | undefined;

    // Select Options
    studentTypeOptions: SelectListItem[] = [];
    cityOptions: SelectListItem[] = [];
    districtOptions: SelectListItem[] = [];

    lessonScheduleOptions: LessonScheduleDefinitionModel[] = [];
    installmentSettingOptions: InstallmentSettingModel[] = [];
    educationDurationOptions: SelectListItem[] = [];
    campaignOptions: SelectListItem[] = [];
    discountOptions: SelectListItem[] = [];
    installmentCountOptions: SelectListItem[] = [];
    paymentMethodOptions: SelectListItem[] = [
        { id: PaymentMethod.Cash.toString(), name: 'Nakit' },
        { id: PaymentMethod.Note.toString(), name: 'Senet' },
        { id: PaymentMethod.CreditCard.toString(), name: 'Kredi Kartı' }
    ];

    lessonSchedule: LessonScheduleDefinitionModel | undefined;

    // Lifecycle
    ngOnInit() {
        this.getOptions();
        this.initSalesForm();
    }


    initSalesForm(): void {
        this.salesForm = this.fb.group({
            step1: this.fb.group({
                firstName: [{ value: null, disabled: true }, Validators.required],
                lastName: [{ value: null, disabled: true }, Validators.required],
                identityNumber: ['', [Validators.required, Validators.minLength(11)]],
                birthDate: [null, Validators.required],
                phone: [{ value: null, disabled: true }, Validators.required],
                secondPhone: [''],
                email: [{ value: null, disabled: true }, Validators.required],
                studentType: [null, Validators.required],
                address: [null, Validators.required],
                cityId: [34, Validators.required],
                districtId: [null, Validators.required],
                branchId: [null, Validators.required],
                signatory: ['student', Validators.required],
                parentFirstName: [''],
                parentLastName: [''],
                parentIdentityNumber: [''],
                parentBirthDate: [''],
                parentPhone: [''],
            }),
            step2: this.fb.group({
                contractType: [ContractType.General, Validators.required],
                educationType: [EducationType.InPerson, Validators.required],
                lessonScheduleId: [null, Validators.required],
                educationDuration: [null, Validators.required],
                campaignId: [null],
                installmentCount: [null, Validators.required],
                paymentMethod: [null, Validators.required],
                discountId: [null],
                downPayment: [null],
                deposit: [null],
                firstInstallmentDate: [null, Validators.required],
            }),
        });

        this.salesForm.get('step1')?.get('cityId')?.valueChanges.subscribe(id => {
            if (id) {
                this.getDistrictList(id);
            }
        });

        this.salesForm.get('step2')?.valueChanges.subscribe(values => {
            this.totalAmount = undefined;
            this.installmentAmount = undefined;
            this.installments = undefined;
        });

        this.salesForm.get('step1')?.get('signatory')?.valueChanges.subscribe(signatory => {

            const isStudent = signatory === 'student';
            const controlsToUpdate = [
                'parentFirstName',
                'parentLastName',
                'parentIdentityNumber',
                'parentBirthDate',
                'parentPhone'
            ];

            controlsToUpdate.forEach(controlName => {
                const control = this.salesForm.get('step1')?.get(controlName) as FormControl;

                if (isStudent) {
                    control.clearValidators();
                    control.updateValueAndValidity();
                } else {
                    control.setValidators([Validators.required]);
                    control.updateValueAndValidity();
                }
            });
        });

        this.salesForm.get('step1')?.patchValue(this.crmRecord);
        this.salesForm.get('step1')?.get('cityId')?.setValue('34');

        this.salesForm.get('step2')?.get('educationDuration')?.valueChanges.subscribe(_ => {
            this.getInstallmentCount();
        });

        this.salesForm.get('step2')?.get('installmentCount')?.valueChanges.subscribe(value => {
            this.depositAndDateControl();
        });

        this.salesForm.get('step2')?.get('paymentMethod')?.valueChanges.subscribe(value => {
            this.getInstallmentCount();
            this.depositAndDateControl();
        });

        this.salesForm.get('step2')?.get('lessonScheduleId')?.valueChanges.subscribe(id => {
            this.lessonSchedule = this.lessonScheduleOptions.find(x => x.id == id) || undefined;
        });

        this.salesForm.get('step2')?.get('paymentMethod')?.setValue(PaymentMethod.Cash.toString());

    }

    depositAndDateControl() {
        const paymentMethod = this.salesForm.get('step2')?.get('paymentMethod')?.value;
        const installmentCount = this.salesForm.get('step2')?.get('installmentCount')?.value;
        if (paymentMethod == PaymentMethod.Cash || installmentCount == 1) {
            this.salesForm.get('step2')?.get('deposit')?.setValue(null);
            this.salesForm.get('step2')?.get('deposit')?.disable();
            this.salesForm.get('step2')?.get('firstInstallmentDate')?.setValue(null);
            this.salesForm.get('step2')?.get('firstInstallmentDate')?.disable();
        }
        else {
            this.salesForm.get('step2')?.get('deposit')?.enable();
            this.salesForm.get('step2')?.get('firstInstallmentDate')?.enable();
        }
    }

    // API/Service Calls
    async getOptions() {
        this.getCityList();
        this.getLessonscheduleList();
        this.getCampaignList();
        this.getDiscountList();
        this.getInstallmentSettingList();

        forkJoin([
            this.utilsHelper.enumToSelectOptionsAsync(StudentType, 'StudentType'),
        ]).subscribe(([studentTypes]) => {
            this.studentTypeOptions = studentTypes;
        });
    }

    async getLessonscheduleList() {
        var paginationFilter = new PaginationFilterModel();
        paginationFilter.pageSize = 100;
        paginationFilter.sortByMultiName = ['schedule'];
        var result = await this.lessonSchedulaDefinitionService.getAll(paginationFilter);
        this.lessonScheduleOptions = result.items;
    }

    async getCampaignList() {
        var paginationFilter = new PaginationFilterModel();
        paginationFilter.pageSize = 100;
        paginationFilter.sortByMultiName = ['definition'];
        var result = await this.campaignService.getAll(paginationFilter, { 'isActive': true });
        this.campaignOptions = result.items.map(x => ({ name: x.definition, id: x.id } as SelectListItem));
    }

    async getDiscountList() {
        var paginationFilter = new PaginationFilterModel();
        paginationFilter.pageSize = 100;
        paginationFilter.sortByMultiName = ['definition'];
        var result = await this.discountService.getAll(paginationFilter, { 'isActive': true });
        this.discountOptions = result.items.map(x => ({ name: x.definition, id: x.id } as SelectListItem));
    }

    async getInstallmentSettingList() {
        var paginationFilter = new PaginationFilterModel();
        paginationFilter.pageSize = 100;
        paginationFilter.sortByMultiName = ['level'];
        var result = await this.installmentSettingService.getAll(paginationFilter, { 'branchId': this.crmRecord?.branchId });
        this.installmentSettingOptions = result.items;

        if (this.installmentSettingOptions) {
            const sortedOptions = this.installmentSettingOptions.slice().sort((a, b) => b.level - a.level);
            const highestLevelOption = sortedOptions[0]?.level | 0;

            this.educationDurationOptions = Array.from({ length: highestLevelOption }, (_, i) => ({
                id: String(i + 1),
                name: `${i + 1} Seviye`
            }));
        }


    }

    async getCityList() {
        this.cityOptions = await this.locationService.getCityList();
    }

    async getDistrictList(cityId: number) {
        this.districtOptions = await this.locationService.getDistrictList(cityId);
    }

    getInstallmentCount() {
        const educationDuration = this.salesForm.get('step2')?.get('educationDuration')?.value;
        const paymentMethod = this.salesForm.get('step2')?.get('paymentMethod')?.value;
        const control = this.salesForm.get('step2')?.get('installmentCount') as FormControl;

        control.setValue(null);
        this.installmentCountOptions = [];

        if (paymentMethod == PaymentMethod.Cash) {
            control.disable();

            return;
        }
        else {
            control.enable();
        }

        if (!educationDuration || !paymentMethod) return;

        const installmentSetting = this.installmentSettingOptions.find(x => x.level == educationDuration);

        if (!installmentSetting) {
            return;
        }

        let installmentCount = 0;

        installmentCount = paymentMethod == PaymentMethod.CreditCard ? installmentSetting.maxCardInstallment : installmentSetting.maxBond;

        this.installmentCountOptions = Array.from({ length: installmentCount }, (_, i) => ({
            id: String(i + 1),
            name: i === 0 ? 'Peşin' : `${i + 1} Taksit`
        }));
    }

    closeSalesModal() {
        this.onCloseModal.emit();
    }


    visibleChange(value: boolean) {
        if (!value) this.closeSalesModal();
    }

    visibleChangeSalesModal(value: boolean) {
        if (!value) this.closeSalesModal();
    }


    isFieldInvalidSales(controlName: string): boolean {
        const control = this.salesForm.get('step1')?.get(controlName) || this.salesForm.get('step2')?.get(controlName) || this.salesForm.get('step3')?.get(controlName);
        return control?.invalid && control?.touched ? true : false;
    }

    nextStep() {
        const stepForm = this.salesForm.get('step' + this.activeStep);

        if (stepForm?.valid) {
            if (this.activeStep == 2 && !this.totalAmount) {
                this.messageService.add({
                    severity: 'error',
                    summary: 'Hata',
                    detail: 'Hesaplama Yapmadınız'
                });
                return;
            }
            this.activeStep++;
        } else {
            stepForm?.markAllAsTouched();
        }
    }

    prevStep() {
        this.activeStep--;
    }

    async calculate() {

        if (this.salesForm.get('step2')?.invalid) {
            this.salesForm.get('step2')?.markAllAsTouched();

            return;
        }

        const data = {
            lessonScheduleId: this.salesForm.get('step2')?.get('lessonScheduleId')?.value,
            branchId: this.crmRecord?.branchId,
            levelCount: this.salesForm.get('step2')?.get('educationDuration')?.value,
            installmentCount: this.salesForm.get('step2')?.get('installmentCount')?.value,
            paymentMethod: Number(this.salesForm.get('step2')?.get('paymentMethod')?.value),
            campaignId: this.salesForm.get('step2')?.get('campaignId')?.value,
            discountId: this.salesForm.get('step2')?.get('discountId')?.value,
            downPayment: this.salesForm.get('step2')?.get('downPayment')?.value,
            firstInstallmentDate: this.salesForm.get('step2')?.get('firstInstallmentDate')?.value,
        } as CalculatePaymentModel;

        const response = await this.salesService.calculateSalesAmount(data);

        this.totalAmount = (response.totalAmount);
        this.installmentAmount = (response.financedAmount);
        this.installments = response.installments;
    }

    async completeStep() {

    }
}
