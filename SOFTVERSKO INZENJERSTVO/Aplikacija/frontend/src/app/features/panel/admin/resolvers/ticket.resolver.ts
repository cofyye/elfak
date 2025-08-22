import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { Observable } from 'rxjs';
import { TicketService } from '../services/ticket.service';
import { IApiResponse } from '../interfaces/ticket.interface';

export const ticketResolver: ResolveFn<Observable<IApiResponse>> = () => {
  const ticketService = inject(TicketService);
  const page = 1;
  const take = 20;
  const status = 'open';
  return ticketService.getTickets(page.toString(), take.toString(), status);
};
