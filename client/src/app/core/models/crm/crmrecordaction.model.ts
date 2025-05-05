import { CrmActionType } from "@/core/enums/crmActionType";

export interface CrmRecordActionModel {
  id: string;
  crmRecordId: string;
  employeeId: string;
  employeeName: string;
  date: Date; 
  actionType: CrmActionType;
  targetDate: Date;
  description: string;
}
