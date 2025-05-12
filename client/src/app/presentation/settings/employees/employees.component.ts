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
import { EmployeeRole } from '@/core/enums/employeeRole';
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
      phone3: [''],
  
      email: ['', [Validators.required, Validators.email]],
      address: [''],
  
      branchId: [null], // İsteğe bağlı
  
      isActive: [true], // Varsayılan true olabilir
  
      salaryType: [null, Validators.required],
      salaryAmount: [null],
      extraPayment: [null],
      salaryNote: ['']
    });
  }  

   getOptions() {
      forkJoin([
        this.enumToSelectOptionsAsync(EmployeeRole, 'EmployeeRole'),
        this.enumToSelectOptionsAsync(SalaryType, 'SalaryType'),
      ]).subscribe(([employeeRoles, salaryTypes]) => {
        this.employeeRoleOptions = employeeRoles;
        this.salaryTypeOptions = salaryTypes;
      });
    }
}

