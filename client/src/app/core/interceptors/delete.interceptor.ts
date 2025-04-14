import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { ConfirmationService, MessageService } from "primeng/api";
import { Observable } from "rxjs";
import { ConfirmHelper } from "../helpers/confirm.helper";

export const deleteInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.method === 'DELETE') {
    const confirmationService = inject(ConfirmationService);
    const messageService = inject(MessageService);
    const notificationHelper = new ConfirmHelper(confirmationService);

    return new Observable(observer => {
      notificationHelper.confirmDelete().then(confirmed => {
        if (confirmed) {
          next(req).subscribe({
            next: (event) => observer.next(event),
            error: (error) => observer.error(error),
            complete: () => observer.complete(),
          });
        } else {
          observer.complete();
        }
      }).catch(error => {
        observer.error(error); // Handle any errors from the confirmation process
      });
    });
  } else {
    return next(req);
  }
};
