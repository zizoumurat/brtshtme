import { CrmDataSource } from "@/core/enums/crmDataSource";
import { CrmStatus } from "@/core/enums/crmStatus";

export interface CrmRecordModel {
    id: string;
    phone: string;
    firstName: string;
    lastName: string;
    secondPhone: string;
    email: string;
    dataProviderId: string;
    salesRepresentativeId: string;
    regionId: string;
    dataSource: CrmDataSource;
    excludeFromCommission: boolean;
    description: string;
    date: string;
    status: CrmStatus;
    branchId: string;
  }
  