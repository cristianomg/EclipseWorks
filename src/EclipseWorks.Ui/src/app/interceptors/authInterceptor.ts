import { inject } from "@angular/core";
import { UserService } from "../services/user.service";
import { HttpHandlerFn, HttpRequest } from "@angular/common/http";

export function authInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
    const user = inject(UserService).getUser();


    if (user){
        const newReq = req.clone({
            headers: req.headers.append('userId', user.id.toString()),
        });
        return next(newReq);
    }
    return next(req);
  }