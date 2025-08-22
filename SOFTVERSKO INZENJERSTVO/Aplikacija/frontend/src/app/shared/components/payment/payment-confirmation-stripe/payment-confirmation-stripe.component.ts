import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment-confirmation-stripe',
  templateUrl: './payment-confirmation-stripe.component.html',
  styleUrl: './payment-confirmation-stripe.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class PaymentConfirmationStripeComponent implements OnInit {
  fullUrl: string = '';

  constructor(public readonly _router: Router) {}

  ngOnInit(): void {
    const type = this._router.url.toString().includes('client');

    this.fullUrl = type ? '/panel/client' : '/panel/expert';
  }
}
