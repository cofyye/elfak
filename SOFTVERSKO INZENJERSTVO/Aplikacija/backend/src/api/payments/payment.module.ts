import { Module } from '@nestjs/common';
import { PayPalModule } from './paypal/paypal.module';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';
import { StripeModule } from './stripe/stripe.module';
import { PaymentService } from './payment.service';
import { PaymentController } from './payment.controller';
import { PaymentEntity } from './entities/payment.entity';
import { PaginationService } from 'src/shared/services/pagination/pagination.service';
import { PaymentFormHistoryEntity } from './entities/payment-form-history.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      UserEntity,
      PaymentEntity,
      PaymentFormHistoryEntity,
    ]),
    PayPalModule,
    StripeModule,
  ],
  controllers: [PaymentController],
  providers: [PaymentService, PaginationService],
})
export class PaymentModule {}
