import { Injectable, inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  ResolveFn,
  Router,
} from '@angular/router';
import { Observable, catchError, mergeMap, of } from 'rxjs';
import { PaymentService } from '../services/payment.service';
import { MessageService } from 'primeng/api';
@Injectable({
  providedIn: 'root',
})
class PaymentSuccessService {
  constructor(
    private readonly _paymentService: PaymentService,
    private readonly _messageService: MessageService,
    private readonly _router: Router
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    _: RouterStateSnapshot
  ): Observable<boolean> {
    let url = '';
    const queryParam = route.queryParamMap;
    if (queryParam.keys.length > 0) {
      url +=
        '?' +
        queryParam.keys
          .map((key) =>
            queryParam
              .getAll(key)
              .map((value) => key + '=' + value)
              .join('&')
          )
          .join('&');
    }

    return this._paymentService.successPayPalPayment(url).pipe(
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

export const paymentSuccessPayPalResolver: ResolveFn<boolean> = (
  route,
  state
) => {
  return inject(PaymentSuccessService).resolve(route, state);
};
