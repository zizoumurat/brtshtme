export interface AppUserModel extends HasId {
  firstName: string;
  lastName: string;
  branchName: string;
  email: string;
  roles: string;
}
