import { Injectable, inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
  ResolveFn,
} from '@angular/router';
import { Observable, catchError, mergeMap, of } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { MessageService } from 'primeng/api';
import { ITokenValidation } from '../interfaces/auth.interface';

@Injectable({
  providedIn: 'root',
})
class ResetPasswordService {
  constructor(
    private readonly _authService: AuthService,
    private readonly _messageService: MessageService,
    private readonly _router: Router
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    _: RouterStateSnapshot
  ): Observable<boolean> {
    const email = route.queryParamMap.get('email');
    const token = route.queryParamMap.get('token');

    const data: ITokenValidation = {
      email,
      token,
    };

    return this._authService.resetPassword(data).pipe(
      mergeMap((response) => {
        if (response.success) {
          return of(true);
        } else {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: response.message,
          });

          this._router.navigate(['/']);

          return of(false);
        }
      }),
      catchError(() => {
        this._router.navigate(['/']);
        return of(false);
      })
    );
  }
}

export const resetPasswordResolver: ResolveFn<boolean> = (route, state) => {
  return inject(ResetPasswordService).resolve(route, state);
};
