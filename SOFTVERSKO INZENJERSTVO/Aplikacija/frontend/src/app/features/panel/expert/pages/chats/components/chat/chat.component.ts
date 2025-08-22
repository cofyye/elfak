import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  HostListener,
  Input,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../../../../../../environments/environment';
import {
  IFormUser,
  ISocketMessage,
  Message,
} from '../../../../interfaces/chat.interface';
import { ChatService } from '../../../../services/chat.service';
import { Subscription, take } from 'rxjs';
import { ProfileService } from '../../../../services/profile.service';
import { Store } from '@ngrx/store';
import { AppState } from '../../../../../../../app.state';
import { getUser } from '../../../../../../auth/store/auth.selectors';
import moment from 'moment';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class ChatComponent implements OnInit, OnDestroy {
  @ViewChild('aboveChatMessages', { static: false })
  private aboveChatMessages!: ElementRef;

  @ViewChild('chatMessages', { static: false })
  private chatMessagesContainer!: ElementRef;

  @ViewChild('chatInputContainer', { static: false })
  private chatInputContainer!: ElementRef;

  private userId: string = '';
  private currentUserId: string = '';

  private routeSub: Subscription = new Subscription();
  private socketNewMessageSub: Subscription = new Subscription();
  private socketNewDeclineFormSub: Subscription = new Subscription();
  private socketNewAcceptFormSub: Subscription = new Subscription();

  toggleForm: boolean = false;
  messages: Message[] = [];
  newMessage: string = '';
  fullname: string = '';
  avatar: string = '';
  isLockedInputs = false; // za formu ako je vec izmenio da ne moze dok neko drugi ne izmeni

  selectedForm: IFormUser = {
    accepted_client: false,
    accepted_expert: false,
    adId: '',
    choosedPlan: '',
    clientId: '',
    date: '',
    time: '',
    description: '',
    id: '',
    tasks: [],
    status: '',
    price: '',
    userType: 'client',
    advert: {
      title: '',
    },
  };
  truncateLength!: number;

  constructor(
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _chatService: ChatService,
    private readonly _profileService: ProfileService,
    private readonly _messageService: MessageService,
    private readonly _router: Router,
    private readonly _store: Store<AppState>
  ) {}

  acceptForm() {
    if (this.isLockedInputs) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Nije Vas red da menjate konsultaciju.',
      });
      return;
    }

    const data: Partial<ISocketMessage> = {
      senderId: this.currentUserId,
      receiverId: this.userId,
      description: this.selectedForm.description,
      price: this.selectedForm.price,
      adId: this.selectedForm.adId,
      clientId: this.selectedForm.clientId,
      plans: JSON.stringify(this.selectedForm.tasks),
      dateTime: moment
        .tz(
          `${this.selectedForm.date}T${this.selectedForm.time}`,
          'Europe/Belgrade'
        )
        .toDate(),
    };

    this._chatService.sendAcceptForm(data);
  }

  rejectForm() {
    if (this.isLockedInputs) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Nije Vas red da menjate konsultaciju.',
      });
      return;
    }

    const data: Partial<ISocketMessage> = {
      senderId: this.currentUserId,
      receiverId: this.userId,
      adId: this.selectedForm.adId,
      clientId: this.selectedForm.clientId,
      lastMessage: this.messages[0]?.text,
      isDeclinedForm: true,
    };

    this._chatService.sendDeclineForm(data);
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  ngAfterViewInit() {
    this.adjustChatMessagesHeight();
  }

  onDialogHide() {
    this.adjustChatMessagesHeight();
  }

  trackByIndex(index: number, item: any): any {
    return index;
  }

  deleteTask(index: number) {
    if (this.isLockedInputs) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Nije Vas red da menjate konsultaciju.',
      });
      return;
    }
    this.selectedForm.tasks.splice(index, 1);
  }

  sendMessage() {
    this._chatService.sendNewMessage({
      text: this.newMessage,
      senderId: this.currentUserId,
      receiverId: this.userId,
      timestamp: moment.tz(moment(), 'Europe/Belgrade').toISOString(),
    } as ISocketMessage);

    this.newMessage = '';
  }

  scrollToBottom(): void {
    try {
      this.chatMessagesContainer.nativeElement.scrollTop =
        this.chatMessagesContainer.nativeElement.scrollHeight;
    } catch (err) {}
  }

  ngOnInit(): void {
    this.setTruncateLength();
    this.routeSub = this._activatedRoute.paramMap.subscribe((params) => {
      this.userId = params.get('id') ?? '';

      this.messages = [];

      this._chatService.getMessagesFromUser(this.userId).subscribe({
        next: (response) => {
          response.data.map((item) => {
            this.messages.push({
              sentBy: item.sentBy,
              profilePic:
                item.sentBy == 'me'
                  ? environment.API_URL + '/uploads/' + item.sender.avatar
                  : environment.API_URL + '/uploads/' + item.sender.avatar,
              text: item.message,
              timestamp: item.createdAt.toString(),
              sender: item.sender,
              receiver: item.receiver,
            });
          });
        },
        error: () => {
          this._router.navigate(['/panel/expert/chats']);
        },
      });

      this._chatService.getForm(this.userId).subscribe({
        next: (response) => {
          if (response.success && response.data) {
            let planName = '';

            switch (response.data.choosedPlan) {
              case 'basic': {
                planName = 'Osnovni';
                break;
              }
              case 'standard': {
                planName = 'Srednji';
                break;
              }
              case 'premium': {
                planName = 'Napredni';
                break;
              }
              case 'custom': {
                planName = 'Personalizovani';
                break;
              }
            }

            this.selectedForm = {
              tasks: response.data.plans,
              date: moment(response.data.dateTime).format('YYYY-MM-DD'),
              time: moment(response.data.dateTime).format('HH:mm'),
              description: response.data.description,
              accepted_client: response.data.accepted_client,
              accepted_expert: response.data.accepted_expert,
              choosedPlan: planName,
              clientId: response.data.clientId,
              adId: response.data.adId,
              status: response.data.status,
              id: response.data.id,
              price: response.data.price,
              userType: response.data.userType,
              advert: {
                title: response.data.advert.title,
              },
            };

            if (
              (this.selectedForm.userType === 'client' &&
                this.selectedForm.accepted_client) ||
              (this.selectedForm.userType === 'expert' &&
                this.selectedForm.accepted_expert)
            ) {
              this.isLockedInputs = true;
            }
          }
        },
        error: () => {
          this._router.navigate(['/panel/expert/chats']);
        },
      });

      this._profileService.getProfileById(this.userId).subscribe({
        next: (response) => {
          this.fullname =
            response.data.firstName + ' ' + response.data.lastName;
          this.avatar =
            environment.API_URL + '/uploads/' + response.data.avatar;
        },
      });
    });

    this.socketNewMessageSub = this._chatService.getNewMessage().subscribe({
      next: (data) => {
        if (
          (data.receiverId == this.userId &&
            data.senderId == this.currentUserId) ||
          (data.receiverId == this.currentUserId &&
            data.senderId == this.userId)
        ) {
          if (data.error && data.senderId == this.currentUserId) {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail: data.error,
            });
            return;
          } else if (data.error) {
            return;
          }

          if (data.senderId == this.currentUserId) {
            this.messages.push({
              profilePic: data.profilePic,
              text: data.text,
              sentBy: 'me',
              timestamp: moment(data.timestamp).format('DD.MM.YYYY. HH:mm[h]'),
            });
          } else {
            this.messages.push({
              profilePic: data.profilePic,
              text: data.text,
              sentBy: 'other',
              timestamp: moment(data.timestamp).format('DD.MM.YYYY. HH:mm[h]'),
            });
          }
        }
      },
    });

    this.socketNewDeclineFormSub = this._chatService
      .getNewDeclineForm()
      .subscribe({
        next: (data) => {
          if (
            (data.receiverId == this.userId &&
              data.senderId == this.currentUserId) ||
            (data.receiverId == this.currentUserId &&
              data.senderId == this.userId)
          ) {
            if (data.error && data.senderId == this.currentUserId) {
              this._messageService.add({
                severity: 'error',
                summary: 'Greska',
                detail: data.error,
              });
              return;
            } else if (data.error) {
              return;
            }

            this.selectedForm = {
              accepted_client: false,
              accepted_expert: false,
              adId: '',
              choosedPlan: '',
              clientId: '',
              date: '',
              time: '',
              description: '',
              id: '',
              tasks: [],
              status: '',
              price: '',
              userType: 'client',
              advert: {
                title: '',
              },
            };

            if (data.senderId == this.currentUserId) {
              this._messageService.add({
                severity: 'success',
                summary: 'Uspesno',
                detail: 'Uspesno ste odbili konsultaciju.',
              });
            }
          }
        },
      });

    this.socketNewAcceptFormSub = this._chatService
      .getNewAcceptForm()
      .subscribe({
        next: (data) => {
          if (
            (data.receiverId == this.userId &&
              data.senderId == this.currentUserId) ||
            (data.receiverId == this.currentUserId &&
              data.senderId == this.userId)
          ) {
            if (data.error && data.senderId == this.currentUserId) {
              this._messageService.add({
                severity: 'error',
                summary: 'Greska',
                detail: data.error,
              });
              return;
            } else if (data.error) {
              return;
            }

            this.selectedForm.accepted_client =
              !this.selectedForm.accepted_client;
            this.selectedForm.accepted_expert =
              !this.selectedForm.accepted_expert;
            this.selectedForm.date = moment(data.dateTime).format('YYYY-MM-DD');
            this.selectedForm.time = moment(data.dateTime).format('HH:mm');
            this.selectedForm.tasks = JSON.parse(data.plans ?? '[]');
            this.selectedForm.description = data.description ?? '';
            this.selectedForm.price = data.price ?? '';
            this.selectedForm.status = data.status ?? 'in_progress';

            if (
              (this.selectedForm.userType === 'client' &&
                this.selectedForm.accepted_client) ||
              (this.selectedForm.userType === 'expert' &&
                this.selectedForm.accepted_expert)
            ) {
              this.isLockedInputs = true;
            } else {
              this.isLockedInputs = false;
            }

            if (data.senderId == this.currentUserId) {
              this._messageService.add({
                severity: 'success',
                summary: 'Uspesno',
                detail: data.success ?? 'Uspesno ste prihvatili konsultaciju.',
              });
            }

            if (data.receiverId == this.currentUserId && !data.error) {
              this._messageService.add({
                severity: 'success',
                summary: 'Uspesno',
                detail: 'Promena na konsultaciji je napravljena.',
              });
            }
          }
        },
      });

    this._store
      .select(getUser)
      .pipe(take(1))
      .subscribe({
        next: (user) => {
          this.currentUserId = user.id;
        },
      });
  }

  ngOnDestroy(): void {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }

    if (this.socketNewMessageSub) {
      this.socketNewMessageSub.unsubscribe();
    }

    if (this.socketNewDeclineFormSub) {
      this.socketNewDeclineFormSub.unsubscribe();
    }

    if (this.socketNewAcceptFormSub) {
      this.socketNewAcceptFormSub.unsubscribe();
    }
  }

  @HostListener('window:resize')
  onResize() {
    this.setTruncateLength();
    this.adjustChatMessagesHeight();
  }

  setTruncateLength() {
    const width = window.innerWidth;

    if (width <= 320) {
      this.truncateLength = 10;
    } else if (width <= 360) {
      this.truncateLength = 15;
    } else if (width <= 400) {
      this.truncateLength = 20;
    } else {
      this.truncateLength = 30;
    }
  }

  onToggleForm() {
    this.toggleForm = !this.toggleForm;
  }

  redirectToCall() {
    this._router.navigate([`/video/${this.selectedForm.id}`]);
  }

  adjustChatMessagesHeight() {
    const chatMessagesElement = this.chatMessagesContainer?.nativeElement;
    const chatInputElement = this.chatInputContainer?.nativeElement;
    const aboveChatMessagesElement = this.aboveChatMessages?.nativeElement;

    const containerHeight = chatMessagesElement.parentElement.clientHeight;
    const chatInputHeight = chatInputElement.clientHeight;
    const aboveChatMessagesHeight = aboveChatMessagesElement.clientHeight;

    let newHeight =
      containerHeight - chatInputHeight - aboveChatMessagesHeight - 30;

    if (window.innerWidth <= 992) {
      newHeight += 8;
    }

    chatMessagesElement.style.height = `${newHeight}px`;
  }
}
