import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { BRANCH_SERVICE } from '@/core/services/crm/branch-service';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { CourseSaleSettingModel } from '@/core/models/crm/courseSaleSetting.model';
import { COURSESALESETTING_SERVICE, ICourseSaleSettingService } from '@/core/services/crm/courseSaleSetting-service';

@Component({
  selector: 'app-course-sale-settings',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './courseSaleSettings.component.html'
})
export class CourseSaleSettingsComponent extends AppBaseComponent<CourseSaleSettingModel, ICourseSaleSettingService> {
  branchService = inject(BRANCH_SERVICE);

  branchOptions: SelectListItem[] = [];

  constructor(private fb: FormBuilder) {
    super(COURSESALESETTING_SERVICE);
    this.pageTitle = 'Kur Satış Ayarı';
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
      minLevel: [null, Validators.required],
      maxLevel: [null, Validators.required],
      rate: ['', Validators.required],
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

