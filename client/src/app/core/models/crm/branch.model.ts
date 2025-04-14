export interface BranchModel extends HasId {
    name: string;
    description: string;
    address: string;
    phoneNumber: string;
    email: string;
    isActive: boolean;
}