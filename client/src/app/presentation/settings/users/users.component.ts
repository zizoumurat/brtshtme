import { Component, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { PaginationFilterModel } from '@/core/models/admin/paginationFilterModel';
import { EMPLOYEE_SERVICE, IEmployeeService } from '@/core/services/crm/employee-service';
import { TableLazyLoadEvent } from 'primeng/table';
import { EmployeeModel, UnassignedEmployeeModel } from '@/core/models/crm/employee.model';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { forkJoin } from 'rxjs';
import { EmployeeRole, UserRole } from '@/core/enums/employeeRole';
import { DefaultSelectOptionDirective } from '@/core/directives/default-select-options.directive';
import { SalaryType } from '@/core/enums/salaryType';
import { AppUserModel } from '@/core/models/crm/appuser.model';
import { APPUSER_SERVICE, IAppUserService } from '@/core/services/crm/appuser.service';
import { passwordPolicyValidator } from '@/core/helpers/passwordValidator';
import { passwordMatchValidator } from '@/core/helpers/passwordMatchValidator';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [
    SharedComponentModule,
    DefaultSelectOptionDirective
  ],
  templateUrl: './users.component.html'
})
export class UsersComponent extends AppBaseComponent<AppUserModel, IAppUserService> {
  EmployeeRole = EmployeeRole;
  UserRole = UserRole;

  salaryTypeOptions: SelectListItem[] = [];
  employeeOptions: UnassignedEmployeeModel[] = [];
  userRoleOptions: SelectListItem[] = [];
  userRoleList: SelectListItem[] = [];

  constructor(private fb: FormBuilder) {
    super(APPUSER_SERVICE);
    this.pageTitle = 'Kullanıcı';
  }

  override ngOnInit() {
    super.ngOnInit();
    this.getOptions();
    this.initForm();
  }

  async initForm(): Promise<void> {
    this.pageForm = this.fb.group({
      branchId: ['', Validators.required],
      employeeId: ['', Validators.required],
      roles: [[], Validators.required],
      password: ['', [Validators.required, passwordPolicyValidator]],
      rePassword: ['', Validators.required],
    }, { validators: passwordMatchValidator });

    this.pageForm.get('branchId')?.valueChanges.subscribe(async id => {
      if (id) {
        this.employeeOptions = await this.recordService.getUnassignedEmployees(id);
      }
    });

    this.pageForm.get('employeeId')?.valueChanges.subscribe(async id => {
      if (id) {
        const selectedEmployee = this.employeeOptions.find(e => e.id === id);
        const rolesControl = this.pageForm.get('roles');

        if (selectedEmployee && selectedEmployee.role == EmployeeRole.Teacher) {
          this.userRoleOptions = this.userRoleList.filter(x => x.id == UserRole.Teacher);
          rolesControl?.setValue([UserRole.Teacher]);
        }
        else {
          this.userRoleOptions = this.userRoleList.filter(x => x.id != UserRole.Teacher);
          rolesControl?.setValue([]);
        } 
      }
    });
  }


  getOptions() {
    forkJoin([
      this.enumToSelectOptionsAsync(UserRole, 'UserRole'),
      this.enumToSelectOptionsAsync(SalaryType, 'SalaryType'),
    ]).subscribe(([userRoleOptions, salaryTypes]) => {
      this.userRoleList = userRoleOptions;
      this.salaryTypeOptions = salaryTypes;
    });
  }
}

