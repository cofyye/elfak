import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { IDataAcceptResponse } from '../../../shared/interfaces/response.interface';
import { IUserProfile } from '../interface/profile-display.interface';

@Injectable({
  providedIn: 'root',
})
export class UserProfileService {
  constructor(private http: HttpClient) {}

  getUserProfile(
    userId: string
  ): Observable<IDataAcceptResponse<IUserProfile>> {
    return this.http.get<IDataAcceptResponse<IUserProfile>>(
      `${environment.API_URL}/users/profile/info/${userId}`
    );
  }
}
