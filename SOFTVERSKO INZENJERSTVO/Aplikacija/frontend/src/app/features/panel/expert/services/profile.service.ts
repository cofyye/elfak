import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import {
  IAcceptResponse,
  IDataAcceptResponse,
} from '../../../../shared/interfaces/response.interface';
import {
  IFirstName,
  ILastName,
  IPassword,
  IProfession,
  IProfileGet,
  IUsername,
} from '../interfaces/profile.interface';

@Injectable()
export class ProfileService {
  constructor(private readonly _httpClient: HttpClient) {}

  changeName(data: IFirstName) {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/users/update/firstname`,
      data
    );
  }

  changeLastName(data: ILastName) {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/users/update/lastname`,
      data
    );
  }

  changeUsername(data: IUsername) {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/users/update/username`,
      data
    );
  }

  changePassword(data: IPassword) {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/users/update/password`,
      data
    );
  }

  changeProfession(data: IProfession) {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/users/update/profession`,
      data
    );
  }

  getProfileInfo() {
    return this._httpClient.get<IDataAcceptResponse<IProfileGet>>(
      `${environment.API_URL}/users/profile/info`
    );
  }

  getProfileById(userId: string) {
    return this._httpClient.get<IDataAcceptResponse<IProfileGet>>(
      `${environment.API_URL}/users/profile/info/${userId}`
    );
  }

  changeAvatar(fileToUpload: File) {
    const formData = new FormData();
    formData.append('avatar', fileToUpload);
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/users/update/avatar`,
      formData
    );
  }
}
