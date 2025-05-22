import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { LoginResponse, User } from '../../../core/models/admin/user';
import { LoginRequest } from '../../../core/models/admin/login-request';
import { IAuthService } from '../../../core/services/admin/auth-service';
import { BASE_URL } from '../../../environments/environment';
import { Router } from '@angular/router';
import { SelectListItem } from '../../../core/models/admin/select-list-item';

@Injectable({
  providedIn: 'root',
})
export class AuthService implements IAuthService {
  private router = inject(Router);
  private apiUrl = `${BASE_URL}/auth`;
  private token = 'auth_token';

  private userRoleList: any[] | undefined = undefined;

  constructor(private http: HttpClient) { }

  async getRoleList(): Promise<any[]> {
    try {
      const response = await firstValueFrom(
        this.http.post<any[]>(`${this.apiUrl}/GetRoleList`, {})
      );

      this.userRoleList = response;

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
            return throwError(() => error.error?.errorMessages || 'An unknown error occurred.');
          })
        )
      );

      localStorage.setItem(this.token, JSON.stringify(response));
      const decodedToken = this.decodeJWT(response.token);
      localStorage.setItem('user', JSON.stringify(decodedToken));

      this.getRoleList();

      this.router.navigate(['']);
    } catch (error: any) {
      throw error || 'An unknown error occurred.';
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

  async isTeacher(): Promise<boolean> {
    return this.userRoleList?.includes('Teacher') ?? false;
  }
}
