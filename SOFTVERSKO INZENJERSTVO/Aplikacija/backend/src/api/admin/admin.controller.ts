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
import { AdminSevice } from './admin.service';
import { AuthenticatedGuard } from 'src/shared/guards/authenticated.guard';
import { RoleGuard } from 'src/shared/guards/role.guard';
import { UserRole } from 'src/shared/enums/role.enum';
import { CreateTagDto } from './dtos/create-tag.dto';
import {
  IDataSendResponse,
  ISendResponse,
} from 'src/shared/interfaces/response.interface';
import { TagEntity } from 'src/shared/entities/tag.entity';
import { functions } from 'src/shared/utils/functions';
import { UserEntity } from 'src/shared/entities/user.entity';
import { TicketEntity } from '../ticket/entities/ticket.entity';
import { Request } from 'express';
import { CreateTicketAnswerDto } from '../ticket/dtos/create-ticket-answer.dto';
import { TicketAnswersEntity } from '../ticket/entities/ticket-answers.entity';
import { PaginationQueryDto } from 'src/shared/dtos/pagination-query.dto';
import { IPaginationData } from 'src/shared/interfaces/pagination.interface';

@Controller('/admin')
@UseGuards(new RoleGuard([UserRole.OWNER, UserRole.ADMIN]))
@UseGuards(AuthenticatedGuard)
export class AdminController {
  constructor(private readonly _adminService: AdminSevice) {}

  /**
   * Ova ruta se koristi kako bi admin dodao tag.
   * Admin unosi ime taga.
   * Vraca true ako je tag dodat, u suprotnom false.
   */
  @Post('/tag/create')
  @HttpCode(HttpStatus.CREATED)
  public async createTag(@Body() body: CreateTagDto): Promise<ISendResponse> {
    const tag: TagEntity = await this._adminService.createTag(body);

    if (!tag) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom dodavanja taga.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste dodali tag.',
    };
  }

  /**
   * Ova ruta se koristi kako bi admin izbrisao tag.
   * Admin unosi id taga.
   * Vraca true ako je tag izbrisan, u suprotnom false.
   */
  @Post('/tag/delete/:id')
  @HttpCode(HttpStatus.OK)
  public async deleteTag(@Param() id: string): Promise<ISendResponse> {
    await this._adminService.deleteTag(id);

    return {
      success: true,
      message: 'Uspesno ste izbrisali tag.',
    };
  }

  /**
   * Ova ruta se koristi kako bi admin poslao odgovor na tiket.
   * Admin unosi podatke.
   * Vraca true ako je odgovor poslat, u suprotnom false.
   */
  @Post('/ticket/answer/create')
  @HttpCode(HttpStatus.CREATED)
  public async createTicketAnswer(
    @Body() body: CreateTicketAnswerDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const user: UserEntity = req.user as UserEntity;

    const ticketAnswer: TicketAnswersEntity =
      await this._adminService.createTicketAnswer(user, body);

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
   * Ova ruta se koristi kako bi admin video sve tikete.
   * Admin poziva rutu.
   * Vraca true ako je odgovor poslat, u suprotnom false.
   */
  @Get('/ticket/get')
  @HttpCode(HttpStatus.OK)
  public async getAllTickets(
    @Query() paginationQuery: PaginationQueryDto,
    @Query('status') status: string,
  ): Promise<IDataSendResponse<IPaginationData<TicketEntity[]>>> {
    const tickets = await this._adminService.getAllTickets(
      paginationQuery,
      status,
    );

    return {
      success: true,
      data: tickets,
      message: 'Uspesno.',
    };
  }

  /**
   * Ova ruta se koristi kako bi admin video informacije o tiketu.
   * Admin poziva rutu.
   * Vraca true i informacije o tiketu ako je uspesno, u suprotnom false.
   */
  @Get('/ticket/get/:ticketId/info')
  @HttpCode(HttpStatus.OK)
  public async getTicketInfo(
    @Param('ticketId') ticketId: string,
  ): Promise<IDataSendResponse<TicketEntity>> {
    const ticket: TicketEntity =
      await this._adminService.getTicketInfo(ticketId);

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

  /**
   * Ova ruta se koristi kako bi admin zatvorio tiket.
   * Admin poziva rutu.
   * Vraca true ako je odgovor poslat, u suprotnom false.
   */
  @Post('/ticket/close/:ticketId')
  @HttpCode(HttpStatus.OK)
  public async closeTicket(
    @Req() req: Request,
    @Param('ticketId') ticketId: string,
  ): Promise<ISendResponse> {
    const user: UserEntity = req.user as UserEntity;

    await this._adminService.closeTicket(user, ticketId);

    return {
      success: true,
      message: 'Uspesno ste zatvorili tiket.',
    };
  }

  /**
   * Ova ruta se koristi kako bi admin poslao obavestenje svima koji su preplaceni.
   * Admin poziva rutu.
   * Vraca true ako je obavestenje poslato, u suprotnom false.
   */
  @Post('/email-listing/send')
  @HttpCode(HttpStatus.OK)
  public async sendAllEmailsFromListing(
    @Body('title') title: string,
    @Body('description') description: string,
  ): Promise<ISendResponse> {
    const sended = await this._adminService.sendAllEmailsFromListing(
      title,
      description,
    );

    if (sended) {
      return {
        success: true,
        message: 'Uspesno ste poslali obavestenje.',
      };
    } else {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom slanja obavestenja.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }
  }
}
