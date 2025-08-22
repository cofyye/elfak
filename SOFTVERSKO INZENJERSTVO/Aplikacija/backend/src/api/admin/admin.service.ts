import { HttpStatus, Injectable, Query } from '@nestjs/common';
import { CreateTagDto } from './dtos/create-tag.dto';
import { TagEntity } from 'src/shared/entities/tag.entity';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { functions } from 'src/shared/utils/functions';
import * as moment from 'moment';
import { UserEntity } from 'src/shared/entities/user.entity';
import { TicketEntity } from '../ticket/entities/ticket.entity';
import { TicketAnswersEntity } from '../ticket/entities/ticket-answers.entity';
import { CreateTicketAnswerDto } from '../ticket/dtos/create-ticket-answer.dto';
import { TicketStatus } from '../ticket/enums/ticket.enum';
import { PaginationQueryDto } from 'src/shared/dtos/pagination-query.dto';
import { PaginationService } from 'src/shared/services/pagination/pagination.service';
import { IPaginationData } from 'src/shared/interfaces/pagination.interface';
import { EmailService } from 'src/shared/services/email/email.service';
import { EmailListingEntity } from 'src/shared/entities/email-listing.entity';

@Injectable()
export class AdminSevice {
  constructor(
    @InjectRepository(TagEntity)
    private readonly _tagRepo: Repository<TagEntity>,
    @InjectRepository(TicketEntity)
    private readonly _ticketRepo: Repository<TicketEntity>,
    @InjectRepository(TicketAnswersEntity)
    private readonly _ticketAnswerRepo: Repository<TicketAnswersEntity>,
    @InjectRepository(EmailListingEntity)
    private readonly _emailListingRepo: Repository<EmailListingEntity>,
    private readonly _paginationService: PaginationService,
    private readonly _emailService: EmailService,
  ) {}

