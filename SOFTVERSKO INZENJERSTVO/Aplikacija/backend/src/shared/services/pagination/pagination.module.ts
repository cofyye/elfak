import { Module } from '@nestjs/common';
import { PaginationService } from './pagination.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';

@Module({
  imports: [TypeOrmModule.forFeature([UserEntity])],
  providers: [PaginationService],
  exports: [PaginationService],
})
export class PaginationModule {}
