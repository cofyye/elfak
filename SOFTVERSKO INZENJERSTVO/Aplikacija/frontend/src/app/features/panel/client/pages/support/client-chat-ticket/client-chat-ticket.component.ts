import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ITicket, IMessage } from '../../../interfaces/ticket.interface';
import { environment as env } from '../../../../../../../environments/environment';
import { Subject } from 'rxjs';
import { ConfirmationService } from 'primeng/api';
import { ProfileService } from '../../../services/profile.service';
import { TicketService } from '../../../services/ticket.service';

@Component({
  selector: 'app-client-chat-ticket',
  templateUrl: './client-chat-ticket.component.html',
  styleUrls: ['./client-chat-ticket.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class ClientChatTicketComponent implements OnInit {
  ticket: ITicket | undefined;
  isInputVisible: boolean = false;
  isOdgovoriDisabled: boolean = false;
  messages: IMessage[] = [];
  ticketId!: string;
  messageText: string = '';
  confirmDialogVisible: boolean = false;
  apiUrl = env.API_URL;

  ticketTypeMapping: { [key: string]: string } = {
    user_problem: 'Problem sa korisnikom',
    suggestion_improvement: 'Predlog za poboljsanje',
    other: 'Ostalo',
    payment_problem: 'Problem sa placanjem',
  };

  constructor(
    private ticketService: TicketService,
    private route: ActivatedRoute,
    private _confirmation: ConfirmationService,
    private profileService: ProfileService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.ticketId = params['ticketid'];
      this.loadTicketDetails();
    });
  }

  loadTicketDetails(): void {
    this.ticketService.getTicketDetails(this.ticketId).subscribe((response) => {
      this.ticket = response.data;

      this.profileService.getProfileInfo().subscribe((profileResponse) => {
        const currentUser = profileResponse.data;

        const ticketMessage: IMessage = {
          sender: currentUser.username,
          message: this.ticket!.message,
          date: this.ticket!.createdAt,
          avatar: currentUser.avatar,
        };

        this.messages = [
          ticketMessage,
          ...this.ticket!.answers.map((answer) => ({
            sender: answer.user.username,
            message: answer.message,
            date: answer.createdAt,
            avatar: answer.user.avatar,
          })),
        ];
      });
    });
  }

  sendMessage(messageText: string): void {
    if (messageText.trim() !== '') {
      const now = new Date();
      const formattedDate = `${now.getDate().toString().padStart(2, '0')}.${(
        now.getMonth() + 1
      )
        .toString()
        .padStart(2, '0')}.${now.getFullYear()}. ${now
        .getHours()
        .toString()
        .padStart(2, '0')}:${now.getMinutes().toString().padStart(2, '0')}h`;

      this.profileService.getProfileInfo().subscribe((profileResponse) => {
        const currentUser = profileResponse.data;

        const newMessage: IMessage = {
          sender: currentUser.username,
          message: messageText,
          date: formattedDate,
          avatar: currentUser.avatar,
        };

        this.ticketService
          .sendTicketAnswer(this.ticketId, messageText)
          .subscribe(
            (response) => {
              if (response.success) {
                console.log('Message sent successfully:', newMessage);
                this.messages.push(newMessage);
                this.messageText = '';
                this.isInputVisible = false;
              } else {
                console.error('Error sending message:', response);
              }
            },
            (error) => {
              console.error('HTTP Error:', error);
            }
          );
      });
    }
  }

  showInputField(): void {
    this.isInputVisible = true;
  }

  cancelResponse(): void {
    this.isInputVisible = false;
    this.messageText = '';
  }

  finishResponse(): void {
    this.openConfirmDialog();
  }

  openConfirmDialog(): void {
    this.confirmDialogVisible = true;
  }

  confirmCloseTicket(): void {
    this.ticketService.closeTicket(this.ticketId).subscribe((response) => {
      if (response.success) {
        if (this.ticket) {
          this.ticket.status = 'closed';
        }
        this.isOdgovoriDisabled = true;
        this.isInputVisible = false;
      } else {
        // Handle error
      }
      this.closeConfirmDialog();
    });
  }

  cancelCloseTicket(): void {
    this.closeConfirmDialog();
  }

  closeConfirmDialog(): void {
    this.confirmDialogVisible = false;
  }

  canDeactivate(): boolean | Subject<boolean> {
    if (this.messageText.trim() === '') {
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
