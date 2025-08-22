import { Module } from '@nestjs/common';
import { UserController } from './user.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from '../../shared/entities/user.entity';
import { UserService } from './user.service';
import { FileUploadService } from 'src/shared/services/file-upload/file-upload.service';
import { EmailListingEntity } from 'src/shared/entities/email-listing.entity';
import { GradeEntity } from '../grade/entities/grade.entity';
import { PaginationService } from 'src/shared/services/pagination/pagination.service';

@Module({
  imports: [
    TypeOrmModule.forFeature([UserEntity, EmailListingEntity, GradeEntity]),
  ],
  controllers: [UserController],
  providers: [UserService, FileUploadService, PaginationService],
})
export class UserModule {}
