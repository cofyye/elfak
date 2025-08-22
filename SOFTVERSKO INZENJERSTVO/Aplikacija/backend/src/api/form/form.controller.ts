import {
  Body,
  Controller,
  Get,
  HttpCode,
  HttpStatus,
  Param,
  Post,
  Req,
  UseGuards,
} from '@nestjs/common';
import { FormSevice } from './form.service';
import { AuthenticatedGuard } from 'src/shared/guards/authenticated.guard';
import { functions } from 'src/shared/utils/functions';
import {
  IDataSendResponse,
  ISendResponse,
} from 'src/shared/interfaces/response.interface';
import { Request } from 'express';
import { UserEntity } from 'src/shared/entities/user.entity';
import { CreateFormDto } from './dtos/create-form.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { FormEntity } from './entities/form.entity';
import { Repository } from 'typeorm';
import { AdvertExpertEntity } from '../advert/entities/advert-expert.entity';
import * as moment from 'moment';
import { DeclineFormDto } from './dtos/decline-form.dto';
import { AcceptFormDto } from './dtos/accept-form.dto';
import { FormEntityExtended } from './interfaces/form.interface';

@Controller('/form')
@UseGuards(AuthenticatedGuard)
export class FormController {
  constructor(
    @InjectRepository(FormEntity)
    private readonly _formRepo: Repository<FormEntity>,
    @InjectRepository(AdvertExpertEntity)
    private readonly _advertExpertRepo: Repository<AdvertExpertEntity>,
    private readonly _formService: FormSevice,
  ) {}

  /**
   * Ova ruta se koristi kako bi korisnik pogledao formu koju je zakazao od strucnjaka.
   * Korisnik poziva ovu rutu.
   * Vraca se forma koja pripada odredjenom korisniku i strucnjaku u chatu.
   */
  @Get('/get/:expertId')
  @HttpCode(HttpStatus.OK)
  public async getForm(
    @Req() req: Request,
    @Param('expertId') expertId: string,
  ): Promise<IDataSendResponse<FormEntity>> {
    try {
      const user: UserEntity = req.user as UserEntity;

      const form = (await this._formService.getForm(
        user.id,
        expertId,
      )) as FormEntityExtended;

      if (form) {
        form.createdAt =
          moment.unix(+form.createdAt).format('DD.MM.YYYY. HH:mm') + 'h';

        form.accepted_client = !!form.accepted_client;
        form.accepted_expert = !!form.accepted_expert;

        form.plans = JSON.parse(form.plans);
        form.advert = JSON.parse(form.advert);

        form.userType = form.clientId == user.id ? 'client' : 'expert';
      }

      return {
        success: true,
        data: form ?? null,
        message: 'Uspesno.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja konsultacije.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi korisnik zakazao konsultaciju za oglas strucnjaku.
   * Korisnik poziva ovu rutu i salje body.
   * Ukoliko je konsultacija zakazana, vraca true, u suprotnom false.
   */
  @Post('/create')
  @HttpCode(HttpStatus.CREATED)
  public async createMessage(
    @Body() body: CreateFormDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    try {
      const user: UserEntity = req.user as UserEntity;

      try {
        JSON.parse(body.plans);
      } catch (err) {
        functions.handleHttpException(
          err,
          false,
          'Planovi nisu prosledjeni u validnom JSON formatu.',
        );
      }

      const form = await this._formService.createForm(user, body);

      if (!form) {
        functions.throwHttpException(
          false,
          'Doslo je do greske prilikom slanja zahteva za konsultaciju.',
          HttpStatus.INTERNAL_SERVER_ERROR,
        );
      }

      return {
        success: true,
        message: 'Uspesno ste poslali zahtev za konsultaciju.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom slanja zahteva za konsultaciju.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi korisnik/strucnjak odbio konsultaciju za oglas.
   * Korisnik/strucnjak poziva ovu rutu i salje body.
   * Ukoliko je konsultacija odbijena, vraca true, u suprotnom false.
   */
  @Post('/decline')
  @HttpCode(HttpStatus.OK)
  public async declineForm(
    @Body() body: DeclineFormDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    try {
      const user: UserEntity = req.user as UserEntity;

      await this._formService.declineForm(user, body);

      return {
        success: true,
        message: 'Uspesno ste odbili konsultaciju.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom odbijanja konsultacije.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi korisnik/strucnjak prihvatio konsultaciju za oglas.
   * Korisnik/strucnjak poziva ovu rutu i salje body.
   * Ukoliko je konsultacija prihvacena, vraca true, u suprotnom false.
   */
  @Post('/accept')
  @HttpCode(HttpStatus.OK)
  public async acceptForm(
    @Body() body: AcceptFormDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    try {
      const user: UserEntity = req.user as UserEntity;

      const acceptFormObj = await this._formService.acceptForm(user, body);

      return {
        success: true,
        message: acceptFormObj.msg,
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom prihvatanja konsultacije.',
      );
    }
  }
}
