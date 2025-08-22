import { HttpStatus, Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';
import { functions } from 'src/shared/utils/functions';
import { CreateFormDto } from './dtos/create-form.dto';
import { FormEntity } from './entities/form.entity';
import * as moment from 'moment';
import { DeclineFormDto } from './dtos/decline-form.dto';
import { AdvertExpertEntity } from '../advert/entities/advert-expert.entity';
import { AcceptFormDto } from './dtos/accept-form.dto';
import { PlanStatus } from './enums/form.enum';
import { ISendFormAccept } from './interfaces/form.interface';
import { PaymentFormHistoryEntity } from '../payments/entities/payment-form-history.entity';

@Injectable()
export class FormSevice {
  constructor(
    @InjectRepository(FormEntity)
    private readonly _formRepo: Repository<FormEntity>,
    @InjectRepository(UserEntity)
    private readonly _userRepo: Repository<UserEntity>,
    @InjectRepository(AdvertExpertEntity)
    private readonly _advertExpertRepo: Repository<AdvertExpertEntity>,
    @InjectRepository(PaymentFormHistoryEntity)
    private readonly _paymentFormHistoryRepo: Repository<PaymentFormHistoryEntity>,
  ) {}

  public async getForm(
    clientId: string,
    expertId: string,
  ): Promise<FormEntity> {
    try {
      const expert = await this._userRepo.findOne({
        where: {
          id: expertId,
        },
      });

      if (!expert) {
        functions.throwHttpException(
          false,
          'Korisnik ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      const rawQuery = `
        SELECT
          form.id,
          form.ad_id AS adId,
          form.client_id AS clientId,
          form.description,
          form.plans,
          form.accepted_expert,
          form.accepted_client,
          form.accepted_client_chat,
          form.choosed_plan AS choosedPlan,
          form.status,
          form.dateTime,
          form.price,
          form.created_at AS createdAt,
          JSON_OBJECT(
            'title', ad.title
          ) AS advert
        FROM forms form
        INNER JOIN ads_expert ad ON form.ad_id = ad.id
        WHERE form.client_id = ? AND ad.expert_id = ? OR form.client_id = ? AND ad.expert_id = ?
      `;

      const form = await this._formRepo.query(rawQuery, [
        clientId,
        expertId,
        expertId,
        clientId,
      ]);

      return form[0];
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja konsultacije.',
      );
    }
  }

  public async createForm(
    user: UserEntity,
    body: CreateFormDto,
  ): Promise<FormEntity> {
    try {
      const advert = await this._advertExpertRepo.findOne({
        where: {
          id: body.adId,
        },
      });

      if (!advert) {
        functions.throwHttpException(
          false,
          'Oglas ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      const formCheck = await this._formRepo
        .createQueryBuilder('form')
        .innerJoin('ads_expert', 'ad', 'form.adId = ad.id')
        .where('form.clientId = :clientId AND ad.expert_id = :expertId', {
          clientId: user.id,
          expertId: advert.expertId,
        })
        .getOne();

      if (formCheck) {
        functions.throwHttpException(
          false,
          'Vec ste poslali zahtev za konsultaciju kod ovog strucnjaka.',
          HttpStatus.CONFLICT,
        );
      }

      if (user.id == advert.expertId) {
        functions.throwHttpException(
          false,
          'Ne mozete poslati zahtev za konsultaciju sami sebi.',
          HttpStatus.CONFLICT,
        );
      }

      if (
        !moment(body.dateTime).isSameOrAfter(moment().add(1, 'day').toDate())
      ) {
        functions.throwHttpException(
          false,
          'Konsultacija moze biti zakazana najmanje dan posle trenutnog datuma.',
          HttpStatus.BAD_REQUEST,
        );
      }

      const form = new FormEntity();
      form.adId = body.adId;
      form.clientId = user.id;
      form.dateTime = body.dateTime;
      form.description = body.description;
      form.plans = body.plans;
      form.choosedPlan = body.choosedPlan;
      form.createdAt = moment().unix();

      return await this._formRepo.save(this._formRepo.create(form));
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom slanja zahteva za konsultaciju.',
      );
    }
  }

  public async declineForm(
    user: UserEntity,
    body: DeclineFormDto,
  ): Promise<void> {
    try {
      const advert = await this._advertExpertRepo.findOne({
        where: {
          id: body.adId,
        },
      });

      if (!advert) {
        functions.throwHttpException(
          false,
          'Oglas ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      const form = await this._formRepo.findOne({
        where: {
          adId: body.adId,
          clientId: body.clientId,
        },
      });

      if (!form) {
        functions.throwHttpException(
          false,
          'Konsultacija ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (!(advert.expertId == user.id || form.clientId == user.id)) {
        functions.throwHttpException(
          false,
          'Nemate pristup za ovu konsultaciju.',
          HttpStatus.FORBIDDEN,
        );
      }

      await this._formRepo.delete(form);
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom odbijanja konsultacije.',
      );
    }
  }

  public async acceptForm(
    user: UserEntity,
    body: AcceptFormDto,
  ): Promise<ISendFormAccept> {
    try {
      const advert = await this._advertExpertRepo.findOne({
        where: {
          id: body.adId,
        },
      });

      if (!advert) {
        functions.throwHttpException(
          false,
          'Oglas ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      const form = await this._formRepo.findOne({
        where: {
          adId: body.adId,
          clientId: body.clientId,
        },
      });

      if (!form) {
        functions.throwHttpException(
          false,
          'Konsultacija ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (!(advert.expertId == user.id || form.clientId == user.id)) {
        functions.throwHttpException(
          false,
          'Nemate pristup za ovu konsultaciju.',
          HttpStatus.FORBIDDEN,
        );
      }

      if (form.status === PlanStatus.ACCEPTED) {
        functions.throwHttpException(
          false,
          'Ova konsultacija je prihvacena i ne mozete je vise menjati.',
          HttpStatus.FORBIDDEN,
        );
      }

      if (
        (advert.expertId == user.id && form.accepted_expert == true) ||
        (form.clientId == user.id && form.accepted_client == true)
      ) {
        functions.throwHttpException(
          false,
          'Nije Vas red da menjate konsultaciju.',
          HttpStatus.FORBIDDEN,
        );
      }

      if (
        !moment(body.dateTime).isSameOrAfter(moment().add(1, 'day').toDate())
      ) {
        functions.throwHttpException(
          false,
          'Konsultacija moze biti zakazana najmanje dan posle trenutnog datuma.',
          HttpStatus.BAD_REQUEST,
        );
      }

      const edits: string[] = [];

      if (form.description != body.description) {
        form.description = body.description;
        edits.push('description');
      }

      if (form.price != body.price.toString()) {
        form.price = body.price.toString();
        edits.push('price');
      }

      if (moment(form.dateTime).unix() != moment(body.dateTime).unix()) {
        form.dateTime = body.dateTime;
        edits.push('dateTime');
      }

      if (form.plans != body.plans) {
        form.plans = body.plans;
        edits.push('plans');
      }

      let msg = '';

      if (edits.length < 1) {
        if (
          (advert.expertId == user.id && form.accepted_client == true) ||
          (form.clientId == user.id && form.accepted_expert == true)
        ) {
          if (+user.tokens < +form.price) {
            functions.throwHttpException(
              false,
              'Nemate dovoljno tokena. Uplatite tokene kako biste prihvatili ovu konsultaciju.',
              HttpStatus.BAD_REQUEST,
            );
          }

          const expert = await this._userRepo.findOne({
            where: {
              id: advert.expertId,
            },
          });

          user.tokens = (Number(user.tokens) - Number(form.price)).toString();
          expert.tokens = (
            Number(expert.tokens) + Number(form.price)
          ).toString();

          await this._userRepo.save(user);
          await this._userRepo.save(expert);

          const plan = JSON.parse(advert.plans)[`${form.choosedPlan}`];

          if (!plan) {
            functions.throwHttpException(
              false,
              'Doslo je do greske prilikom prihvatanja konsultacije.',
              HttpStatus.BAD_REQUEST,
            );
          }

          form.accepted_client = true;
          form.accepted_expert = true;
          form.endTime = moment(form.dateTime)
            .add(plan['consultation_time_minutes'], 'minutes')
            .toDate();
          form.status = PlanStatus.ACCEPTED;
          msg = 'Uspesno ste prihvatili konsultaciju.';

          const formHistoryPayment = new PaymentFormHistoryEntity();
          formHistoryPayment.title = advert.title;
          formHistoryPayment.tokens = form.price;
          formHistoryPayment.userId = form.clientId;
          formHistoryPayment.paymentDate = moment().unix();

          await this._paymentFormHistoryRepo.save(
            this._paymentFormHistoryRepo.create(formHistoryPayment),
          );
        } else {
          form.accepted_client = !form.accepted_client;
          form.accepted_expert = !form.accepted_expert;
          msg = 'Uspesno ste izmenili konsultaciju.';
        }
      } else {
        form.accepted_client = !form.accepted_client;
        form.accepted_expert = !form.accepted_expert;
        msg = 'Uspesno ste izmenili konsultaciju.';
      }

      if (form.accepted_expert == true && form.accepted_client_chat == false) {
        form.accepted_client_chat = true;
      }

      await this._formRepo.save(form);

      return {
        accepted_client: form.accepted_client,
        accepted_expert: form.accepted_expert,
        accepted_client_chat: form.accepted_client_chat,
        user_type: form.clientId == user.id ? 'client' : 'expert',
        status: form.status,
        msg,
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
