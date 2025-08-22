import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileDisplayComponent } from './profile-display/profile-display.component';
import { NavBarComponent } from '../../shared/components/navbar/navbar.component';
import { ProfileDisplayRoutingModule } from './profile-display-routing.module';
import { FooterComponent } from '../../shared/components/footer/footer.component';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { DividerModule } from 'primeng/divider';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { RatingModule } from 'primeng/rating';
import { TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [ProfileDisplayComponent],
  imports: [
    CommonModule,
    NavBarComponent,
    FooterComponent,
    ProfileDisplayRoutingModule,
    AvatarModule,
    AvatarGroupModule,
    DividerModule,
    CardModule,
    ButtonModule,
    RatingModule,
    TableModule,
    FormsModule,
  ],
})
export class ProfileDisplayModule {}
