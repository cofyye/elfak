import { Module } from '@nestjs/common';
import { AuthController } from './auth.controller';
import { AuthService } from './auth.service';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from '../../shared/entities/user.entity';
import { EmailModule } from 'src/shared/services/email/email.module';
import { JwtStrategy } from './strategies/jwt.strategy';
import { FileUploadModule } from 'src/shared/services/file-upload/file-upload.module';

@Module({
  imports: [
    TypeOrmModule.forFeature([UserEntity]),
    EmailModule,
    FileUploadModule,
  ],
  controllers: [AuthController],
  providers: [JwtStrategy, AuthService],
})
export class AuthModule {}
