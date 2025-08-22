import { ViewportScroller } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HomeService, IUser } from './services/home.service';
import { MessageService } from 'primeng/api';
import { IDataAcceptResponse } from '../shared/interfaces/response.interface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class HomeComponent implements OnInit {
  experts: IUser[] = [];
  constructor(
    private route: ActivatedRoute,
    private viewportScroller: ViewportScroller,
    private homeService: HomeService,
    private messageService: MessageService
  ) { }
  mail: string = '';

  ngOnInit(): void {
    const responseData = (this.route.snapshot.data[
      'home'
    ] as IDataAcceptResponse<IUser[]>).data;
    this.experts = responseData;
    this.route.fragment.subscribe((fragment) => {
      if (fragment) {
        this.viewportScroller.scrollToAnchor(fragment);
      }
    });
  }

  addToMailingList() {
    const emailPattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;
    if (emailPattern.test(this.mail)) {
      this.homeService.addEmail(this.mail).subscribe(() => {
        this.mail = '';
        this.messageService.add({severity:'success', summary:'Uspesno', detail:'Vas mail je uspesno dodat u nasu listu!'});
      });
    } else {
      this.messageService.add({severity:'warn', summary:'Nevalidan email', detail:'Molimo Vas unesite validan email!'});
    }
  }
}