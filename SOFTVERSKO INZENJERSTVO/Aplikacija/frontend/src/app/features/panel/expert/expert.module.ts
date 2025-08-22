import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './pages/profile/profile.component';
import { SidebarModule } from 'primeng/sidebar';
import { AvatarModule } from 'primeng/avatar';
import { HomeComponent } from './pages/home/home.component';
import { ButtonModule } from 'primeng/button';
import { ExpertRoutingModule } from './expert-routing.module';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { IndexComponent } from './pages/index.component';
import { DividerModule } from 'primeng/divider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { AddAdComponent } from './pages/ads/add-ad/add-ad.component';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FileUploadModule } from 'primeng/fileupload';
import { AdsListComponent } from './pages/ads/ads-list/ads-list.component';
import { TableModule } from 'primeng/table';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { SelectButtonModule } from 'primeng/selectbutton';
import { CardModule } from 'primeng/card';
import { PasswordModule } from 'primeng/password';
import { TooltipModule } from 'primeng/tooltip';
import { AdEditComponent } from './pages/ads/ad-edit/ad-edit.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ProfileService } from './services/profile.service';
import { ChatListComponent } from './pages/chats/chat-list.component';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { ChatComponent } from './pages/chats/components/chat/chat.component';
import { ChatHomeComponent } from './pages/chats/components/chat-home/chat-home.component';
import { TagModule } from 'primeng/tag';
import { AccordionModule } from 'primeng/accordion';
import { TabViewModule } from 'primeng/tabview';
import { PaginatorModule } from 'primeng/paginator';
import { ElementWidthDirectiveExpert } from './directives/element-width.directive';
import { TextMeasureService } from './services/text-measure.service';
import { AdvertService } from './services/advert.service';
import { ChatService } from './services/chat.service';
import { FloatLabelModule } from 'primeng/floatlabel';
import { SupportConfirmationComponent } from './pages/support/support-confirmation/support-confirmation.component';
import { TicketComponent } from './pages/support/ticket/ticket.component';
import { TicketListComponent } from './pages/support/ticket-list/ticket-list.component';
import { TruncatePipe } from '../../../shared/pipes/truncate.pipe';
import { ChatTicketComponent } from './pages/support/chat-ticket/chat-ticket.component';
import { DialogModule } from 'primeng/dialog';
import { TicketService } from './services/ticket.service';
import { ToastModule } from 'primeng/toast';

@NgModule({
  declarations: [
    IndexComponent,
    ProfileComponent,
    SidebarComponent,
    HomeComponent,
    AddAdComponent,
    AdsListComponent,
    AdEditComponent,
    ChatListComponent,
    ChatComponent,
    ChatHomeComponent,
    ElementWidthDirectiveExpert,
    TicketComponent,
    SupportConfirmationComponent,
    TicketListComponent,
    TruncatePipe,
    ChatTicketComponent,
  ],
  imports: [
    ToastModule,
    TableModule,
    CommonModule,
    SidebarModule,
    AvatarModule,
    ButtonModule,
    ExpertRoutingModule,
    DividerModule,
    FormsModule,
    InputTextModule,
    InputTextareaModule,
    FileUploadModule,
    ToggleButtonModule,
    SelectButtonModule,
    CardModule,
    PasswordModule,
    TooltipModule,
    ReactiveFormsModule,
    AutoCompleteModule,
    ConfirmDialogModule,
    DialogModule,
    ScrollPanelModule,
    IconFieldModule,
    InputIconModule,
    TagModule,
    AccordionModule,
    TabViewModule,
    PaginatorModule,
    FloatLabelModule,
  ],
  providers: [
    ConfirmationService,
    ProfileService,
    TextMeasureService,
    AdvertService,
    ChatService,
    TicketService
  ],
})
export class ExpertModule { }
