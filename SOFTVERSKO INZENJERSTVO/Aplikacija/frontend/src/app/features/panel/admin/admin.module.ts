import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './pages/profile/profile.component';
import { SidebarModule } from 'primeng/sidebar';
import { AvatarModule } from 'primeng/avatar';
import { ButtonModule } from 'primeng/button';
import { AdminRoutingModule } from './admin-routing.module';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { IndexComponent } from './pages/index.component';
import { DividerModule } from 'primeng/divider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { SelectButtonModule } from 'primeng/selectbutton';
import { CardModule } from 'primeng/card';
import { PasswordModule } from 'primeng/password';
import { TooltipModule } from 'primeng/tooltip';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ProfileService } from './services/profile.service';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { TagModule } from 'primeng/tag';
import { AccordionModule } from 'primeng/accordion';
import { TabViewModule } from 'primeng/tabview';
import { PaginatorModule } from 'primeng/paginator';
import { ElementWidthDirectiveExpert } from './directives/element-width.directive';
import { TextMeasureService } from './services/text-measure.service';
import { AdvertService } from './services/advert.service';
import { ChatService } from './services/chat.service';
import { FloatLabelModule } from 'primeng/floatlabel';
import { TicketListComponent } from './pages/support/ticket-list/ticket-list.component';
import { ChatTicketComponent } from './pages/support/chat-ticket/chat-ticket.component';
import { MailingListComponent } from './pages/mailing-list/mailing-list.component';
import { ToastModule } from 'primeng/toast';

@NgModule({
  declarations: [
    IndexComponent,
    ProfileComponent,
    SidebarComponent,
    ElementWidthDirectiveExpert,
    TicketListComponent,
    ChatTicketComponent,
    MailingListComponent,
  ],
  imports: [
    TableModule,
    CommonModule,
    SidebarModule,
    AvatarModule,
    ButtonModule,
    AdminRoutingModule,
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
    ScrollPanelModule,
    IconFieldModule,
    InputIconModule,
    TagModule,
    AccordionModule,
    TabViewModule,
    PaginatorModule,
    FloatLabelModule,
    ToastModule
  ],
  providers: [
    ConfirmationService,
    ProfileService,
    TextMeasureService,
    AdvertService,
    ChatService,
  ],
})
export class AdminModule {}
