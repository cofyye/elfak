import { NgModule } from '@angular/core';
import { AuthModule } from './auth/auth.module';
import { PanelModule } from './panel/panel.module';
import { AdvertModule } from './advert/advert.module';
import { ProfileDisplayModule } from './profile-display/profile-display.module';

@NgModule({
  declarations: [],
  imports: [AuthModule, PanelModule, AdvertModule, ProfileDisplayModule],
  exports: [],
})
export class FeaturesModule {}
