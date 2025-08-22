import { HttpStatus, Injectable } from '@nestjs/common';
import { PayPalApiService } from 'src/shared/services/payments/paypal/paypal-api.service';
import { CreatePaymentDto } from '../dtos/create-payment.dto';
import { CapturePaymentDto } from '../dtos/capture-payment.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { PaymentEntity } from '../entities/payment.entity';
import { functions } from 'src/shared/utils/functions';
import { UserEntity } from 'src/shared/entities/user.entity';
import { Request } from 'express';
import * as moment from 'moment';

@Injectable()
export class PayPalService {
  constructor(
    @InjectRepository(UserEntity)
    private readonly _userRepo: Repository<UserEntity>,
    @InjectRepository(PaymentEntity)
    private readonly _paymentRepo: Repository<PaymentEntity>,
    private readonly _paypalApiService: PayPalApiService,
  ) {}

  public async createPayment(
    body: CreatePaymentDto,
    req: Request,
  ): Promise<string> {
    return await this._paypalApiService.createPayment(body, req);
  }

  public async successPayment(
    query: CapturePaymentDto,
    user: UserEntity,
    req: Request,
  ): Promise<PaymentEntity> {
    try {
      const paypal = await this._paypalApiService.capturePayment(query);

      let payment = await this._paymentRepo.findOne({
        where: {
          paymentId: query.token,
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
      payment.method = 'PayPal';
      payment.paymentId = query.token;
      payment.payerId = query.PayerID;
      payment.status = paypal.status;
      payment.currency =
        paypal.purchase_units[0].payments.captures[0].amount.currency_code;
      payment.amountPaid =
        paypal.purchase_units[0].payments.captures[0].amount.value;
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
