import { ParticipantType } from "@/core/enums/participantType.enum";

export interface IncentiveSettingModel {
  id: string;
  participantType: ParticipantType;
  minAmount: number;
  maxAmount: number;
  salesCommission: number;
  collectionCommission: number;
  bonus: number;
  note: string;
}


