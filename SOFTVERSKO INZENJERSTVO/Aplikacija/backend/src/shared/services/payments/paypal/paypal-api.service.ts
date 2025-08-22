import { HttpStatus, Injectable } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import { CapturePaymentDto } from 'src/api/payments/dtos/capture-payment.dto';
import { CreatePaymentDto } from 'src/api/payments/dtos/create-payment.dto';
import {
  IPayPalCreateOrderLinks,
  IPayPalTransaction,
} from 'src/shared/interfaces/paypal.interface';
import { functions } from 'src/shared/utils/functions';
import { Request } from 'express';

@Injectable()
export class PayPalApiService {
  constructor(private readonly _configService: ConfigService) {}

  private async generateAccessToken(): Promise<string> {
    let token = '';
    try {
      const response = await fetch(
        `${this._configService.get<string>('PAYPAL_BASE_URL')}/v1/oauth2/token`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            Authorization:
              'Basic ' +
              Buffer.from(
                `${this._configService.get<string>('PAYPAL_CLIENT_ID')}:${this._configService.get<string>('PAYPAL_SECRET')}`,
              ).toString('base64'),
          },
          body: new URLSearchParams({
            grant_type: 'client_credentials',
          }).toString(),
        },
      ).then((res) => res.json());

      token = response['access_token'];
    } catch (err) {
      token = '';
    }

    return token;
  }

  public async createPayment(
    body: CreatePaymentDto,
    req: Request,
  ): Promise<string> {
    try {
      const accessToken = await this.generateAccessToken();

      const price = (1 / 10) * body.tokens;

      const response = await fetch(
        `${this._configService.get<string>('PAYPAL_BASE_URL')}/v2/checkout/orders`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${accessToken}`,
          },
          body: JSON.stringify({
            intent: 'CAPTURE',
            purchase_units: [
              {
                amount: {
                  currency_code: 'EUR',
                  value: price.toString(),
                  breakdown: {
                    item_total: {
                      currency_code: 'EUR',
                      value: price.toString(),
                    },
                  },
                },
                items: [
                  {
                    name: `Pitaj Strucnjaka - Uplata ${body.tokens} tokena`,
                    description: `Dobijte strucne odgovore na svoja pitanja!`,
                    quantity: 1,
                    unit_amount: {
                      currency_code: 'EUR',
                      value: price.toString(),
                    },
                  },
                ],
              },
            ],
            application_context: {
              return_url: `${this._configService.get<string>('FRONTEND_URL')}${body.returnUrlPrefix}/success`,
              cancel_url: `${this._configService.get<string>('FRONTEND_URL')}${body.returnUrlPrefix}/cancel`,
              shipping_preference: 'NO_SHIPPING',
              user_action: 'PAY_NOW',
              brand_name: this._configService.get<string>('APP_NAME'),
            },
          }),
        },
      ).then((res) => res.json());

      const links = response['links'] as IPayPalCreateOrderLinks[];

      req.session.tokens = body.tokens;
      req.session.save();

      return links.find((item) => item.rel === 'approve').href;
    } catch (err) {
      req.session.destroy((_) => null);
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom generisanja narudzbine.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }
  }

  public async capturePayment(
    query: CapturePaymentDto,
  ): Promise<IPayPalTransaction> {
    try {
      const accessToken = await this.generateAccessToken();

      const response = await fetch(
        `${this._configService.get<string>('PAYPAL_BASE_URL')}/v2/checkout/orders/${query.token}/capture`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${accessToken}`,
          },
        },
      ).then((res) => res.json());

      return response as IPayPalTransaction;
    } catch (err) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom placanja.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }
  }
}
