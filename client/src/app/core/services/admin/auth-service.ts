import { LoginResponse, User } from '../../models/admin/user';
import { LoginRequest } from '../../models/admin/login-request';
import { SelectListItem } from '../../models/admin/select-list-item';
import { MenuItemModel } from '../../models/admin/menu-item';

export interface IAuthService {
  login(request: LoginRequest): Promise<void>;
  getRoleList(payload: { appuserRef: number, firmRef: number }): Promise<SelectListItem[]>;
  logout(): void;
  isAuthenticated(): boolean;
  getUser(): User | null;
  getToken(): string | null;
  getRole(): number | null;
  isTeacher(): Promise<boolean>;
}


