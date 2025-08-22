import { Module } from '@nestjs/common';
import { PayPalApiService } from './paypal-api.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';

@Module({
  imports: [TypeOrmModule.forFeature([UserEntity])],
  providers: [PayPalApiService],
  exports: [PayPalApiService],
})
export class PayPalApiModule {}
