import { DayOfWeek } from "@/core/enums/dayOfWeek";
import { EducationType } from "@/core/enums/educationType";
import { ScheduleCategory } from "@/core/enums/scheduleCategory";
import { ScheduleType } from "@/core/enums/studentType";

export interface LessonScheduleDefinitionModel {
  id: string;
  studentType: ScheduleType;
  educationType: EducationType;
  schedule: string;
  scheduleCode: string;
  dayCount: number;
  dayHour: number;
  days: DayOfWeek[]; 
  startTime: string;
  endTime: string;
  discount: number;
  scheduleCategory: ScheduleCategory;
  branchId: string;
}
