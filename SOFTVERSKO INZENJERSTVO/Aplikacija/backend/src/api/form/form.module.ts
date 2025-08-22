import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';
import { FormSevice } from './form.service';
import { FormController } from './form.controller';
import { AdvertExpertEntity } from '../advert/entities/advert-expert.entity';
import { FormEntity } from './entities/form.entity';
import { PaymentFormHistoryEntity } from '../payments/entities/payment-form-history.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      UserEntity,
      AdvertExpertEntity,
      FormEntity,
      PaymentFormHistoryEntity,
    ]),
  ],
  controllers: [FormController],
  providers: [FormSevice],
})
export class FormModule {}
