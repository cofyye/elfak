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
import { PayPalService } from './paypal.service';
import { CreatePaymentDto } from '../dtos/create-payment.dto';
import { CapturePaymentDto } from '../dtos/capture-payment.dto';
import { Request } from 'express';
import { UserEntity } from 'src/shared/entities/user.entity';
import { functions } from 'src/shared/utils/functions';

@Controller('/payment/paypal')
@UseGuards(AuthenticatedGuard)
export class PayPalController {
  constructor(private readonly _paypalService: PayPalService) {}

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
    const link = await this._paypalService.createPayment(body, req);

    return {
      success: true,
      data: link,
      message: 'Uspesno.',
    };
  }

  /**
   * Ova ruta se koristi kako bi se zavrsila narudzbina za tokene.
   * Korisnik unosi token i PayerID.
   * Vraca true ako je narudzbina kreirana, u suprotnom false.
   */
  @Post('/success')
  @HttpCode(HttpStatus.OK)
  public async successPayment(
    @Query() query: CapturePaymentDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const user = req.user as UserEntity;

    const payment = await this._paypalService.successPayment(query, user, req);

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
