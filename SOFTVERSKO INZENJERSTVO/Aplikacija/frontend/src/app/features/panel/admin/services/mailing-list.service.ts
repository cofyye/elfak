import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { IEmailInfo } from '../pages/mailing-list/mailing-list.component';
import { IAcceptResponse } from '../../../../shared/interfaces/response.interface';

@Injectable({
  providedIn: 'root'
})
export class MailingListService {

  constructor(private _httpClient: HttpClient) { }

  sendEmail(emailData: IEmailInfo) {
    return this._httpClient.post<IAcceptResponse>(`${environment.API_URL}/admin/email-listing/send`, emailData);
  }
}