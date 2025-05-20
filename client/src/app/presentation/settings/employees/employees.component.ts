import { Component, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DefaultTableOptionsDirective } from '@/core/directives/table-options.directive';
import { SharedComponentModule } from '@/presentation/admin/shared/shared-components.module';
import { PaginationFilterModel } from '@/core/models/admin/paginationFilterModel';
import { EMPLOYEE_SERVICE, IEmployeeService } from '@/core/services/crm/employee-service';
import { TableLazyLoadEvent } from 'primeng/table';
import { EmployeeModel } from '@/core/models/crm/employee.model';
import { AppBaseComponent } from '@/presentation/admin/shared/base.component';
import { SelectListItem } from '@/core/models/select-list-item.model';
import { forkJoin } from 'rxjs';
import { EmployeeRole, UserRole } from '@/core/enums/employeeRole';
import { DefaultSelectOptionDirective } from '@/core/directives/default-select-options.directive';
import { SalaryType } from '@/core/enums/salaryType';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [
    SharedComponentModule,
    DefaultSelectOptionDirective
  ],
  templateUrl: './employees.component.html'
})
export class EmployeesComponent extends AppBaseComponent<EmployeeModel, IEmployeeService> {
  EmployeeRole = EmployeeRole;
  UserRole = UserRole

  employeeRoleOptions: SelectListItem[] = [];
  salaryTypeOptions: SelectListItem[] = [];

  constructor(private fb: FormBuilder) {
    super(EMPLOYEE_SERVICE);
    this.pageTitle = 'Personel';
  }

  override ngOnInit() {
    super.ngOnInit();
    this.getOptions();
    this.initForm();
  }

  get isTeacher(): boolean {
    return this.pageForm.get('role')?.value === EmployeeRole.Teacher;
  }

  get isMonthlySalary(): boolean {
    return this.pageForm.get('salaryType')?.value === SalaryType.Monthly;
  }

  get isHourlySalary(): boolean {
    return this.pageForm.get('salaryType')?.value === SalaryType.Hourly;
  }

  get isUndefinedSalary(): boolean {
    return this.pageForm.get('salaryType')?.value === SalaryType.Undefined;
  }

  get showSalaryFields(): boolean {
    return this.pageForm.get('salaryType')?.value != SalaryType.Undefined;
  }

  initForm(): void {
    this.pageForm = this.fb.group({
      id: [null],

      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      identityNumber: ['', Validators.required],
      role: [null, Validators.required],

      startDate: [null, Validators.required],
      birthDate: [null, Validators.required],

      phone1: ['', Validators.required],
      phone2: [''],

      email: ['', [Validators.required, Validators.email]],
      address: [''],

      branchId: [null, Validators.required],

      isActive: [true],

      salaryType: [null, Validators.required],
      salaryAmount: [null],
      extraPayment: [null],
      salaryNote: [''],
      applyOvertime: [false],
      overtimeQuota: [null],
      overtimeHourlyRate: [null],
      specialLessonHourlyRate: [null],
    });

    this.pageForm.get('role')?.valueChanges.subscribe(role => {
      const isTeacher = role === EmployeeRole.Teacher;

      if (!isTeacher) {
        this.pageForm.patchValue({
          specialLessonHourlyRate: null,
          applyOvertime: false,
          overtimeQuota: null,
          overtimeHourlyRate: null,
        });
      }
    });

    this.pageForm.get('salaryType')?.valueChanges.subscribe(salaryType => {
      const isMonthly = salaryType === SalaryType.Monthly;
      const isTeacher = this.pageForm.get('role')?.value === EmployeeRole.Teacher;

      if (!isMonthly || !isTeacher) {
        this.pageForm.patchValue({
          applyOvertime: false,
          specialLessonHourlyRate: null,
          overtimeQuota: null,
          overtimeHourlyRate: null,
        });
      }
    });

    this.pageForm.get('applyOvertime')?.valueChanges.subscribe(applyOvertime => {
      if (!applyOvertime) {
        this.pageForm.patchValue({
          overtimeQuota: null,
          overtimeHourlyRate: null,
        });
      }
    });
  }



  override openModal(): void {
    super.openModal();
    this.pageForm.get('salaryType')?.setValue(SalaryType.Undefined);
  }

  getOptions() {
    forkJoin([
      this.enumToSelectOptionsAsync(EmployeeRole, 'EmployeeRole'),
      this.enumToSelectOptionsAsync(SalaryType, 'SalaryType'),
    ]).subscribe(([employeeRoles, salaryTypes]) => {
      this.employeeRoleOptions = employeeRoles.sort((a, b) =>
        a.name.localeCompare(b.name)
      );
      this.salaryTypeOptions = salaryTypes;
    });
  }
}

