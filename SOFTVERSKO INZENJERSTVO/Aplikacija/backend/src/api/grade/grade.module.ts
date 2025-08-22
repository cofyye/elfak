import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { GradeEntity } from './entities/grade.entity';
import { GradeController } from './grade.controller';
import { GradeSevice } from './grade.service';
import { AdvertExpertEntity } from '../advert/entities/advert-expert.entity';
import { UserEntity } from 'src/shared/entities/user.entity';
import { AvailabilityToRateEntity } from './entities/availability-to-rate.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      UserEntity,
      GradeEntity,
      AdvertExpertEntity,
      AvailabilityToRateEntity,
    ]),
  ],
  controllers: [GradeController],
  providers: [GradeSevice],
})
export class GradeModule {}
