import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { BRANCH_SERVICE } from '@/core/services/crm/branch-service';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { ClassRoomModel } from '@/core/models/crm/classRoom.model';
import { CLASSROOM_SERVICE, IClassRoomService } from '@/core/services/crm/classroom-service';

@Component({
  selector: 'app-classrooms',
  standalone: true,
  imports: [
    DefaultTableOptionsDirective,
    SharedComponentModule
  ],
  templateUrl: './classrooms.component.html'
})
export class ClassRoomsComponent extends AppBaseComponent<ClassRoomModel, IClassRoomService> {
  branchService = inject(BRANCH_SERVICE);

  branchOptions: SelectListItem[] = [];

  constructor(private fb: FormBuilder) {
    super(CLASSROOM_SERVICE);
    this.pageTitle = 'Derslik';
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
      name: ['', Validators.required],
      capacity: ['', Validators.required]
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

