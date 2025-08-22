import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './pages/index.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { profileResolver } from './resolvers/profile.resolver';
import { canDeactivateGuard } from '../../../shared/guards/unsaved-changes.guard';
import { authenticatedGuard } from '../../../shared/guards/authenticated.guard';
import { TicketListComponent } from './pages/support/ticket-list/ticket-list.component';
import { ChatTicketComponent } from './pages/support/chat-ticket/chat-ticket.component';
import { ticketResolver } from './resolvers/ticket.resolver';
import { MailingListComponent } from './pages/mailing-list/mailing-list.component';
import { isAdminGuard } from '../../../shared/guards/is-admin.guard';

const routes: Routes = [
  {
    path: '',
    component: IndexComponent,
    canActivateChild: [authenticatedGuard, isAdminGuard],
    children: [
      {
        path: '',
        redirectTo: 'ticket-list',
        pathMatch: 'full',
      },
      {
        path: 'profile',
        component: ProfileComponent,
        pathMatch: 'full',
        canDeactivate: [canDeactivateGuard],
        resolve: { profile: profileResolver },
      },
      {
        path: 'admin',
        component: ProfileComponent,
        pathMatch: 'full',
        canDeactivate: [canDeactivateGuard],
        resolve: { profile: profileResolver },
      },
      {
        path: 'ticket-list',
        component: TicketListComponent,
        pathMatch: 'full',
        resolve: {
          tickets: ticketResolver,
        },
      },
      {
        path: 'ticket/:ticketid',
        component: ChatTicketComponent,
        pathMatch: 'full',
        canDeactivate: [canDeactivateGuard],
      },
      {
        path: 'mailing-list',
        component: MailingListComponent,
        pathMatch: 'full',
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
