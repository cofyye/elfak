import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { IApiResponse, ITicket } from '../interfaces/ticket.interface';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  constructor(private http: HttpClient) {}

  getTickets(page = '1', take = '20', status = ''): Observable<IApiResponse> {
    return this.http.get<IApiResponse>(
      `${environment.API_URL}/admin/ticket/get?page=${page}&take=${take}&status=${status}&order=DESC`
    );
  }

  getTicketDetails(
    ticketId: string
  ): Observable<{ success: boolean; data: ITicket }> {
    return this.http.get<{ success: boolean; data: ITicket }>(
      `${environment.API_URL}/admin/ticket/get/${ticketId}/info`
    );
  }

  sendTicketAnswer(
    ticketId: string,
    message: string
  ): Observable<{ success: boolean }> {
    return this.http.post<{ success: boolean }>(
      `${environment.API_URL}/admin/ticket/answer/create`,
      { ticketId, message }
    );
  }

  closeTicket(ticketId: string): Observable<{ success: boolean }> {
    return this.http.post<{ success: boolean }>(
      `${environment.API_URL}/admin/ticket/close/${ticketId}`,
      {}
    );
  }
}
