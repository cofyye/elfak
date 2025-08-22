import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { IDataAcceptResponse } from "../interfaces/response.interface";
import { IProfile } from "../interfaces/profile.interace";

@Injectable({ providedIn: 'root' })
export class NavbarService {
    constructor(private _httpClient: HttpClient) { }

    getProfileInfo() {
        return this._httpClient.get<IDataAcceptResponse<IProfile>>(`${environment.API_URL}/users/profile/info`);
    }
}