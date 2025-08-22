import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ITicket } from '../../../interfaces/ticket.interface';
import { TicketService } from '../../../services/ticket.service';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class TicketListComponent implements OnInit {
  ticketList: ITicket[] = [];
  statusList: { status: string }[] = [
    { status: 'Otvoren' },
    { status: 'Zatvoren' },
  ];
  selectedStatus: { status: string } | undefined;
  filteredTicketList: ITicket[] = [];
  first = 0;
  rows = 12;
  reportMessage = '';
  totalRecords = 0;

  constructor(
    private route: ActivatedRoute,
    private ticketService: TicketService
  ) {}

  ngOnInit() {
    this.fetchTickets(); // Fetch tickets without sorting initially
  }

  ngOnChanges() {
    this.filterTickets();
  }

  filterTickets() {
    this.first = 0; // Reset pagination when filtering
    this.fetchTickets();
  }

  onPageChange(event: any) {
    this.first = event.first;
    this.rows = event.rows;

    this.fetchTickets();
  }

  private fetchTickets() {
    const page = (this.first / this.rows + 1).toString();
    const take = this.rows.toString();
    const sortBy = this.selectedStatus
      ? this.reverseTranslateStatus(this.selectedStatus.status)
      : '';

    this.ticketService.getTickets(page, take, sortBy).subscribe((response) => {
      this.ticketList = response.data.data.map((ticket: ITicket) => ({
        ...ticket,
        status: this.translateStatus(ticket.status),
      }));
      this.totalRecords = response.data.meta.itemCount;
      this.filteredTicketList = this.ticketList;
      this.updateReportMessage();
    });
  }

  private updateReportMessage() {
    const page = Math.floor(this.first / this.rows) + 1;
    const end = Math.min(page * this.rows, this.totalRecords);
    this.reportMessage = `Prikazano ${this.first + 1} do ${end} od ukupno ${
      this.totalRecords
    } zahteva`;
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
