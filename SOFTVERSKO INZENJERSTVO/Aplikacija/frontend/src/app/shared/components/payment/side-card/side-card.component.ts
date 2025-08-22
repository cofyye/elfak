import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { IProfileGet } from '../../../../features/panel/client/interfaces/profile.interface';
import { IDataAcceptResponse } from '../../../interfaces/response.interface';
@Component({
  selector: 'app-side-card',
  templateUrl: './side-card.component.html',
  styleUrls: ['./side-card.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class SideCardComponent implements OnInit {
  balance: string = '';
  fullUrl: string = '';

  constructor(
    public readonly _router: Router,
    private readonly _activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const type = this._router.url.toString().includes('client');
    this.fullUrl = type ? '/panel/client' : '/panel/expert';

    const responseData = (
      this._activatedRoute.snapshot.data[
      'profile'
      ] as IDataAcceptResponse<IProfileGet>
    ).data;
    this.balance = responseData.tokens.toString();
  }
}
