import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';
import { PaymentEntity } from '../entities/payment.entity';
import { StripeController } from './stripe.controller';
import { StripeService } from './stripe.service';

@Module({
  imports: [TypeOrmModule.forFeature([UserEntity, PaymentEntity])],
  controllers: [StripeController],
  providers: [StripeService],
})
export class StripeModule {}
