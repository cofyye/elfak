import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  HostListener,
  OnInit,
  QueryList,
  ViewChildren,
} from '@angular/core';
import { IChatInfo } from '../../interfaces/chat-info.interface';
import { TextMeasureService } from '../../../expert/services/text-measure.service';

@Component({
  selector: 'app-chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.css'],
})
export class ChatsComponent implements OnInit {
  selectedChat: string = '';
  toggle: boolean = false;
  searchTerm: string = '';

  @ViewChildren('chatLabel') chatLabels!: QueryList<ElementRef>;
  maxChars: number = 0;
  truncatedTexts: string[] = [];

  chats: IChatInfo[] = [
    {
      name: 'Tommy Shelby',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/walter.jpg',
      chat: 'Brate kad ce popravljamo ono sto smo pricali.',
      chatStatus: '../../../../../../assets/double-check.svg',
    },
    {
      name: 'Andjela Mia Doncov',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/amyelsner.png',
      chat: 'Dobar dan videla sam Vas oglas iz struktura podataka.',
      chatStatus: '../../../../../../assets/double-check.svg',
    },
    {
      name: 'Vuk Vukadinovic',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/onyamalimba.png',
      chat: 'Zanima me kako bi se odvijao kurs iz Uvoda u racunarstvo koji nudite na profilu?',
      chatStatus: '../../../../../../assets/check.svg',
    },
    {
      name: 'Mateja Jovic',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/asiyajavayant.png',
      chat: 'Zdravo Mateja, da li si zadovoljna listom koju sam ti poslao?',
      chatStatus: '../../../../../../assets/check.svg',
    },
    {
      name: 'Filip Lakicevic',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/onyamalimba.png',
      chat: 'Zdravo Filipe, da li bismo mogli da se cujemo veceras kako bismo se dogovorili oko konsultacija sutra?',
      chatStatus: '../../../../../../assets/load-message.svg',
    },
    {
      name: 'Andjela Pavlovic',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/amyelsner.png',
      chat: 'Prijatno!',
      chatStatus: '../../../../../../assets/double-check.svg',
    },
    {
      name: 'Aleksa Ilic',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/walter.jpg',
      chat: 'U redu, cujemo se u utorak.',
      chatStatus: '../../../../../../assets/double-check.svg',
    },
    {
      name: 'Nikola Doncov',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/walter.jpg',
      chat: 'Aha, razumem, znaci moja je greska bila u kodu?',
      chatStatus: '../../../../../../assets/load-message.svg',
    },
    {
      name: 'Vuk Vukadinovic',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/onyamalimba.png',
      chat: 'Zanima me kako bi se odvijao kurs iz Uvoda u racunarstvo koji nudite na profilu?',
      chatStatus: '../../../../../../assets/check.svg',
    },
    {
      name: 'Mateja Jovic',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/asiyajavayant.png',
      chat: 'Zdravo Mateja, da li si zadovoljna listom koju sam ti poslao?',
      chatStatus: '../../../../../../assets/check.svg',
    },
    {
      name: 'Filip Lakicevic',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/onyamalimba.png',
      chat: 'Zdravo Filipe, da li bismo mogli da se cujemo veceras kako bismo se dogovorili oko konsultacija sutra?',
      chatStatus: '../../../../../../assets/load-message.svg',
    },
    {
      name: 'Andjela Pavlovic',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/amyelsner.png',
      chat: 'Prijatno!',
      chatStatus: '../../../../../../assets/double-check.svg',
    },
    {
      name: 'Aleksa Ilic',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/walter.jpg',
      chat: 'U redu, cujemo se u utorak.',
      chatStatus: '../../../../../../assets/double-check.svg',
    },
    {
      name: 'Nikola Doncov',
      profilePicture:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/walter.jpg',
      chat: 'Aha, razumem, znaci moja je greska bila u kodu?',
      chatStatus: '../../../../../../assets/load-message.svg',
    },
  ];
  filteredChats: IChatInfo[] = [];

  constructor(
    private textMeasureService: TextMeasureService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.filteredChats = this.chats;
    this.checkScreenWidth();
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
