import { HttpStatus, Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { functions } from 'src/shared/utils/functions';
import { UserEntity } from 'src/shared/entities/user.entity';
import * as moment from 'moment';
import { TicketEntity } from './entities/ticket.entity';
import { CreateTicketDto } from './dtos/create-ticket.dto';
import { TicketStatus, TicketType } from './enums/ticket.enum';
import { CreateTicketAnswerDto } from './dtos/create-ticket-answer.dto';
import { TicketAnswersEntity } from './entities/ticket-answers.entity';

@Injectable()
export class TicketSevice {
  constructor(
    @InjectRepository(TicketEntity)
    private readonly _ticketRepo: Repository<TicketEntity>,
    @InjectRepository(TicketAnswersEntity)
    private readonly _ticketAnswerRepo: Repository<TicketAnswersEntity>,
  ) {}

  public async createTicket(
    user: UserEntity,
    body: CreateTicketDto,
  ): Promise<TicketEntity> {
    try {
      if (body.type === TicketType.USER_PROBLEM && !body.username) {
        functions.throwHttpException(
          false,
          'Korisnicko ime ne sme biti prazno.',
          HttpStatus.BAD_REQUEST,
        );
      }

      let ticket = new TicketEntity();
      ticket.message = body.message;
      ticket.title = body.title;
      ticket.type = body.type;

      if (body.type === TicketType.USER_PROBLEM) {
        ticket.username = body.username;
      }

      ticket.userId = user.id;
      ticket.createdAt = moment().unix();

      return await this._ticketRepo.save(this._ticketRepo.create(ticket));
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom slanja tiketa.',
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

      if (ticket.userId !== user.id) {
        functions.throwHttpException(
          false,
          'Nemate dozvolu da odgovorite na ovaj tiket.',
          HttpStatus.FORBIDDEN,
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

      if (ticket.userId !== user.id) {
        functions.throwHttpException(
          false,
          'Nemate dozvolu da zatvorite ovaj tiket.',
          HttpStatus.FORBIDDEN,
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

  public async getAllOwnTickets(
    user: UserEntity,
    sortBy: string,
  ): Promise<TicketEntity[]> {
    try {
      let tickets: TicketEntity[] = [];

      if (
        sortBy == TicketStatus.OPEN ||
        sortBy == TicketStatus.CLOSED ||
        sortBy == TicketStatus.ANSWERED
      ) {
        tickets = await this._ticketRepo.find({
          where: {
            userId: user.id,
            status: sortBy,
          },
          order: {
            createdAt: 'DESC',
          },
          take: 50,
        });
      } else {
        tickets = await this._ticketRepo.find({
          where: {
            userId: user.id,
          },
          order: {
            createdAt: 'DESC',
          },
          take: 50,
        });
      }

      tickets.map((item) => {
        item.createdAt =
          moment.unix(+item.createdAt).format('DD.MM.YYYY. HH:mm') + 'h';
      });

      return tickets;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja tiketa.',
      );
    }
  }

  public async getTicketInfo(
    user: UserEntity,
    ticketId: string,
  ): Promise<TicketEntity> {
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

      if (ticket.userId != user.id) {
        functions.throwHttpException(
          false,
          'Nemate pristup ovom tiketu.',
          HttpStatus.FORBIDDEN,
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
}
