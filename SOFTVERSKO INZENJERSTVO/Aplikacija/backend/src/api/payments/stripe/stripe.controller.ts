import {
  Body,
  Controller,
  HttpCode,
  HttpStatus,
  Post,
  Query,
  Req,
  UseGuards,
} from '@nestjs/common';
import {
  IDataSendResponse,
  ISendResponse,
} from 'src/shared/interfaces/response.interface';
import { AuthenticatedGuard } from 'src/shared/guards/authenticated.guard';
import { CreatePaymentDto } from '../dtos/create-payment.dto';
import { Request } from 'express';
import { UserEntity } from 'src/shared/entities/user.entity';
import { SessionIdDto } from '../dtos/session-id.dto';
import { StripeService } from './stripe.service';
import { functions } from 'src/shared/utils/functions';

@Controller('/payment/stripe')
@UseGuards(AuthenticatedGuard)
export class StripeController {
  constructor(private readonly _stripeService: StripeService) {}

  /**
   * Ova ruta se koristi kako bi se kreirala narudzbina za tokene.
   * Korisnik unosi broj tokena.
   * Vraca true ako je narudzbina kreirana, u suprotnom false.
   */
  @Post('/create')
  @HttpCode(HttpStatus.CREATED)
  public async createPayment(
    @Body() body: CreatePaymentDto,
    @Req() req: Request,
  ): Promise<IDataSendResponse<string>> {
    const link = await this._stripeService.createPayment(body, req);

    return {
      success: true,
      data: link,
      message: 'Uspesno.',
    };
  }

  /**
   * Ova ruta se koristi kako bi se zavrsila narudzbina za tokene.
   * Korisnik unosi session_id.
   * Vraca true ako je narudzbina kreirana, u suprotnom false.
   */
  @Post('/success')
  @HttpCode(HttpStatus.OK)
  public async successPayment(
    @Query() query: SessionIdDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const user = req.user as UserEntity;

    const payment = await this._stripeService.successPayment(query, user, req);

    if (!payment) {
      functions.throwHttpException(
        false,
        'Placanje je uspesno ali dogodila se greska prilikom cuvanja.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno.',
    };
  }
}
