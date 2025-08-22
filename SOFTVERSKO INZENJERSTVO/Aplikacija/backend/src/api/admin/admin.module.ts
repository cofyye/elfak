import { Module } from '@nestjs/common';
import { AdminController } from './admin.controller';
import { AdminSevice } from './admin.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { TagEntity } from 'src/shared/entities/tag.entity';
import { UserEntity } from 'src/shared/entities/user.entity';
import { TicketEntity } from '../ticket/entities/ticket.entity';
import { TicketAnswersEntity } from '../ticket/entities/ticket-answers.entity';
import { PaginationService } from 'src/shared/services/pagination/pagination.service';
import { EmailListingEntity } from 'src/shared/entities/email-listing.entity';
import { EmailService } from 'src/shared/services/email/email.service';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      TagEntity,
      UserEntity,
      TicketEntity,
      TicketAnswersEntity,
      EmailListingEntity,
    ]),
  ],
  controllers: [AdminController],
  providers: [AdminSevice, PaginationService, EmailService],
})
export class AdminModule {}
