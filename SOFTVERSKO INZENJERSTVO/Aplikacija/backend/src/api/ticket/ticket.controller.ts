import {
  Body,
  Controller,
  Get,
  HttpCode,
  HttpStatus,
  Param,
  Post,
  Query,
  Req,
  UseGuards,
} from '@nestjs/common';
import {
  IDataSendResponse,
  ISendResponse,
} from 'src/shared/interfaces/response.interface';
import { functions } from 'src/shared/utils/functions';
import { AuthenticatedGuard } from 'src/shared/guards/authenticated.guard';
import { Request } from 'express';
import { UserEntity } from 'src/shared/entities/user.entity';
import { TicketSevice } from './ticket.service';
import { TicketEntity } from './entities/ticket.entity';
import { CreateTicketDto } from './dtos/create-ticket.dto';
import { CreateTicketAnswerDto } from './dtos/create-ticket-answer.dto';
import { TicketAnswersEntity } from './entities/ticket-answers.entity';

@Controller('/ticket')
@UseGuards(AuthenticatedGuard)
export class TicketController {
  constructor(private readonly _ticketService: TicketSevice) {}

  /**
   * Ova ruta se koristi kako bi korisnik poslao tiket.
   * Korisnik unosi podatke o tiketu.
   * Vraca true ako je tiket poslat, u suprotnom false.
   */
  @Post('/create')
  @HttpCode(HttpStatus.CREATED)
  public async createTicket(
    @Body() body: CreateTicketDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const user: UserEntity = req.user as UserEntity;

    const ticket: TicketEntity = await this._ticketService.createTicket(
      user,
      body,
    );

    if (!ticket) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom slanja tiketa.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste poslali tiket.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik poslao odgovor na tiket.
   * Korisnik unosi podatke.
   * Vraca true ako je odgovor poslat, u suprotnom false.
   */
  @Post('/answer/create')
  @HttpCode(HttpStatus.CREATED)
  public async createTicketAnswer(
    @Body() body: CreateTicketAnswerDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const user: UserEntity = req.user as UserEntity;

    const ticketAnswer: TicketAnswersEntity =
      await this._ticketService.createTicketAnswer(user, body);

    if (!ticketAnswer) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom slanja odgovora.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste poslali odgovor.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik zatvorio tiket.
   * Korisnik poziva rutu.
   * Vraca true ako je odgovor poslat, u suprotnom false.
   */
  @Post('/close/:ticketId')
  @HttpCode(HttpStatus.OK)
  public async closeTicket(
    @Req() req: Request,
    @Param('ticketId') ticketId: string,
  ): Promise<ISendResponse> {
    const user: UserEntity = req.user as UserEntity;

    await this._ticketService.closeTicket(user, ticketId);

    return {
      success: true,
      message: 'Uspesno ste zatvorili tiket.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik dobio sve tikete.
   * Korisnik poziva rutu.
   * Vraca true i listu tiketa ako je uspesno, u suprotnom false.
   */
  @Get('/get/own')
  @HttpCode(HttpStatus.OK)
  public async getAllOwnTickets(
    @Req() req: Request,
    @Query('sortBy') sortBy: string,
  ): Promise<IDataSendResponse<TicketEntity[]>> {
    const user: UserEntity = req.user as UserEntity;

    const tickets: TicketEntity[] = await this._ticketService.getAllOwnTickets(
      user,
      sortBy,
    );

    return {
      success: true,
      data: tickets,
      message: 'Uspesno.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik video informacije o tiketu.
   * Korisnik poziva rutu.
   * Vraca true i informacije o tiketu ako je uspesno, u suprotnom false.
   */
  @Get('/get/:ticketId/info')
  @HttpCode(HttpStatus.OK)
  public async getTicketInfo(
    @Req() req: Request,
    @Param('ticketId') ticketId: string,
  ): Promise<IDataSendResponse<TicketEntity>> {
    const user: UserEntity = req.user as UserEntity;

    const ticket: TicketEntity = await this._ticketService.getTicketInfo(
      user,
      ticketId,
    );

    if (!ticket) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom vracanja tiketa.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      data: ticket,
      message: 'Uspesno.',
    };
  }
}
