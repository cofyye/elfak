import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AdvertSevice } from './advert.service';
import { AdvertExpertEntity } from './entities/advert-expert.entity';
import { AdvertController } from './advert.controller';
import { AdvertClientEntity } from './entities/advert-client.entity';
import { FileUploadModule } from 'src/shared/services/file-upload/file-upload.module';
import { TagEntity } from 'src/shared/entities/tag.entity';
import { UserEntity } from 'src/shared/entities/user.entity';
import { PaginationModule } from 'src/shared/services/pagination/pagination.module';
import { GradeEntity } from '../grade/entities/grade.entity';
import { AvailabilityToRateEntity } from '../grade/entities/availability-to-rate.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      AdvertExpertEntity,
      AdvertClientEntity,
      GradeEntity,
      TagEntity,
      UserEntity,
      AvailabilityToRateEntity,
    ]),
    FileUploadModule,
    PaginationModule,
  ],
  controllers: [AdvertController],
  providers: [AdvertSevice],
})
export class AdvertModule {}
