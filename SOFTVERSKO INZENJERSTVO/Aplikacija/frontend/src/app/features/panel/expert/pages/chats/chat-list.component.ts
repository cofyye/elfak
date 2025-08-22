import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  HostListener,
  OnDestroy,
  OnInit,
  QueryList,
  ViewChildren,
} from '@angular/core';
import { IChatInfo, IChatResponse } from '../../interfaces/chat.interface';
import { TextMeasureService } from '../../services/text-measure.service';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../../../../../environments/environment';
import { MessageService } from 'primeng/api';
import { Store } from '@ngrx/store';
import { AppState } from '../../../../../app.state';
import { Subscription, take } from 'rxjs';
import { getUser } from '../../../../auth/store/auth.selectors';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.css'],
})
export class ChatListComponent implements OnInit, OnDestroy {
  selectedChat: string = '';
  toggle: boolean = false;
  searchTerm: string = '';

  private socketSub: Subscription = new Subscription();
  private currentUserId: string = '';

  @ViewChildren('chatLabel') chatLabels!: QueryList<ElementRef>;
  maxChars: number = 0;
  truncatedTexts: string[] = [];

  chats: IChatInfo[] = [];
  filteredChats: IChatInfo[] = [];

  constructor(
    private readonly _activatedRoute: ActivatedRoute,
    private textMeasureService: TextMeasureService,
    private cdr: ChangeDetectorRef,
    private readonly _chatService: ChatService,
    private readonly _messageService: MessageService,
    private readonly _store: Store<AppState>
  ) {}

  ngOnInit() {
    this.checkScreenWidth();

    const data = this._activatedRoute.snapshot.data[
      'messages'
    ] as IChatResponse[];

    data.map((item) => {
      this.chats.push({
        userId: item.user.id,
        name: item.user.firstName + ' ' + item.user.lastName,
        chat: item.message,
        profilePicture: environment.API_URL + '/uploads/' + item.user.avatar,
        chatStatus:
          item.status === 'read'
            ? '../../../../../../assets/double-check.svg'
            : '../../../../../../assets/check.svg',
      });
    });

    this.filteredChats = this.chats;

    this.socketSub = this._chatService.getNewMessageChatPart().subscribe({
      next: (data) => {
        if (
          data.senderId == this.currentUserId ||
          data.receiverId == this.currentUserId
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

          const chatIndex = this.chats.findIndex(
            (item) =>
              item.userId == data.senderId || item.userId == data.receiverId
          );

          if (data.isDeclinedForm) {
            if (chatIndex != -1) {
              this.chats[chatIndex].chat = data.lastMessage ?? '';
              const chat = this.chats[chatIndex];
              this.chats.splice(chatIndex, 1);
              this.chats.unshift(chat);
              this.filteredChats = [];
              setTimeout(() => {
                this.filteredChats = this.chats;
              }, 1);
            }
          } else {
            if (chatIndex == -1) {
              this.chats.unshift({
                chatStatus: '../../../../../../assets/check.svg',
                chat: data.text,
                name:
                  this.currentUserId == data.senderId
                    ? data.receiverName ?? ''
                    : data.senderName ?? '',
                profilePicture:
                  this.currentUserId == data.senderId
                    ? data.profilePicReceiver ?? ''
                    : data.profilePicSender ?? '',
                userId:
                  this.currentUserId == data.senderId
                    ? data.receiverId
                    : data.senderId,
              });

              this.filteredChats = this.chats;
            } else {
              this.chats.splice(chatIndex, 1);
              this.chats.unshift({
                chatStatus: '../../../../../../assets/check.svg',
                chat: data.text,
                name:
                  this.currentUserId == data.senderId
                    ? data.receiverName ?? ''
                    : data.senderName ?? '',
                profilePicture:
                  this.currentUserId == data.senderId
                    ? data.profilePicReceiver ?? ''
                    : data.profilePicSender ?? '',
                userId:
                  this.currentUserId == data.senderId
                    ? data.receiverId
                    : data.senderId,
              });

              this.filteredChats = this.chats;
            }
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
    if (this.socketSub) {
      this.socketSub.unsubscribe();
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.checkScreenWidth();
  }

  checkScreenWidth() {
    this.toggle = window.innerWidth < 992;
  }

  selectChat(chat: string) {
    this.selectedChat = chat;
  }

  filterChats() {
    const searchTermRegex = new RegExp(this.searchTerm.trim(), 'i');
    this.filteredChats = this.chats.filter((chat) =>
      searchTermRegex.test(chat.name.trim())
    );
  }

  // limitText(text: string): string {
  //   const words = text.split(' ');
  //   if (words.length > this.maxWords) {
  //     return words.slice(0, this.maxWords).join(' ') + '...';
  //   }
  //   return text;
  // }

  limitText(text: string, font: string, maxWidth: number): string {
    const ellipsis = '...';
    let truncatedText = text;

    while (
      this.textMeasureService.measureText(truncatedText, font) > maxWidth &&
      truncatedText.length > 0
    ) {
      truncatedText = truncatedText.slice(0, -1);
    }

    if (truncatedText.length < text.length) {
      truncatedText = truncatedText.slice(0, -1) + ellipsis;
    }

    return truncatedText;
  }

  onWidthChange(newWidth: number, index: number) {
    if (this.chatLabels.toArray()[index]) {
      const font = window.getComputedStyle(
        this.chatLabels.toArray()[index].nativeElement
      ).font;
      this.truncatedTexts[index] = this.limitText(
        this.chats[index].chat,
        font,
        newWidth
      );
    }
    this.cdr.detectChanges();
  }

  ngAfterViewInit() {
    this.updateTruncatedTexts();
    this.cdr.detectChanges();
  }

  updateTruncatedTexts() {
    this.chatLabels.forEach((label, index) => {
      const font = window.getComputedStyle(label.nativeElement).font;
      this.truncatedTexts[index] = this.limitText(
        this.chats[index].chat,
        font,
        label.nativeElement.offsetWidth
      );
    });
    this.cdr.detectChanges();
  }
}
