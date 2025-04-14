import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { IAuthService } from '../services/admin/auth-service';
import { AUTH_SERVICE } from '../services/admin/auth-token';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject<IAuthService>(AUTH_SERVICE);

  const token = authService.getToken();

  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  return next(req);
};
