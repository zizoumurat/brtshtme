import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { COURSESALESETTING_SERVICE } from '@/core/services/crm/courseSaleSetting-service';
import { InstallmentSettingModel } from '@/core/models/crm/installmentSetting.model';
import { IInstallmentSettingService, INSTALLMENTSETTING_SERVICE } from '@/core/services/crm/installmentSetting-service';
import { BRANCH_SERVICE } from '@/core/services/crm/branch-service';

@Component({
  selector: 'app-installment-settings',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './installmentSettings.component.html'
})
export class InstallmentSettingsComponent extends AppBaseComponent<InstallmentSettingModel, IInstallmentSettingService> {
  branchService = inject(BRANCH_SERVICE);

  branchOptions: SelectListItem[] = [];

  constructor(private fb: FormBuilder) {
    super(INSTALLMENTSETTING_SERVICE);
    this.pageTitle = 'Eğitim Süresi / Taksit Sayısı';
  }

  override ngOnInit() {
    super.ngOnInit();
    this.getBranchList();
    this.initForm();
  }

  initForm(): void {
    this.pageForm = this.fb.group({
      id: [null],
      branchId: ['', Validators.required],
      level: [null, Validators.required],
      maxBond: [null, Validators.required],
      maxCardInstallment: ['', Validators.required],
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

