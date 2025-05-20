import { StudentType } from "@/core/enums/studentType";
import { BranchModel } from "./branch.model";
import { CrmRecordModel } from "./crmrecord.model";
import { AppUserModel } from "./appuser.model";

export interface StudentModel {
  id: string;
  firstName: string;
  lastName: string;
  identityNumber: string;
  birthDate: Date;
  phone: string;
  secondPhone: string;
  email: string;
  studentType: StudentType;
  address: string;
  cityId: number;
  districtId: number;

  parentFirstName?: string;
  parentLastName?: string;
  parentPhone?: string;
  parentIdentityNumber?: string;
  parentBirthDate?: Date;

  branchId: string;
  branch?: BranchModel;

  crmRecordId?: string;
  crmRecord?: CrmRecordModel;

  userId: string;
  user?: AppUserModel;
}