  public async createTag(body: CreateTagDto): Promise<TagEntity> {
    try {
      let tag = await this._tagRepo.findOne({
        where: {
          name: body.name,
        },
      });

      if (tag) {
        functions.throwHttpException(
          false,
          'Tag sa ovim imenom vec postoji.',
          HttpStatus.CONFLICT,
        );
      }

      tag.name = body.name;
      tag.createdAt = moment().unix();

      return await this._tagRepo.save(this._tagRepo.create(tag));
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom dodavanja taga.',
      );
    }
  }

  public async deleteTag(id: string): Promise<void> {
    try {
      if (!(await this._tagRepo.delete({ id })).affected) {
        functions.throwHttpException(
          false,
          'Ovaj tag nije pronadjen.',
          HttpStatus.NOT_FOUND,
        );
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom brisanja taga.',
      );
    }
  }

  public async createTicketAnswer(
    user: UserEntity,
    body: CreateTicketAnswerDto,
  ): Promise<TicketAnswersEntity> {
    try {
      const ticket = await this._ticketRepo.findOne({
        where: {
          id: body.ticketId,
        },
      });

      if (!ticket) {
        functions.throwHttpException(
          false,
          'Tiket ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (ticket.status === TicketStatus.CLOSED) {
        functions.throwHttpException(
          false,
          'Ne mozete odgovoriti na tiket koji je zatvoren.',
          HttpStatus.CONFLICT,
        );
      }

      let ticketAnswer = new TicketAnswersEntity();
      ticketAnswer.answeredId = user.id;
      ticketAnswer.ticketId = body.ticketId;
      ticketAnswer.message = body.message;
      ticketAnswer.createdAt = moment().unix();

      return await this._ticketAnswerRepo.save(
        this._ticketAnswerRepo.create(ticketAnswer),
      );
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom odgovora na tiket.',
      );
    }
  }

  public async getTicketInfo(ticketId: string): Promise<TicketEntity> {
    try {
      const ticket = await this._ticketRepo.findOne({
        where: {
          id: ticketId,
        },
        relations: {
          user: true,
        },
        select: {
          user: {
            id: true,
            firstName: true,
            lastName: true,
            username: true,
            avatar: true,
          },
        },
      });

      if (!ticket) {
        functions.throwHttpException(
          false,
          'Tiket ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      const ticketAnswers = await this._ticketAnswerRepo.find({
        where: {
          ticketId: ticket.id,
        },
        relations: {
          user: true,
        },
        order: {
          createdAt: 'ASC',
        },
        select: {
          user: {
            id: true,
            firstName: true,
            lastName: true,
            username: true,
            avatar: true,
          },
        },
      });

      ticket.createdAt =
        moment.unix(+ticket.createdAt).format('DD.MM.YYYY. HH:mm') + 'h';

      const formattedTicketAnswers = ticketAnswers.map((item) => {
        item.createdAt =
          moment.unix(+item.createdAt).format('DD.MM.YYYY. HH:mm') + 'h';

        return item;
      });

      ticket.answers = formattedTicketAnswers;

      return ticket;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja tiketa.',
      );
    }
  }

  public async closeTicket(user: UserEntity, ticketId: string): Promise<void> {
    try {
      const ticket = await this._ticketRepo.findOne({
        where: {
          id: ticketId,
        },
      });

      if (!ticket) {
        functions.throwHttpException(
          false,
          'Tiket ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (ticket.status === TicketStatus.CLOSED) {
        functions.throwHttpException(
          false,
          'Ne mozete zatvoriti tiket koji je vec zatvoren.',
          HttpStatus.CONFLICT,
        );
      }

      ticket.status = TicketStatus.CLOSED;

      await this._ticketRepo.save(ticket);
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom zatvaranja tiketa.',
      );
    }
  }

  public async getAllTickets(
    paginationQuery: PaginationQueryDto,
    status: string,
  ): Promise<IPaginationData<TicketEntity[]>> {
    try {
      const queryBuilder = this._ticketRepo.createQueryBuilder('ticket');

      if (status) {
        queryBuilder
          .select([
            'ticket.id AS id',
            'ticket.title AS title',
            'ticket.message AS message',
            'ticket.created_at AS createdAt',
            'ticket.username AS username',
            'ticket.status AS status',
            'ticket.user_id AS userId',
            'ticket.type AS type',
          ])
          .where('ticket.status = :status', { status })
          .orderBy(`ticket.createdAt`, paginationQuery.order)
          .take(paginationQuery.take)
          .skip(paginationQuery.skip);
      } else {
        queryBuilder
          .select([
            'ticket.id AS id',
            'ticket.title AS title',
            'ticket.message AS message',
            'ticket.created_at AS createdAt',
            'ticket.username AS username',
            'ticket.status AS status',
            'ticket.user_id AS userId',
            'ticket.type AS type',
          ])
          .orderBy(`ticket.createdAt`, paginationQuery.order)
          .take(paginationQuery.take)
          .skip(paginationQuery.skip);
      }

      const tickets = await this._paginationService.find(
        queryBuilder,
        paginationQuery,
      );

      tickets.data = tickets.data.map((item) => {
        item.createdAt =
          moment.unix(+item.createdAt).format('DD.MM.YYYY. HH:mm') + 'h';

        return item;
      });

      return tickets;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja svih tiketa.',
      );
    }
  }

  public async sendAllEmailsFromListing(
    title: string,
    description: string,
  ): Promise<boolean> {
    try {
      if (!title || !description) {
        functions.throwHttpException(
          false,
          'Polja ne smeju biti prazna.',
          HttpStatus.INTERNAL_SERVER_ERROR,
        );
      }

      const emailsListing = await this._emailListingRepo.find();

      const emails = emailsListing.map((item) => item.email).join(',');

      const sended = await this._emailService.sendNotificationsEmailListing(
        emails,
        title,
        description,
      );

      return sended;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom slanja obavestenja.',
      );
    }
  }
}
