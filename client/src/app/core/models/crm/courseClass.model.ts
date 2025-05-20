import { ClassType, EducationType } from "@/core/enums/educationType";
import { ScheduleType } from "@/core/enums/studentType";

export interface CourseClassModel {
  id: string;
  branchId: string;
  name: string;
  classType: ClassType;
  educationType: EducationType;
  scheduleType: ScheduleType;
  lessonScheduleDefinitionId: string;
  levelId: string;
  description: string;
  startDate: string;
  endDate: string;  
  capacity: number;
  unit: number;
  classroomId?: string | null;
  createdAt: string;
}
