import { Component, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { PaginationFilterModel } from '@/core/models/admin/paginationFilterModel';
import { BRANCH_SERVICE, IBranchService } from '@/core/services/crm/branch-service';
import { TableLazyLoadEvent } from 'primeng/table';
import { BranchModel } from '@/core/models/crm/branch.model';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';

@Component({
  selector: 'app-branches',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './branches.component.html'
})
export class BranchesComponent extends AppBaseComponent<BranchModel, IBranchService> {

  constructor(private fb: FormBuilder) {
    super(BRANCH_SERVICE);
    this.setPageUrl('/etrainings/branchs');
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
    });
  }
}

