import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { LoginResponse, User } from '../../../core/models/admin/user';
import { LoginRequest } from '../../../core/models/admin/login-request';
import { IAuthService } from '../../../core/services/admin/auth-service';
import { BASE_URL } from '../../../environments/environment';
import { JwtPayload, jwtDecode } from "jwt-decode";
import { Router } from '@angular/router';
import { SelectListItem } from '../../../core/models/admin/select-list-item';
import { MenuItemModel } from '../../../core/models/admin/menu-item';

@Injectable({
  providedIn: 'root',
})
export class AuthService implements IAuthService {
  private router = inject(Router);
  private apiUrl = `${BASE_URL}/auth`;
  private token = 'auth_token';

  constructor(private http: HttpClient) { }

  async getMenuList(): Promise<MenuItemModel[]> {
    try {
      var user = this.getUser();
      var firmRef = this.getFirm();
      var roleRef = this.getRole();
      if (!user || !firmRef || !roleRef) {
        throw new Error('User, firm or role is not selected.');
      }

      const payload = {
        appuserRef: user.Ref,
        firmRef,
        roleRef
      };

      const response = await firstValueFrom(
        this.http.post<{ data: MenuItemModel[] }>(`${this.apiUrl}/GetMenuList`, payload).pipe(
          map(res => res.data) // Veriyi doğru türde dönüyoruz
        )
      );

      return response;
    } catch (error) {
      console.error('Get menu list failed:', error);
      throw new Error('Failed to get menu list. Please try again.');
    }
  }

  async getRoleList(payload: { appuserRef: number; firmRef: number; }): Promise<SelectListItem[]> {
    try {
      const response = await firstValueFrom(
        this.http.post<{ data: any[] }>(`${this.apiUrl}/GetRoleList`, payload).pipe(
          map(res =>
            res.data.map(role => ({
              id: role.ref,
              name: role.name
            }))
          )
        )
      );

      return response;
    } catch (error) {
      console.error('Get role list failed:', error);
      throw new Error('Failed to get role list. Please try again.');
    }
  }

  async getFirmList(appuserRef: number): Promise<SelectListItem[]> {
    try {
      const response = await firstValueFrom(
        this.http.post<{ data: any[] }>(`${this.apiUrl}/GetFirmList`, { appuserRef }).pipe(
          map(res =>
            res.data.map(firm => ({
              id: firm.ref,
              name: firm.name
            }))
          ),
          catchError(error => {
            console.error('Get firm list failed:', error);
            return throwError(() => new Error('Failed to get firm list. Please try again.'));
          })
        )
      );

      return response;
    } catch (error) {
      throw new Error(error instanceof Error ? error.message : 'An unknown error occurred.');
    }
  }

  async login(request: LoginRequest): Promise<void> {
    try {
      const response = await firstValueFrom(
        this.http.post<{ data: LoginResponse }>(`${this.apiUrl}`, request).pipe(
          map(res => res.data),
          catchError(error => {
            console.error('Login failed:', error);
            return throwError(() => new Error('Login request failed. Please try again.'));
          })
        )
      );

      localStorage.setItem(this.token, JSON.stringify(response));
      const decodedToken = this.decodeJWT(response.token);
      localStorage.setItem('user', JSON.stringify(decodedToken));

      this.router.navigate(['']);
    } catch (error) {
      throw new Error(error instanceof Error ? error.message : 'An unknown error occurred.');
    }
  }

  decodeJWT(token: string): any {
    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload;
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/auth/login']);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem(this.token);
  }

//   isAuthenticated(): boolean {
//     this.token = localStorage.getItem("token") ?? "";
//     console.log("token:",this.token);
//     if(this.token === ""){
//       this.router.navigateByUrl("/auth/login");
//       return false;
//     }

//     const decode: JwtPayload | any = jwtDecode(this.token);
//     const exp = decode.exp;
//     const now = new Date().getTime() / 1000;
//     console.log("decode");

//     if(now > exp){
//       this.router.navigateByUrl("/auth/login");
//       return false;
//     }

//     return true;
// }

  isFirmAndRoleSelected(): boolean {
    return this.getFirm() !== null && this.getRole() !== null;
  }

  getToken(): string | null {
    const authData = localStorage.getItem(this.token);
    if (!authData) return null;

    try {
      const parsedData: LoginResponse = JSON.parse(authData);
      return parsedData.token ?? null;
    } catch {
      return null;
    }
  }

  getUser(): User | null {
    const authData = localStorage.getItem("user");
    if (!authData) return null;

    try {
      const parsedData: User = JSON.parse(authData);
      return parsedData ?? null;
    } catch {
      return null;
    }
  }

  setFirmAndRole(firmRef: number, roleRef: number): void {
    localStorage.setItem('selectedFirmRef', firmRef.toString());
    localStorage.setItem('selectedRoleRef', roleRef.toString());
  }

  getFirm(): number | null {
    return localStorage.getItem('selectedFirmRef') ? Number(localStorage.getItem('selectedFirmRef')) : null;
  }

  getRole(): number | null {
    return localStorage.getItem('selectedRoleRef') ? Number(localStorage.getItem('selectedRoleRef')) : null;
  }
}
