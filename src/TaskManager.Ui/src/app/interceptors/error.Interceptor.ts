import { inject } from "@angular/core";
import { HttpErrorResponse, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { ToastService } from "../services/toast.service";
import { catchError, throwError } from "rxjs";

export function errorInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
    const toastService = inject(ToastService)
    return next(req).pipe(
        catchError((err: HttpErrorResponse) => {
            toastService.showError(err.error.error)

            return throwError(() => err)
        })
    );
}