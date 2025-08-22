import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '../../app.state';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { getUser } from '../../features/auth/store/auth.selectors';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private store: Store<AppState>, private http: HttpClient) {}

  isAuthenticated(): Observable<boolean> {
    return this.store.select(getUser).pipe(map((user) => user.loggedIn));
  }

  logout(): Observable<any> {
    console.log('Izlogovan');
    return this.http.post(`${environment.API_URL}/auth/signout`, {});
  }
}
