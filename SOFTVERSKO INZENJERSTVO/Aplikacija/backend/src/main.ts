import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { ValidationPipe } from '@nestjs/common';
import { ClassValidatorFilter } from './shared/filters/class-validator.filter';
import { TrimPipe } from './shared/pipes/trim.pipe';
import * as cookieParser from 'cookie-parser';
import * as fileUpload from 'express-fileupload';
import * as session from 'express-session';
import { NestExpressApplication } from '@nestjs/platform-express';

async function bootstrap() {
  const app = await NestFactory.create<NestExpressApplication>(AppModule);

  app.useBodyParser('json', { limit: '500mb' });
  app.useBodyParser('urlencoded', { limit: '500mb' });

  app.set('trust proxy', 1);

  app.use(
    session({
      secret: process.env.SESSION_TOKEN,
      cookie: {
        sameSite: 'none',
        secure: true,
        httpOnly: true,
      },
    }),
  );
  app.use(cookieParser());
  app.use(fileUpload());

  app.enableCors({
    origin: process.env.FRONTEND_URL,
    credentials: true,
  });
  app.useGlobalPipes(
    new TrimPipe(),
    new ValidationPipe({
      whitelist: true,
      transform: true,
      transformOptions: { enableImplicitConversion: true },
      stopAtFirstError: true,
    }),
  );
  app.useGlobalFilters(new ClassValidatorFilter());

  await app.listen(3000);
}

bootstrap();
