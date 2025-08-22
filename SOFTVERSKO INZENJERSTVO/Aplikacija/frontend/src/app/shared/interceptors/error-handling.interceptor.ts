import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { IAcceptResponse } from '../interfaces/response.interface';
import { MessageService } from 'primeng/api';

@Injectable()
export class ErrorHandlingInterceptor implements HttpInterceptor {
  constructor(private readonly _messageService: MessageService) {}

  intercept(
    req: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(req).pipe(
      catchError((err: HttpErrorResponse) => {
        const error: IAcceptResponse = err.error as IAcceptResponse;

        if (error?.success === false) {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: error?.message,
          });
        } else {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: 'Dogodila se greska. Prijavite ovo administratoru.',
          });
        }

        return throwError(() => err);
      })
    );
  }
}
