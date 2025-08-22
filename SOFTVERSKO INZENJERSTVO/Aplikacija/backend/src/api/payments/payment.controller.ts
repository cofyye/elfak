import {
  Controller,
  Get,
  HttpCode,
  HttpStatus,
  Query,
  Req,
  UseGuards,
} from '@nestjs/common';
import { AuthenticatedGuard } from 'src/shared/guards/authenticated.guard';
import { PaymentEntity } from './entities/payment.entity';
import { IDataSendResponse } from 'src/shared/interfaces/response.interface';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Request } from 'express';
import { PaginationQueryDto } from 'src/shared/dtos/pagination-query.dto';
import { UserEntity } from 'src/shared/entities/user.entity';
import { PaginationService } from 'src/shared/services/pagination/pagination.service';
import { functions } from 'src/shared/utils/functions';
import { IPaginationData } from 'src/shared/interfaces/pagination.interface';
import * as moment from 'moment';
import { PaymentFormHistoryEntity } from './entities/payment-form-history.entity';

@Controller('/payment')
@UseGuards(AuthenticatedGuard)
export class PaymentController {
  constructor(
    @InjectRepository(PaymentEntity)
    private readonly _paymentRepo: Repository<PaymentEntity>,
    @InjectRepository(PaymentFormHistoryEntity)
    private readonly _paymentFormHistoryRepo: Repository<PaymentFormHistoryEntity>,
    private readonly _paginationService: PaginationService,
  ) {}

  /**
   * Ova ruta se koristi kako bi prikazala korisniku istoriju placanja.
   * Korisnik poziva ovu rutu.
   * Vraca true ako je istorija vracena, u suprotnom false.
   */
  @Get('/get/own')
  @HttpCode(HttpStatus.OK)
  public async getOwnPayments(
    @Query() paginationQuery: PaginationQueryDto,
    @Req() req: Request,
  ): Promise<IDataSendResponse<IPaginationData<PaymentEntity[]>>> {
    try {
      const user: UserEntity = req.user as UserEntity;

      const queryBuilder = this._paymentRepo.createQueryBuilder('payment');

      queryBuilder
        .orderBy(`payment.payment_date`, paginationQuery.order)
        .skip(paginationQuery.skip)
        .take(paginationQuery.take)
        .where('payment.user_id = :userid', { userid: user.id });

      const payments = await this._paginationService.find(
        queryBuilder,
        paginationQuery,
      );

      payments.data = payments.data.map((item) => {
        item.paymentDate =
          moment.unix(+item.paymentDate).format('DD.MM.YYYY. HH:mm') + 'h';

        return item;
      });

      return {
        success: true,
        data: payments,
        message: 'Uspesno.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja svih placanja.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi prikazala korisniku istoriju placanja konsultacija.
   * Korisnik poziva ovu rutu.
   * Vraca true ako je istorija vracena, u suprotnom false.
   */
  @Get('/get/form/own')
  @HttpCode(HttpStatus.OK)
  public async getOwnFormPayments(
    @Query() paginationQuery: PaginationQueryDto,
    @Req() req: Request,
  ): Promise<IDataSendResponse<IPaginationData<PaymentFormHistoryEntity[]>>> {
    try {
      const user: UserEntity = req.user as UserEntity;

      const queryBuilder =
        this._paymentFormHistoryRepo.createQueryBuilder('payment');

      queryBuilder
        .select([
          'payment.id AS id',
          'payment.user_id AS userId',
          'payment.title AS title',
          'payment.tokens AS tokens',
          'payment.payment_date AS paymentDate',
        ])
        .orderBy(`payment.payment_date`, paginationQuery.order)
        .skip(paginationQuery.skip)
        .take(paginationQuery.take)
        .where('payment.user_id = :userid', { userid: user.id });

      const payments = await this._paginationService.find(
        queryBuilder,
        paginationQuery,
      );

      payments.data = payments.data.map((item) => {
        item.paymentDate =
          moment.unix(+item.paymentDate).format('DD.MM.YYYY. HH:mm') + 'h';

        return item;
      });

      return {
        success: true,
        data: payments,
        message: 'Uspesno.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja svih placanja.',
      );
    }
  }
}
