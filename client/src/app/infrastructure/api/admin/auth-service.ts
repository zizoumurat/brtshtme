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

  getRole(): number | null {
    return localStorage.getItem('selectedRoleRef') ? Number(localStorage.getItem('selectedRoleRef')) : null;
  }
}
