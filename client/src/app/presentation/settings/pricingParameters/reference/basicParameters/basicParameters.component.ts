import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { BRANCHPRICINGSETTINGS_SERVICE, IBranchPricingSettingsService } from '@/core/services/crm/branchPricingSettings-service';
import { BranchPricingSettingsModel } from '@/core/models/crm/branchPricingSettings.model';
import { BRANCH_SERVICE, IBranchService } from '@/core/services/crm/branch-service';

@Component({
  selector: 'app-basic-parameters',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './basicParameters.component.html'
})
export class BasicParametersComponent {
  pageForm!: FormGroup;
  initialData: BranchPricingSettingsModel | null = null;
  branchId = signal<string | null>(null);
  branchOptions: { id: string; name: string }[] = [];

  readonly service: IBranchPricingSettingsService = inject(BRANCHPRICINGSETTINGS_SERVICE);
  readonly branchService: IBranchService = inject(BRANCH_SERVICE);

  constructor(private fb: FormBuilder) { }

  async ngOnInit(): Promise<void> {
    this.initForm();

    await this.getUserBranchList();

    if (this.branchOptions.length > 0) {
      const firstBranchId = this.branchOptions[0].id;
      this.pageForm.patchValue({ branchId: firstBranchId }, { emitEvent: false });
      this.branchId.set(firstBranchId);
      await this.getBranchPricingSettings(firstBranchId);
    }
  }

  initForm(): void {
    this.pageForm = this.fb.group({
      id: [null],
      branchId: [null, Validators.required],
      hourlyRate: [null, Validators.required],
      discountForPrepayment: [null, Validators.required],
      cashPrepaymentDiscount: [null, Validators.required],
      creditCardInstallmentDiscount: [null, Validators.required],
      installmentRate: [null, Validators.required],
      collectionRateForBonus: [null, Validators.required],
    });

    this.pageForm.get('branchId')!.valueChanges.subscribe(async value => {
      this.branchId.set(value);
      this.pageForm.patchValue({
        id: null,
        hourlyRate: null,
        discountForPrepayment: null,
        cashPrepaymentDiscount: null,
        creditCardInstallmentDiscount: null,
        installmentRate: null,
        collectionRateForBonus: null
      }, { emitEvent: false });

      if (value) {
        await this.getBranchPricingSettings(value);
      }
    });
  }

  async getUserBranchList(): Promise<void> {
    this.branchOptions = await this.branchService.getUserBranchList();
  }

  async getBranchPricingSettings(branchId: string): Promise<void> {
    const settings = await this.service.getSettingsByBranchId(branchId);
    if (settings) {
      this.pageForm.patchValue(settings, { emitEvent: false });
    }
  }

  async submit() {
    if (this.pageForm.valid) {
      const formData = this.pageForm.value;

      await this.service.addOrUpdate(formData);

    } else {
      this.pageForm.markAllAsTouched();
    }
  }

  isFieldInvalid(controlName: string): boolean {
    const control = this.pageForm.get(controlName);
    return !!(control && control.invalid && control.touched);
  }
}
