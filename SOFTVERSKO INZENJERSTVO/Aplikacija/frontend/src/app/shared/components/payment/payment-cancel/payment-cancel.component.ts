import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment-cancel',
  templateUrl: './payment-cancel.component.html',
  styleUrl: './payment-cancel.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class PaymentCancelComponent implements OnInit {
  fullUrl: string = '';

  constructor(public readonly _router: Router) {}

  ngOnInit(): void {
    const type = this._router.url.toString().includes('client');

    this.fullUrl = type ? '/panel/client' : '/panel/expert';
  }
}
