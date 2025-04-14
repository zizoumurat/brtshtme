export interface User {
  Ref: number;
  Email: string;
  FullName: string;
  ImgUrl: string;
}

export interface LoginResponse {
  token: string;
  refreshToken: string;
  refreshTokenExpires: Date;
}
