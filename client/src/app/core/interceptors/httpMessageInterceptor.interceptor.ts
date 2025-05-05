import { HttpInterceptorFn, HttpErrorResponse, HttpResponse } from "@angular/common/http";
import { inject } from "@angular/core";
import { Router } from "@angular/router";
import { ConfirmationService, MessageService } from "primeng/api";
import { catchError, of, tap, throwError } from "rxjs";
import { ConfirmHelper } from "../helpers/confirm.helper";

export const httpMessageInterceptor: HttpInterceptorFn = (req, next) => {
  const excludedUrlList = ['/auth'];

  if (excludedUrlList.some(url => req.url.includes(url))) {
    return next(req);
  }

  const confirmationService = inject(ConfirmationService);
  const messageService = inject(MessageService);
  const router = inject(Router);
  const notificationService = new ConfirmHelper(confirmationService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      console.log('error', error);
      if (error.status === 401) {
        localStorage.clear();
        router.navigate(['/auth/login']);
      }
      let errorMessage = '';
      if (error.error instanceof ErrorEvent) {
        errorMessage = `Hata: ${error.error.message}`;
      }
      else if (error.error.ErrorMessages) {
        errorMessage = error.error.ErrorMessages.map((e: any) => `errors.${e}`).join('<br>');
      }
      else {
        errorMessage = error?.error?.Message || 'Bilinmeyen bir hata oluştu.';
      }

      messageService.add({
        severity: 'error',
        summary: 'Hata',
        detail: errorMessage
      });

      return throwError(() => error);
    }),
    tap((response) => {
      if (response instanceof HttpResponse && response.body) {
        const body = response.body as { message?: string; isSuccessful?: boolean };

        // Başarılı mesajı sadece POST veya PUT işlemlerinde gösterelim
        if (req.method === 'POST' || req.method === 'PUT' || req.method === 'DELETE') {
          if (body.message) {
            messageService.add({
              severity: "success",
              summary: "Başarılı",
              detail: `results.${body.message}`,
            });
          }
        }
      }
    })
  );
};
