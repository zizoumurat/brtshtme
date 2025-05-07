export interface User {
  Id: string;
  BranchId: string;
  Email: string;
  FullName: string;
  Name: string;
  ImgUrl: string;
}

export interface LoginResponse {
  token: string;
  refreshToken: string;
  refreshTokenExpires: Date;
}
