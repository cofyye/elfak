import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdvertRoutingModule } from './advert-routing.module';
import { AdvertListComponent } from './advert-list/advert-list.component';
import { AdvertSingleComponent } from './advert-single/advert-single.component';
import { DividerModule } from 'primeng/divider';
import { CardModule } from 'primeng/card';
import { NavBarComponent } from '../../shared/components/navbar/navbar.component';
import { ButtonModule } from 'primeng/button';
import { IndexComponent } from './index.component';
import { SplitterModule } from 'primeng/splitter';
import { FieldsetModule } from 'primeng/fieldset';
import { AdvertCardComponent } from './advert-card/advert-card.component';
import { ImageModule } from 'primeng/image';
import { ChipModule } from 'primeng/chip';
import { RatingModule } from 'primeng/rating';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { FormsModule } from '@angular/forms';
import { PaginatorModule } from 'primeng/paginator';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { FooterComponent } from '../../shared/components/footer/footer.component';
import { GalleriaModule } from 'primeng/galleria';
import { DropdownModule } from 'primeng/dropdown';
import { TabViewModule } from 'primeng/tabview';
import { TreeModule } from 'primeng/tree';
// import { AdvertSingleService } from './services/advert-single.service';
import { DialogModule } from 'primeng/dialog';
import { AdvertService } from './services/advert.service';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { CalendarModule } from 'primeng/calendar';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FloatLabelModule } from 'primeng/floatlabel';
@NgModule({
  declarations: [
    AdvertListComponent,
    AdvertSingleComponent,
    IndexComponent,
    AdvertCardComponent,
  ],
  imports: [
    CommonModule,
    TreeModule,
    AdvertRoutingModule,
    PaginatorModule,
    IconFieldModule,
    InputIconModule,
    DividerModule,
    CardModule,
    ButtonModule,
    TabViewModule,
    FormsModule,
    FieldsetModule,
    SplitterModule,
    ImageModule,
    ChipModule,
    RatingModule,
    AvatarModule,
    AvatarGroupModule,
    NavBarComponent,
    FooterComponent,
    GalleriaModule,
    DropdownModule,
    DialogModule,
    BsDatepickerModule,
    TimepickerModule,
    CalendarModule,
    InputTextareaModule,
    FloatLabelModule,
  ],
  providers: [AdvertService],
})
export class AdvertModule {}
