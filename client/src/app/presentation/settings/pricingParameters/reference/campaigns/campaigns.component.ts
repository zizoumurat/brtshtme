import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { BRANCH_SERVICE } from '@/core/services/crm/branch-service';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { CampaignModel } from '@/core/models/crm/campaign.model';
import { CAMPAIGN_SERVICE, ICampaignService } from '@/core/services/crm/campaign-service';

@Component({
  selector: 'app-campaigns',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './campaigns.component.html'
})
export class CampaignsComponent extends AppBaseComponent<CampaignModel, ICampaignService> {
  branchService = inject(BRANCH_SERVICE);

  branchOptions: SelectListItem[] = [];

  constructor(private fb: FormBuilder) {
    super(CAMPAIGN_SERVICE);
    this.pageTitle = 'Kampanya';
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
      definition: ['', Validators.required],
      discountRate: ['', Validators.required],
      isActive: [true],
    });
  }

  override resetForm(): void {
    super.resetForm();
    this.pageForm.get('isActive')?.setValue(true);
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

