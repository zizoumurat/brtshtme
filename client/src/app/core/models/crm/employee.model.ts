import { EmployeeRole } from "@/core/enums/employeeRole";
import { SalaryType } from "@/core/enums/salaryType";

export interface EmployeeModel extends HasId {
  firstName: string;
  lastName: string;
  identityNumber: string;
  role: EmployeeRole;
  startDate: string;
  birthDate: string;

  phone1: string;
  phone2: string;
  phone3: string;

  email: string;
  address: string;

  branchId?: string;
  isActive: boolean;

  salaryType: SalaryType;
  salaryAmount?: number;
  extraPayment?: number;
  salaryNote: string;
}

export interface UnassignedEmployeeModel {
  id: string;
  name: string;
  role: EmployeeRole;
}