import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TicketService } from '../../../services/ticket.service';
import { MessageService } from 'primeng/api';
import { ITicket, ITicketInput } from '../../../interfaces/ticket.interface';
import { ConfirmationService } from 'primeng/api';
import { Subject } from 'rxjs';

interface TicketType {
  name: string;
  type: string;
}

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css'],
  encapsulation: ViewEncapsulation.None,
  providers: [TicketService],
})
export class TicketComponent implements OnInit {
  tickets: TicketType[] = [];
  selectedTicket: TicketType | undefined;

  opisProblema: string = '';
  username: string = '';
  naslov: string = '';
  confirmed: boolean = false;

  constructor(
    private _confirmation: ConfirmationService,
    private readonly _ticketService: TicketService,
    private readonly _messageService: MessageService,
    private readonly _router: Router
  ) { }

  ngOnInit() {
    this.tickets = [
      { name: 'Problem sa korisnikom', type: 'user_problem' },
      { name: 'Problem sa placanjem', type: 'payment_problem' },
      { name: 'Predlog za poboljsanje', type: 'suggestion_improvement' },
      { name: 'Ostalo', type: 'other' },
    ];
  }

  isFormValid(): boolean {
    if (this.selectedTicket) {
      if (this.selectedTicket.type === 'user_problem') {
        return (
          this.username.trim() !== '' &&
          this.opisProblema.trim() !== '' &&
          this.naslov.trim() !== ''
        );
      } else {
        return this.opisProblema.trim() !== '' && this.naslov.trim() !== '';
      }
    }
    return false;
  }

  submitForm() {
    this.confirmed = true;
    const ticketData: ITicketInput = {
      userId: '2bfd058f-7066-4b09-9f52-feacd9bb81c3',
      title: this.naslov,
      message: this.opisProblema,
      username:
        this.selectedTicket?.type === 'user_problem'
          ? this.username
          : undefined,
      type: this.selectedTicket ? this.selectedTicket.type : '',
    };

    this._ticketService.createTicket(ticketData).subscribe({
      next: (response) => {
        this._messageService.add({
          severity: 'success',
          summary: 'Uspešno',
          detail: 'Vaš tiket je uspešno poslat.',
        });
        this._router.navigate(['/panel/expert/ticket/confirmation']);
      },
    });
  }

  canDeactivate() {
    if (
      this.opisProblema == '' &&
      this.naslov == '' &&
      this.username == '' &&
      this.confirmed == false
    ) {
      return true;
    }
    if (this.confirmed) {
      return true;
    }
    const deactivateSubject = new Subject<boolean>();
    this._confirmation.confirm({
      message:
        'Imate nesacuvane promene. Da li ste sigurni da zelite da napustite ovu stranicu?',
      header: 'Potvrda',
      icon: 'pi pi-exclamation-triangle',
      acceptIcon: 'none',
      rejectIcon: 'none',
      rejectButtonStyleClass: 'p-button-text',
      acceptLabel: 'Da',
      rejectLabel: 'Ne',
      accept: () => {
        deactivateSubject.next(true);
      },
      reject: () => {
        deactivateSubject.next(false);
      },
    });
    return deactivateSubject;
  }
}
