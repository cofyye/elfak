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
import { GradeService } from '../services/grade.service';

@Injectable({
  providedIn: 'root',
})
class GradeResolverService {
  constructor(
    private readonly _authService: AuthService,
    private readonly _messageService: MessageService,
    private readonly _gradeService: GradeService,
    private readonly _router: Router
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    _: RouterStateSnapshot
  ): Observable<boolean> {
    const adId = route.paramMap.get('adId');

    return this._gradeService.checkGradeAvailibility(adId ?? '').pipe(
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

export const canGradeResolver: ResolveFn<boolean> = (route, state) => {
  return inject(GradeResolverService).resolve(route, state);
};
