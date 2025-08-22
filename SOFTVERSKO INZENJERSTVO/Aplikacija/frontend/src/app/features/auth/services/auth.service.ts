import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  IAcceptResponse,
  IDataAcceptResponse,
} from '../../../shared/interfaces/response.interface';
import { environment } from '../../../../environments/environment';
import {
  ITokenValidation,
  IEmail,
  ISignIn,
  ISignInData,
  ISignUp,
  IResetPassword,
} from '../interfaces/auth.interface';

@Injectable()
export class AuthService {
  isAuthenticated(): Observable<boolean> | undefined {
    throw new Error('Method not implemented.');
  }
  constructor(private readonly _httpClient: HttpClient) {}

  public register(user: ISignUp): Observable<IAcceptResponse> {
    const formData: FormData = new FormData();

    formData.append('firstName', user.firstName);
    formData.append('lastName', user.lastName);
    formData.append('username', user.username);
    formData.append('email', user.email);
    formData.append('password', user.password);
    formData.append('isExpert', String(user.isExpert));
    formData.append('avatar', user.avatar);

    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/auth/signup`,
      formData
    );
  }

  public login(data: ISignIn): Observable<IDataAcceptResponse<ISignInData>> {
    return this._httpClient.post<IDataAcceptResponse<ISignInData>>(
      `${environment.API_URL}/auth/signin`,
      data
    );
  }

  public requestCode(data: IEmail): Observable<IAcceptResponse> {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/auth/verification/resend/${data.email}`,
      {}
    );
  }

  public confirmEmail(data: ITokenValidation): Observable<IAcceptResponse> {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/auth/verification/confirm?email=${data.email}&token=${data.token}`,
      {}
    );
  }

  public resetPassword(data: ITokenValidation): Observable<IAcceptResponse> {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/auth/password/reset?email=${data.email}&token=${data.token}`,
      {}
    );
  }

  public forgotPassword(data: IEmail): Observable<IAcceptResponse> {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/auth/password/forgot/${data.email}`,
      {}
    );
  }

  public changePassword(data: IResetPassword): Observable<IAcceptResponse> {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/auth/password/change`,
      data
    );
  }
}
