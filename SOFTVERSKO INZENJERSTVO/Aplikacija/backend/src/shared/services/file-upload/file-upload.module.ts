import { Module } from '@nestjs/common';
import { FileUploadService } from './file-upload.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';

@Module({
  imports: [TypeOrmModule.forFeature([UserEntity])],
  providers: [FileUploadService],
  exports: [FileUploadService],
})
export class FileUploadModule {}
