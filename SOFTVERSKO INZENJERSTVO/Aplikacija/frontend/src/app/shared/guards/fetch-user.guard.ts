import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable, catchError, of, switchMap, take } from 'rxjs';
import { AppState } from '../../app.state';
import { Store } from '@ngrx/store';
import { hideLoadingPage, showLoadingPage } from '../store/loading.actions';
import { IDataAcceptResponse } from '../interfaces/response.interface';
import { ILoginStatus } from '../interfaces/user.interface';
import { environment } from '../../../environments/environment';
import { saveUser } from '../../features/auth/store/auth.actions';
import { getUser } from '../../features/auth/store/auth.selectors';

@Injectable({
  providedIn: 'root',
})
class FetchGuardService {
  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _store: Store<AppState>
  ) {}

  canActivate(
    _: ActivatedRouteSnapshot,
    __: RouterStateSnapshot
  ): Observable<boolean> {
    return this._store.select(getUser).pipe(
      take(1),
      switchMap((user) => {
        if (user.fetched) {
          return of(true);
        } else {
          this._store.dispatch(showLoadingPage());

          return this._httpClient
            .post<IDataAcceptResponse<ILoginStatus>>(
              `${environment.API_URL}/users/check/session`,
              {}
            )
            .pipe(
              take(1),
              switchMap((response) => {
                if (response.success) {
                  this._store.dispatch(
                    saveUser({
                      user: {
                        id: response.data.id,
                        isExpert: response.data.isExpert,
                        role: response.data.role,
                        fetched: true,
                        loggedIn: true,
                      },
                    })
                  );
                } else {
                  this._store.dispatch(
                    saveUser({
                      user: {
                        id: '',
                        isExpert: false,
                        role: 'user',
                        fetched: true,
                        loggedIn: false,
                      },
                    })
                  );
                }

                this._store.dispatch(hideLoadingPage());

                return of(true);
              }),
              catchError(() => {
                this._store.dispatch(
                  saveUser({
                    user: {
                      id: '',
                      isExpert: false,
                      role: 'user',
                      fetched: true,
                      loggedIn: false,
                    },
                  })
                );
                this._store.dispatch(hideLoadingPage());

                return of(true);
              })
            );
        }
      })
    );
  }
}

export const fetchUserGuard: CanActivateFn = (
  next: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
): Observable<boolean> => {
  return inject(FetchGuardService).canActivate(next, state);
};
