import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ITicket } from '../../../interfaces/ticket.interface';
import { TicketService } from '../../../services/ticket.service';

@Component({
  selector: 'app-client-ticket-list',
  templateUrl: './client-ticket-list.component.html',
  styleUrls: ['./client-ticket-list.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class ClientTicketListComponent implements OnInit {
  ticketList: ITicket[] = [];
  statusList: { status: string }[] = [
    { status: 'Otvoren' },
    { status: 'Zatvoren' },
  ];
  selectedStatus: { status: string } | undefined;

  constructor(
    private route: ActivatedRoute,
    private ticketService: TicketService
  ) {}

  ngOnInit() {
    this.fetchTickets(); // Fetch tickets without sorting initially
  }

  private fetchTickets() {
    console.log('Fetching tickets');
    this.ticketService.getAllTickets().subscribe({
      next: (data) => {
        this.ticketList = data.data;
      },
      error: (error) => {
        console.error('Error fetching tickets', error);
      },
    });
  }

  translateStatus(status: string): string {
    switch (status) {
      case 'open':
        return 'Otvoren';
      case 'closed':
        return 'Zatvoren';
      default:
        return status;
    }
  }

  reverseTranslateStatus(status: string): string {
    switch (status) {
      case 'Otvoren':
        return 'open';
      case 'Zatvoren':
        return 'closed';
      default:
        return '';
    }
  }
}
