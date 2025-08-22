import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './pages/profile/profile.component';
import { SidebarModule } from 'primeng/sidebar';
import { AvatarModule } from 'primeng/avatar';
import { ButtonModule } from 'primeng/button';
import { ClientRoutingModule } from './client-routing.module';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { IndexComponent } from './pages/index.component';
import { DividerModule } from 'primeng/divider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
// import { AddAdComponent } from './pages/ads/add-ad/add-ad.component';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FileUploadModule } from 'primeng/fileupload';
// import { AdsListComponent } from './pages/ads/ads-list/ads-list.component';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { SelectButtonModule } from 'primeng/selectbutton';
import { CardModule } from 'primeng/card';
import { PasswordModule } from 'primeng/password';
import { TooltipModule } from 'primeng/tooltip';
// import { AdEditComponent } from './pages/ads/ad-edit/ad-edit.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ProfileService } from './services/profile.service';
import { ChatsComponent } from './pages/chats/chats.component';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { ChatComponent } from './pages/chats/components/chat/chat.component';
import { AllChatComponent } from './pages/chats/components/all-chat/all-chat.component';
import { TagModule } from 'primeng/tag';
import { AccordionModule } from 'primeng/accordion';
import { TabViewModule } from 'primeng/tabview';
import { PaginatorModule } from 'primeng/paginator';
import { TextMeasureService } from '../expert/services/text-measure.service';
import { ElementWidthDirectiveClient } from './directives/element-width.directive';
import { HomeComponent } from './pages/home/home.component';
import { TicketService } from './services/ticket.service';
import { ClientChatTicketComponent } from './pages/support/client-chat-ticket/client-chat-ticket.component';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ClientSupportConfirmationComponent } from './pages/support/client-support-confirmation/client-support-confirmation.component';
import { ClientTicketComponent } from './pages/support/cliient-ticket/client-ticket.component';
import { ClientTicketListComponent } from './pages/support/client-ticket-list/client-ticket-list.component';

@NgModule({
  declarations: [
    IndexComponent,
    ProfileComponent,
    SidebarComponent,
    HomeComponent,
    ChatsComponent,
    ChatComponent,
    AllChatComponent,
    ElementWidthDirectiveClient,
    ClientChatTicketComponent,
    ClientSupportConfirmationComponent,
    ClientTicketComponent,
    ClientTicketListComponent,
  ],
  imports: [
    TableModule,
    CommonModule,
    SidebarModule,
    AvatarModule,
    ButtonModule,
    ClientRoutingModule,
    DividerModule,
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
    TicketService,
  ],
})
export class ClientModule {}
