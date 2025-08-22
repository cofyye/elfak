import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment-confirmation-paypal',
  templateUrl: './payment-confirmation-paypal.component.html',
  styleUrl: './payment-confirmation-paypal.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class PaymentConfirmationPayPalComponent implements OnInit {
  fullUrl: string = '';

  constructor(public readonly _router: Router) {}

  ngOnInit(): void {
    const type = this._router.url.toString().includes('client');

    this.fullUrl = type ? '/panel/client' : '/panel/expert';
  }
}
