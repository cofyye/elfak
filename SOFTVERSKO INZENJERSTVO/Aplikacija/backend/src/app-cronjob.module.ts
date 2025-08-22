import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { CronJobService } from './app-cronjob.service';
import { FormEntity } from './api/form/entities/form.entity';
import { UserEntity } from './shared/entities/user.entity';
import { AvailabilityToRateEntity } from './api/grade/entities/availability-to-rate.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      UserEntity,
      FormEntity,
      AvailabilityToRateEntity,
    ]),
  ],
  controllers: [],
  providers: [CronJobService],
})
export class CronJobModule {}
