import { Component, ViewEncapsulation } from '@angular/core';
import { MailingListService } from '../../services/mailing-list.service';
import { MessageService } from 'primeng/api';

export interface IEmailInfo {
  title: string;
  description: string;
}

@Component({
  selector: 'app-mailing-list',
  templateUrl: './mailing-list.component.html',
  styleUrl: './mailing-list.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class MailingListComponent {
  notification!: string;

  titleOfNotification!: string;

  constructor(private _mailingListService: MailingListService, private messageService: MessageService) { }

  sendNotification() {
    if (this.titleOfNotification && this.notification) {
      const emailData : IEmailInfo = {
        title: this.titleOfNotification,
        description: this.notification
      };

      this._mailingListService.sendEmail(emailData).subscribe({
        next:(response)=>{
          this.titleOfNotification = '';
          this.notification = '';
          this.messageService.add({severity:'success', summary:'Upesno', detail:'Uspesno poslat email svim pretplacenim korisnicima!'});
        },
        error:(error)=>{
        }
      });
    }
  }
}
