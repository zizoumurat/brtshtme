import { LoginResponse, User } from '../../models/admin/user';
import { LoginRequest } from '../../models/admin/login-request';
import { SelectListItem } from '../../models/admin/select-list-item';
import { MenuItemModel } from '../../models/admin/menu-item';

export interface IAuthService {
  login(request: LoginRequest): Promise<void>;
  getFirmList(appuserRef: number): Promise<SelectListItem[]>;
  getRoleList(payload: { appuserRef: number, firmRef: number }): Promise<SelectListItem[]>;
  getMenuList(): Promise<MenuItemModel[]>
  logout(): void;
  isAuthenticated(): boolean;
  isFirmAndRoleSelected(): boolean;
  getUser(): User | null;
  getToken(): string | null;
  setFirmAndRole(firmRef: number, roleRef: number): void;
  getFirm(): number | null;
  getRole(): number | null;
}


