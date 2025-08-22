import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { IDataAcceptResponse } from '../../shared/interfaces/response.interface';

export interface IUser {
    user_id: string;
    first_name: string;
    last_name: string;
    username: string;
    email: string;
    avatar: string;
    profession: string | null;
    bio: string | null;
    grade: string;
}


@Injectable({
    providedIn: 'root'
})
export class HomeService {

    constructor(private _httpClient: HttpClient) { }

    addEmail(email: string) {
        return this._httpClient.post(`${environment.API_URL}/users/email-listing/${email}/add`, {});
    }

    getBestExperts() {
        return this._httpClient.get<IDataAcceptResponse<IUser[]>>(`${environment.API_URL}/users/get/best-experts`);
    }
}
