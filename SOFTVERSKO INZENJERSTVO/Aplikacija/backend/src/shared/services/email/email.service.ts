import { MailerService } from '@nestjs-modules/mailer';
import { Injectable } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';

import * as moment from 'moment';

@Injectable()
export class EmailService {
  constructor(
    private readonly _mailerService: MailerService,
    private readonly _configService: ConfigService,
  ) {}

  public async sendConfirmation(email: string, verificationToken: string) {
    try {
      await this._mailerService.sendMail({
        from: `${this._configService.get<string>('APP_NAME')} - ${this._configService.get<string>('SMTP_NOREPLY_MAIL')}`,
        to: email,
        subject: `[${this._configService.get<string>('APP_NAME')}] Molimo Vas, verifikujte Vasu e-mail adresu.`,
        template: 'confirm-email',
        context: {
          url: this._configService.get<string>('FRONTEND_URL'),
          email,
          verificationToken,
          year: moment().toDate().getFullYear(),
        },
      });

      return true;
    } catch (_: unknown) {
      console.log(_);
      return false;
    }
  }

  public async sendResetPassword(email: string, passwordToken: string) {
    try {
      await this._mailerService.sendMail({
        from: `${this._configService.get<string>('APP_NAME')} - ${this._configService.get<string>('SMTP_NOREPLY_MAIL')}`,
        to: email,
        subject: `[${this._configService.get<string>('APP_NAME')}] Vracanje naloga.`,
        template: 'reset-password',
        context: {
          url: this._configService.get<string>('FRONTEND_URL'),
          email,
          passwordToken,
          year: moment().toDate().getFullYear(),
        },
      });

      return true;
    } catch (_: unknown) {
      return false;
    }
  }

  public async sendNotificationsEmailListing(
    emails: string,
    title: string,
    description: string,
  ) {
    try {
      await this._mailerService.sendMail({
        from: `${this._configService.get<string>('APP_NAME')} - ${this._configService.get<string>('SMTP_NOREPLY_MAIL')}`,
        to: emails,
        subject: `[${this._configService.get<string>('APP_NAME')}] ${title}`,
        text: description,
        html: description,
      });

      return true;
    } catch (_: unknown) {
      return false;
    }
  }
}
