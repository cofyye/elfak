import { Module } from '@nestjs/common';
import { PayPalService } from './paypal.service';
import { PayPalApiModule } from 'src/shared/services/payments/paypal/paypal-api.module';
import { PayPalController } from './paypal.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';
import { PaymentEntity } from '../entities/payment.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([UserEntity, PaymentEntity]),
    PayPalApiModule,
  ],
  controllers: [PayPalController],
  providers: [PayPalService],
})
export class PayPalModule {}
