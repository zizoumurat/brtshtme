export interface BranchModel extends HasId {
    name: string;
    description: string;
    address: string;
    phoneNumber: string;
    email: string;
    lessonDurationInMinutes: number;
    breakDurationInMinutes: number;
    levelDurationInHours: number;
}

