import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './pages/index.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { ChatsComponent } from './pages/chats/chats.component';
import { ChatComponent } from './pages/chats/components/chat/chat.component';
import { AllChatComponent } from './pages/chats/components/all-chat/all-chat.component';
import { profileResolver } from './resolvers/profile.resolver';
import { canDeactivateGuard } from '../../../shared/guards/unsaved-changes.guard';
import { AmountComponent } from '../../../shared/components/payment/amount/amount.component';
import { HistoryCreditComponent } from '../../../shared/components/payment/history-credit/history-credit.component';
import { HistoryServiceComponent } from '../../../shared/components/payment/history-service/history-service.component';
import { PaymentConfirmationPayPalComponent } from '../../../shared/components/payment/payment-confirmation-paypal/payment-confirmation-paypal.component';
import { paymentOwnResolver } from '../../../shared/resolvers/payment-own.resolver';
import { paymentSuccessPayPalResolver } from '../../../shared/resolvers/payment-success-paypal.resolver';
import { PaymentCancelComponent } from '../../../shared/components/payment/payment-cancel/payment-cancel.component';
import { PaymentConfirmationStripeComponent } from '../../../shared/components/payment/payment-confirmation-stripe/payment-confirmation-stripe.component';
import { paymentSuccessStripeResolver } from '../../../shared/resolvers/payment-success-stripe.resolver';
import { HomeComponent } from './pages/home/home.component';
import { ClientTicketComponent } from './pages/support/cliient-ticket/client-ticket.component';
import { ClientSupportConfirmationComponent } from './pages/support/client-support-confirmation/client-support-confirmation.component';
import { ClientTicketListComponent } from './pages/support/client-ticket-list/client-ticket-list.component';
import { ClientChatTicketComponent } from './pages/support/client-chat-ticket/client-chat-ticket.component';
import { paymentServiceResolver } from '../../../shared/resolvers/payment-service.interface';

const routes: Routes = [
  {
    path: '',
    component: IndexComponent,
    children: [
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
      },
      {
        path: 'home',
        component: HomeComponent,
        pathMatch: 'full',
        resolve: { profile: profileResolver },
      },
      {
        path: 'profile',
        component: ProfileComponent,
        pathMatch: 'full',
        canDeactivate: [canDeactivateGuard],
        resolve: { profile: profileResolver },
      },
      {
        path: 'chats',
        component: ChatsComponent,
        children: [
          {
            path: '',
            component: AllChatComponent,
            pathMatch: 'full',
          },
          {
            path: ':id',
            component: ChatComponent,
            pathMatch: 'full',
          },
        ],
      },
      {
        path: 'history-credit',
        component: HistoryCreditComponent,
        pathMatch: 'full',
        resolve: {
          payments: paymentOwnResolver,
        },
      },
      {
        path: 'history-service',
        component: HistoryServiceComponent,
        pathMatch: 'full',
        resolve: {
          payments: paymentServiceResolver,
        },
      },
      {
        path: 'amount',
        component: AmountComponent,
        pathMatch: 'full',
      },
      {
        path: 'amount/paypal/cancel',
        component: PaymentCancelComponent,
        pathMatch: 'full',
      },
      {
        path: 'amount/stripe/cancel',
        component: PaymentCancelComponent,
        pathMatch: 'full',
      },
      {
        path: 'amount/paypal/success',
        component: PaymentConfirmationPayPalComponent,
        pathMatch: 'full',
        resolve: {
          payments: paymentSuccessPayPalResolver,
        },
      },
      {
        path: 'amount/stripe/success',
        component: PaymentConfirmationStripeComponent,
        pathMatch: 'full',
        resolve: {
          payments: paymentSuccessStripeResolver,
        },
      },
      {
        path: 'ticket',
        component: ClientTicketComponent,
        canDeactivate: [canDeactivateGuard],
        pathMatch: 'full',
      },
      {
        path: 'ticket/confirmation',
        component: ClientSupportConfirmationComponent,
        pathMatch: 'full',
      },
      {
        path: 'ticket-list',
        component: ClientTicketListComponent,
        pathMatch: 'full',
      },
      {
        path: 'ticket/:ticketid',
        component: ClientChatTicketComponent,
        pathMatch: 'full',
        canDeactivate: [canDeactivateGuard],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientRoutingModule {}
