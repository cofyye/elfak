import { NgModule } from '@angular/core';
import { AmountComponent } from './amount/amount.component';
import { HistoryCreditComponent } from './history-credit/history-credit.component';
import { HistoryServiceComponent } from './history-service/history-service.component';
import { PaymentConfirmationPayPalComponent } from './payment-confirmation-paypal/payment-confirmation-paypal.component';
import { SideCardComponent } from './side-card/side-card.component';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { DividerModule } from 'primeng/divider';
import { RouterModule } from '@angular/router';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { PaymentCancelComponent } from './payment-cancel/payment-cancel.component';
import { PaymentConfirmationStripeComponent } from './payment-confirmation-stripe/payment-confirmation-stripe.component';

@NgModule({
  declarations: [
    HistoryCreditComponent,
    SideCardComponent,
    PaymentConfirmationPayPalComponent,
    PaymentConfirmationStripeComponent,
    HistoryServiceComponent,
    AmountComponent,
    PaymentCancelComponent,
  ],
  imports: [
    CommonModule,
    ButtonModule,
    DividerModule,
    CardModule,
    RouterModule,
    PaginatorModule,
    TableModule,
  ],
  exports: [],
})
export class SharedPaymentModule {}
