import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { TicketController } from './ticket.controller';
import { TicketSevice } from './ticket.service';
import { UserEntity } from 'src/shared/entities/user.entity';
import { TicketEntity } from './entities/ticket.entity';
import { TicketAnswersEntity } from './entities/ticket-answers.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([UserEntity, TicketEntity, TicketAnswersEntity]),
  ],
  controllers: [TicketController],
  providers: [TicketSevice],
})
export class TicketModule {}
