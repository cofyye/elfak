import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { IndexComponent } from './pages/index.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { AddAdComponent } from './pages/ads/add-ad/add-ad.component';
import { AdsListComponent } from './pages/ads/ads-list/ads-list.component';
import { AdEditComponent } from './pages/ads/ad-edit/ad-edit.component';
import { ChatListComponent } from './pages/chats/chat-list.component';
import { ChatComponent } from './pages/chats/components/chat/chat.component';
import { ChatHomeComponent } from './pages/chats/components/chat-home/chat-home.component';
import { profileResolver } from './resolvers/profile.resolver';
import { canDeactivateGuard } from '../../../shared/guards/unsaved-changes.guard';
import { authenticatedGuard } from '../../../shared/guards/authenticated.guard';
import { AmountComponent } from '../../../shared/components/payment/amount/amount.component';
import { HistoryCreditComponent } from '../../../shared/components/payment/history-credit/history-credit.component';
import { HistoryServiceComponent } from '../../../shared/components/payment/history-service/history-service.component';
import { PaymentConfirmationPayPalComponent } from '../../../shared/components/payment/payment-confirmation-paypal/payment-confirmation-paypal.component';
import { paymentOwnResolver } from '../../../shared/resolvers/payment-own.resolver';
import { paymentSuccessPayPalResolver } from '../../../shared/resolvers/payment-success-paypal.resolver';
import { PaymentCancelComponent } from '../../../shared/components/payment/payment-cancel/payment-cancel.component';
import { PaymentConfirmationStripeComponent } from '../../../shared/components/payment/payment-confirmation-stripe/payment-confirmation-stripe.component';
import { paymentSuccessStripeResolver } from '../../../shared/resolvers/payment-success-stripe.resolver';
import { tagResolver } from './resolvers/tags.resolver';
import { chatConversationResolver } from './resolvers/chat-conversation.resolver';
import { expertAdsResolver } from './resolvers/advert-list.resolver';
import { TicketComponent } from './pages/support/ticket/ticket.component';
import { SupportConfirmationComponent } from './pages/support/support-confirmation/support-confirmation.component';
import { TicketListComponent } from './pages/support/ticket-list/ticket-list.component';
import { ChatTicketComponent } from './pages/support/chat-ticket/chat-ticket.component';
import { isExpertGuard } from '../../../shared/guards/is-expert.guard';
import { paymentServiceResolver } from '../../../shared/resolvers/payment-service.interface';

const routes: Routes = [
  {
    path: '',
    component: IndexComponent,
    canActivateChild: [authenticatedGuard, isExpertGuard],
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
        path: 'ads',
        component: AdsListComponent,
        pathMatch: 'full',
        resolve: {
          ads: expertAdsResolver,
        },
      },
      {
        path: 'ads/add',
        component: AddAdComponent,
        pathMatch: 'full',
        resolve: {
          data: tagResolver,
        },
      },
      {
        path: 'ads/edit/:add_id',
        component: AdEditComponent,
      },
      {
        path: 'chats',
        component: ChatListComponent,
        resolve: {
          messages: chatConversationResolver,
        },
        children: [
          {
            path: '',
            component: ChatHomeComponent,
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
          profile: profileResolver,
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
        component: TicketComponent,
        canDeactivate: [canDeactivateGuard],
        pathMatch: 'full',
      },
      {
        path: 'ticket/confirmation',
        component: SupportConfirmationComponent,
        pathMatch: 'full',
      },
      {
        path: 'ticket-list',
        component: TicketListComponent,
        pathMatch: 'full',
      },
      {
        path: 'ticket/:ticketid',
        component: ChatTicketComponent,
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
export class ExpertRoutingModule {}
