import { HttpStatus, Injectable } from '@nestjs/common';
import { CreateUserDto } from './dtos/create-user.dto';
import { UserEntity } from '../../shared/entities/user.entity';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import * as bcrypt from 'bcrypt';
import * as moment from 'moment';
import { EmailService } from 'src/shared/services/email/email.service';
import { functions } from '../../shared/utils/functions';
import { LoginUserDto } from './dtos/login-user.dto';
import { ChangePasswordDto } from './dtos/change-password.dto';
import { ConfirmVerificationDto } from './dtos/confirm-verification.dto';
import { EmailDto } from './dtos/email.dto';
import { FileUploadService } from 'src/shared/services/file-upload/file-upload.service';
import { UploadedFile } from 'express-fileupload';
import { ResetPasswordDto } from './dtos/reset-password.dto';

@Injectable()
export class AuthService {
  constructor(
    @InjectRepository(UserEntity)
    private readonly _userRepo: Repository<UserEntity>,
    private readonly _emailService: EmailService,
    private readonly _fileUploadService: FileUploadService,
  ) {}

  public async createUser(
    body: CreateUserDto,
    avatar: UploadedFile | UploadedFile[],
  ): Promise<UserEntity> {
    let filename = '';

    try {
      let user = await this._userRepo.findOne({
        where: [{ email: body.email }, { username: body.username }],
      });

      if (user?.email === body.email) {
        functions.throwHttpException(
          false,
          'Korisnik sa ovom e-mail adresom vec postoji.',
          HttpStatus.CONFLICT,
        );
      }

      if (user?.username === body.username) {
        functions.throwHttpException(
          false,
          'Korisnik sa ovim korisnickim imenom vec postoji.',
          HttpStatus.CONFLICT,
        );
      }

      user = new UserEntity();
      user.firstName = body.firstName;
      user.lastName = body.lastName;
      user.email = body.email;
      user.username = body.username;
      user.password = body.password;
      user.isExpert = body.isExpert == 'true' ? true : false;
      user.registrationDate = moment().unix();

      const salt = await bcrypt.genSalt(10);
      const password = await bcrypt.hash(user.password, salt);

      filename = await this._fileUploadService.uploadImage('avatar', avatar);

      if (!filename) {
        functions.throwHttpException(
          false,
          'Doslo je do greske prilikom otpremanja slike.',
          HttpStatus.INTERNAL_SERVER_ERROR,
        );
      }

      user.avatar = filename;
      user.password = password;
      user.verificationToken = functions.generateCode(6);
      user.verificationExpDate = moment().add(1, 'day').toDate();

      if (
        !(await this._emailService.sendConfirmation(
          user.email,
          user.verificationToken,
        ))
      ) {
        functions.throwHttpException(
          false,
          'Doslo je do greske prilikom slanja verifikacionog koda.',
          HttpStatus.FAILED_DEPENDENCY,
        );
      }

      return await this._userRepo.save(this._userRepo.create(user));
    } catch (err) {
      this._fileUploadService.deleteFile(filename);

      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom registracije.',
      );
    }
  }

  public async loginUser(body: LoginUserDto): Promise<UserEntity> {
    try {
      const user = await this._userRepo.findOne({
        where: [{ email: body.email }],
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Neispravan e-mail ili lozinka.',
          HttpStatus.FORBIDDEN,
        );
      }

      if (!user.active) {
        functions.throwHttpExceptionData<{ verified: boolean }>(
          false,
          { verified: false },
          'Molimo Vas da verifikujete nalog.',
          HttpStatus.FORBIDDEN,
        );
      }

      const isMatched = await bcrypt.compare(body.password, user.password);

      if (!isMatched) {
        functions.throwHttpException(
          false,
          'Neispravan e-mail ili lozinka.',
          HttpStatus.FORBIDDEN,
        );
      }

      return user;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Neuspesno prijavljivanje. Pokusajte ponovo.',
      );
    }
  }

  public async confirmVerification(
    query: ConfirmVerificationDto,
  ): Promise<boolean> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          email: query.email,
          verificationToken: query.token,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Nevazeci verifikacioni kod. Pokusajte ponovo.',
          HttpStatus.CONFLICT,
        );
      }

      if (user.active) {
        functions.throwHttpException(
          false,
          'Ovaj nalog je vec verifikovan.',
          HttpStatus.CONFLICT,
        );
      }

      if (moment().isAfter(user.verificationExpDate)) {
        functions.throwHttpException(
          false,
          'Istekao je Vas verifikacioni kod. Molimo Vas da zatrazite novi.',
          HttpStatus.GONE,
        );
      }

      user.verificationToken = null;
      user.verificationExpDate = null;
      user.active = true;

      await this._userRepo.save(user);

      return true;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom verifikacije naloga.',
      );
    }
  }

  public async resendVerification(param: EmailDto): Promise<boolean> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          email: param.email,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Nalog sa ovom e-mail adresom ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (user.active) {
        functions.throwHttpException(
          false,
          'Ovaj nalog je vec verifikovan.',
          HttpStatus.CONFLICT,
        );
      }

      user.verificationToken = functions.generateCode(6);
      user.verificationExpDate = moment().add(1, 'day').toDate();

      await this._userRepo.save(user);

      if (
        !(await this._emailService.sendConfirmation(
          user.email,
          user.verificationToken,
        ))
      ) {
        functions.throwHttpException(
          false,
          'Doslo je do greske prilikom slanja verifikacionog koda.',
          HttpStatus.FAILED_DEPENDENCY,
        );
      }

      return true;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom slanja verifikacionog koda.',
      );
    }
  }

  public async forgotPassword(param: EmailDto) {
    try {
      const user = await this._userRepo.findOne({
        where: {
          email: param.email,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Nalog sa ovom e-mail adresom ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      user.passwordToken = functions.generateCode(12);
      user.passwordExpDate = moment().add(2, 'hours').toDate();

      await this._userRepo.save(user);

      if (
        !(await this._emailService.sendResetPassword(
          user.email,
          user.passwordToken,
        ))
      ) {
        functions.throwHttpException(
          false,
          'Doslo je do greske prilikom slanja zahteva za resetovanje lozinke.',
          HttpStatus.FAILED_DEPENDENCY,
        );
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom slanja zahteva za resetovanje lozinke.',
      );
    }
  }

  public async resetPassword(query: ResetPasswordDto): Promise<boolean> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          email: query.email,
          passwordToken: query.token,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Nevazeci token za resetovanje lozinke. Pokusajte ponovo.',
          HttpStatus.CONFLICT,
        );
      }

      if (moment().isAfter(user.passwordExpDate)) {
        functions.throwHttpException(
          false,
          'Istekao je Vas token za resetovanje lozinke. Molimo Vas da zatrazite novi.',
          HttpStatus.GONE,
        );
      }

      return true;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom provere tokena za resetovanje lozinke.',
      );
    }
  }

  public async changePassword(body: ChangePasswordDto) {
    try {
      if (body.password !== body.confirmPassword) {
        functions.throwHttpException(
          false,
          'Lozinke se ne podudaraju.',
          HttpStatus.BAD_REQUEST,
        );
      }

      const user = await this._userRepo.findOne({
        where: {
          email: body.email,
          passwordToken: body.token,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Nevazeci token za resetovanje lozinke. Pokusajte ponovo.',
          HttpStatus.CONFLICT,
        );
      }

      if (moment().isAfter(user.passwordExpDate)) {
        functions.throwHttpException(
          false,
          'Istekao je Vas token za resetovanje lozinke. Molimo Vas da zatrazite novi.',
          HttpStatus.GONE,
        );
      }

      const salt = await bcrypt.genSalt(10);
      const password = await bcrypt.hash(body.password, salt);

      user.password = password;
      user.passwordToken = null;
      user.passwordExpDate = null;

      await this._userRepo.save(user);

      return true;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom izmene lozinke.',
      );
    }
  }
}
