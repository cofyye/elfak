import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';
import { ChatSevice } from './chat.service';
import { ChatController } from './chat.controller';
import { ChatEntity } from './entities/chat.entity';
import { ChatGateway } from './chat.gateway';
import { FormSevice } from '../form/form.service';
import { FormEntity } from '../form/entities/form.entity';
import { AdvertExpertEntity } from '../advert/entities/advert-expert.entity';
import { PaymentFormHistoryEntity } from '../payments/entities/payment-form-history.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      UserEntity,
      ChatEntity,
      FormEntity,
      AdvertExpertEntity,
      PaymentFormHistoryEntity,
    ]),
  ],
  controllers: [ChatController],
  providers: [ChatSevice, ChatGateway, FormSevice],
})
export class ChatModule {}
