import { Injectable } from '@nestjs/common';
import { Cron } from '@nestjs/schedule';
import { InjectRepository } from '@nestjs/typeorm';
import { FormEntity } from './api/form/entities/form.entity';
import { Repository } from 'typeorm';
import { AvailabilityToRateEntity } from './api/grade/entities/availability-to-rate.entity';
import * as moment from 'moment';

@Injectable()
export class CronJobService {
  constructor(
    @InjectRepository(FormEntity)
    private readonly _formRepo: Repository<FormEntity>,
    @InjectRepository(AvailabilityToRateEntity)
    private readonly _availibilityToRateRepo: Repository<AvailabilityToRateEntity>,
  ) {}

  @Cron('* * * * *')
  async handleCronFormEndTime() {
    try {
      const rawQuery = `
      SELECT *
      FROM forms
      WHERE status = 'accepted' AND endtime <= NOW();
    `;

      const forms = await this._formRepo.query(rawQuery);

      let count = 0;
      for (const form of forms) {
        const formId = form.id;
        const adId = form.ad_id;
        const clientId = form.client_id;

        const isAvailable = await this._availibilityToRateRepo.findOne({
          where: {
            adId: adId,
            userId: clientId,
          },
        });

        if (!isAvailable) {
          const availabilityToRate = new AvailabilityToRateEntity();
          availabilityToRate.adId = adId;
          availabilityToRate.userId = clientId;
          availabilityToRate.createdAt = moment().unix();

          await this._availibilityToRateRepo.save(
            this._availibilityToRateRepo.create(availabilityToRate),
          );
        }

        const deleteRawQuery = `
        DELETE FROM forms WHERE id = ?
        `;

        await this._formRepo.query(deleteRawQuery, [formId]);

        count++;
      }

      console.log(`Izbrisano ${count} konsultacija.`);
    } catch (err) {
      console.log(err);
    }
  }
}
