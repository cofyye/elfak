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
class IsExpertService {
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
        if (!user.isExpert) {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail:
              'Morate biti strucnjak kako biste pristupili ovoj stranici.',
          });

          this._router.navigate(['/']);

          return false;
        }

        return true;
      })
    );
  }
}

export const isExpertGuard: CanActivateFn = (
  route,
  state
): Observable<boolean> => {
  return inject(IsExpertService).canActivate(route, state);
};
