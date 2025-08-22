import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { IAcceptResponse, IDataAcceptResponse } from '../../../../shared/interfaces/response.interface';
import { ITicket, ITicketInput } from '../interfaces/ticket.interface';


@Injectable()
export class TicketService {
  constructor(private readonly _httpClient: HttpClient) { }

  createTicket(data: ITicketInput): Observable<IAcceptResponse> {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/ticket/create`,
      data
    );
  }

  getAllTickets(): Observable<IDataAcceptResponse<ITicket[]>> {
    return this._httpClient.get<IDataAcceptResponse<ITicket[]>>(`${environment.API_URL}/ticket/get/own`);
  }

  getTicketDetails(
    ticketId: string
  ): Observable<IDataAcceptResponse<ITicket>> {
    return this._httpClient.get<IDataAcceptResponse<ITicket>>(
      `${environment.API_URL}/ticket/get/${ticketId}/info`
    );
  }

  sendTicketAnswer(
    ticketId: string,
    message: string
  ): Observable<IAcceptResponse> {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/ticket/answer/create`,
      { ticketId, message }
    );
  }

  closeTicket(ticketId: string): Observable<IAcceptResponse> {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/ticket/close/${ticketId}`,
      {}
    );
  }
}
