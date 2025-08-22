import { Injectable, inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from '../../app.state';
import { Observable, map, take } from 'rxjs';
import { getUser } from '../../features/auth/store/auth.selectors';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root',
})
class NotAuthenticatedService {
  constructor(
    private readonly _messageService: MessageService,
    private readonly _router: Router,
    private readonly _store: Store<AppState>
  ) {}

  canActivate(
    _: ActivatedRouteSnapshot,
    __: RouterStateSnapshot
  ): Observable<boolean> {
    return this._store.select(getUser).pipe(
      take(1),
      map((user) => {
        if (user.loggedIn) {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: 'Zao nam je, nemate dozvolu da pristupite ovoj stranici.',
          });

          this._router.navigate(['/']);

          return false;
        }

        return true;
      })
    );
  }
}

export const notAuthenticatedGuard: CanActivateFn = (
  route,
  state
): Observable<boolean> => {
  return inject(NotAuthenticatedService).canActivate(route, state);
};
