import { HttpStatus, Inject, Injectable } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import { STRIPE_CLIENT } from './constants';
import Stripe from 'stripe';
import { CreatePaymentDto } from 'src/api/payments/dtos/create-payment.dto';
import { functions } from 'src/shared/utils/functions';
import { SessionIdDto } from 'src/api/payments/dtos/session-id.dto';
import { Request } from 'express';

@Injectable()
export class StripeApiService {
  constructor(
    @Inject(STRIPE_CLIENT) private readonly _stripe: Stripe,
    private readonly _configService: ConfigService,
  ) {}

  public async createPayment(
    body: CreatePaymentDto,
    req: Request,
  ): Promise<string> {
    try {
      const price = (1 / 10) * body.tokens * 100;

      const response = await this._stripe.checkout.sessions.create({
        payment_method_types: ['card'],
        line_items: [
          {
            price_data: {
              currency: 'eur',
              product_data: {
                name: `Pitaj Strucnjaka - Uplata ${body.tokens} tokena`,
                description: `Dobijte strucne odgovore na svoja pitanja!`,
              },
              unit_amount: price,
            },
            quantity: 1,
          },
        ],
        mode: 'payment',
        success_url: `${this._configService.get<string>('FRONTEND_URL')}${body.returnUrlPrefix}/success?session_id={CHECKOUT_SESSION_ID}`,
        cancel_url: `${this._configService.get<string>('FRONTEND_URL')}${body.returnUrlPrefix}/cancel`,
      });

      req.session.tokens = body.tokens;
      req.session.save();

      return response.url;
    } catch (err) {
      req.session.destroy((_) => null);
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom generisanja narudzbine.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }
  }

  public async retrievePayment(
    query: SessionIdDto,
  ): Promise<Stripe.Response<Stripe.Checkout.Session>> {
    try {
      return await this._stripe.checkout.sessions.retrieve(query.session_id);
    } catch (err) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom placanja.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }
  }
}
