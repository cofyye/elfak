import { HttpStatus, Injectable } from '@nestjs/common';
import { CreatePaymentDto } from '../dtos/create-payment.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { PaymentEntity } from '../entities/payment.entity';
import { UserEntity } from 'src/shared/entities/user.entity';
import { Request } from 'express';
import { StripeApiService } from 'src/shared/services/payments/stripe/stripe-api.service';
import { SessionIdDto } from '../dtos/session-id.dto';
import { functions } from 'src/shared/utils/functions';
import * as moment from 'moment';

@Injectable()
export class StripeService {
  constructor(
    @InjectRepository(UserEntity)
    private readonly _userRepo: Repository<UserEntity>,
    @InjectRepository(PaymentEntity)
    private readonly _paymentRepo: Repository<PaymentEntity>,
    private readonly _stripeApiService: StripeApiService,
  ) {}

  public async createPayment(
    body: CreatePaymentDto,
    req: Request,
  ): Promise<string> {
    return await this._stripeApiService.createPayment(body, req);
  }

  public async successPayment(
    query: SessionIdDto,
    user: UserEntity,
    req: Request,
  ): Promise<PaymentEntity> {
    try {
      const stripe = await this._stripeApiService.retrievePayment(query);

      let payment = await this._paymentRepo.findOne({
        where: {
          paymentId: query.session_id,
        },
      });

      if (payment) {
        functions.throwHttpException(
          false,
          'Ova narudzbina je vec izvrsena.',
          HttpStatus.CONFLICT,
        );
      }

      payment = new PaymentEntity();
      payment.method = 'Stripe';
      payment.paymentId = query.session_id;
      payment.payerId = 'cus_' + functions.generateRandomString(10);
      payment.status = stripe.status.toUpperCase();
      payment.currency = stripe.currency.toUpperCase();
      payment.amountPaid = (stripe.amount_total / 100).toString();
      payment.userId = user.id;
      payment.tokens = req.session.tokens.toString();
      payment.paymentDate = moment().unix();

      let count = Number(user.tokens);
      count += req.session.tokens;
      user.tokens = count.toString();

      this._userRepo.save(user);

      req.session.destroy((_) => null);

      return await this._paymentRepo.save(this._paymentRepo.create(payment));
    } catch (err) {
      req.session.destroy((_) => null);
      functions.throwHttpException(
        false,
        'Ova narudzbina je vec izvrsena.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }
  }
}
