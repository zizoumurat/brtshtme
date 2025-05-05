import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { BRANCH_SERVICE, IBranchService } from '@/core/services/crm/branch-service';
import { BranchModel } from '@/core/models/crm/branch.model';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';

@Component({
  selector: 'app-branches',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule,
  ],
  templateUrl: './branches.component.html'
})
export class BranchesComponent extends AppBaseComponent<BranchModel, IBranchService> {

  constructor(private fb: FormBuilder) {
    super(BRANCH_SERVICE);
    this.pageTitle = 'Şube';
  }

  override ngOnInit() {
    super.ngOnInit();
    this.initForm();
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

  override deleteRecord(recordId: number): void {
    super.deleteRecord(recordId);
    this.recordService.clearBranchCache();
  }

  override saveRecord(): Promise<void> {
    return super.saveRecord().then(() => {
      this.recordService.clearBranchCache();
    });
  }
}

