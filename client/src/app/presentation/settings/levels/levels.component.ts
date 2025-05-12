import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { BRANCH_SERVICE } from '@/core/services/crm/branch-service';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { LevelModel } from '@/core/models/crm/level.model';
import { LEVEL_SERVICE, ILevelService } from '@/core/services/crm/level-service';

@Component({
  selector: 'app-levels',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './levels.component.html'
})
export class LevelsComponent extends AppBaseComponent<LevelModel, ILevelService> {
  branchService = inject(BRANCH_SERVICE);

  branchOptions: SelectListItem[] = [];

  constructor(private fb: FormBuilder) {
    super(LEVEL_SERVICE);
    this.pageTitle = 'Seviye';
  }

  override ngOnInit() {
    super.ngOnInit();
    this.getBranchList();
    this.initForm();
  }

  initForm(): void {
    this.pageForm = this.fb.group({
      id: [null],
      name: ['', Validators.required],
      definition: ['']
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

