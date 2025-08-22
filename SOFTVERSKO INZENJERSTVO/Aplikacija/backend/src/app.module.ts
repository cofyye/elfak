import { Module } from '@nestjs/common';
import { AuthModule } from './api/auth/auth.module';
import { UserModule } from './api/user/user.module';
import { TypeOrmModule } from '@nestjs/typeorm';
import { ConfigModule, ConfigService } from '@nestjs/config';
import { MailerModule } from '@nestjs-modules/mailer';
import { PassportModule } from '@nestjs/passport';
import { JwtModule } from '@nestjs/jwt';
import { HandlebarsAdapter } from '@nestjs-modules/mailer/dist/adapters/handlebars.adapter';
import { join } from 'path';
import { UserEntity } from './shared/entities/user.entity';
import { AdvertExpertEntity } from './api/advert/entities/advert-expert.entity';
import { AdvertModule } from './api/advert/advert.module';
import { TagEntity } from './shared/entities/tag.entity';
import { AdminModule } from './api/admin/admin.module';
import { GradeEntity } from './api/grade/entities/grade.entity';
import { GradeModule } from './api/grade/grade.module';
import { ServeStaticModule } from '@nestjs/serve-static';
import { AvailabilityToRateEntity } from './api/grade/entities/availability-to-rate.entity';
import { PaymentModule } from './api/payments/payment.module';
import { PaymentEntity } from './api/payments/entities/payment.entity';
import { StripeApiModule } from './shared/services/payments/stripe/stripe-api.module';
import { ChatModule } from './api/chat/chat.module';
import { ChatEntity } from './api/chat/entities/chat.entity';
import { FormEntity } from './api/form/entities/form.entity';
import { FormModule } from './api/form/form.module';
import { TicketEntity } from './api/ticket/entities/ticket.entity';
import { TicketModule } from './api/ticket/ticket.module';
import { TicketAnswersEntity } from './api/ticket/entities/ticket-answers.entity';
import { EmailListingEntity } from './shared/entities/email-listing.entity';
import { PaymentFormHistoryEntity } from './api/payments/entities/payment-form-history.entity';
import { ScheduleModule } from '@nestjs/schedule';
import { CronJobModule } from './app-cronjob.module';

@Module({
  imports: [
    ConfigModule.forRoot({
      isGlobal: true,
      envFilePath: '.env',
    }),
    TypeOrmModule.forRootAsync({
      inject: [ConfigService],
      useFactory: async (configService: ConfigService) => {
        return {
          type: 'mysql',
          host: configService.get<string>('DB_HOSTNAME'),
          username: configService.get<string>('DB_USERNAME'),
          password: configService.get<string>('DB_PASSWORD'),
          database: configService.get<string>('DB_DATABASE'),
          synchronize: true,
          entities: [
            UserEntity,
            TagEntity,
            AdvertExpertEntity,
            GradeEntity,
            AvailabilityToRateEntity,
            PaymentEntity,
            PaymentFormHistoryEntity,
            ChatEntity,
            FormEntity,
            TicketEntity,
            TicketAnswersEntity,
            EmailListingEntity,
          ],
        };
      },
    }),
    MailerModule.forRootAsync({
      inject: [ConfigService],
      useFactory: async (configService: ConfigService) => {
        return {
          transport: {
            host: configService.get<string>('SMTP_HOSTNAME'),
            port: parseInt(configService.get<string>('SMTP_PORT')),
            secure: false,
            logger: false,
            debug: false,
            auth: {
              user: configService.get<string>('SMTP_USERNAME'),
              pass: configService.get<string>('SMTP_PASSWORD'),
            },
          },
          template: {
            dir: join(__dirname, './shared/services/email/templates'),
            adapter: new HandlebarsAdapter(),
            options: {
              strict: true,
            },
          },
        };
      },
    }),
    ServeStaticModule.forRoot({
      rootPath: join(__dirname, '..', 'uploads'),
      serveRoot: '/uploads',
    }),
    ScheduleModule.forRoot(),
    StripeApiModule.forRoot(process.env.STRIPE_KEY, {
      apiVersion: '2024-04-10',
    }),
    PassportModule,
    JwtModule.register({
      global: true,
      secret: process.env.JWT_ACCESS_TOKEN,
      signOptions: {
        expiresIn: process.env.JWT_ACCESS_TOKEN_EXPIRE,
      },
    }),
    AdminModule,
    UserModule,
    AuthModule,
    AdvertModule,
    GradeModule,
    PaymentModule,
    ChatModule,
    FormModule,
    TicketModule,
    CronJobModule,
  ],
  controllers: [],
  providers: [],
})
export class AppModule {}
