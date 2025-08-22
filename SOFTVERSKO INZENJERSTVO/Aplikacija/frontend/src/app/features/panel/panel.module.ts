import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpertModule } from './expert/expert.module';
import { ClientModule } from './client/client.module';
import { SharedPaymentModule } from '../../shared/components/payment/shared-payment.module';
import { AdminModule } from './admin/admin.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ExpertModule,
    ClientModule,
    SharedPaymentModule,
    AdminModule,
  ],
})
export class PanelModule {}
