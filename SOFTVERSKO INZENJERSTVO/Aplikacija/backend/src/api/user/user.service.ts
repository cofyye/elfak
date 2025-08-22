import { HttpStatus, Injectable } from '@nestjs/common';
import { Repository } from 'typeorm';
import { UserEntity } from '../../shared/entities/user.entity';
import { InjectRepository } from '@nestjs/typeorm';
import { functions } from 'src/shared/utils/functions';
import { UpdateUsernameDto } from './dtos/update-username.dto';
import { UpdateFirstNameDto } from './dtos/update-first-name.dto';
import { UpdateLastNameDto } from './dtos/update-last-name.dto';
import { UpdatePasswordDto } from './dtos/update-password.dto';
import * as bcrypt from 'bcrypt';
import { UpdateProfessionDto } from './dtos/update-profession.dto';
import fileUpload from 'express-fileupload';
import { FileUploadService } from 'src/shared/services/file-upload/file-upload.service';

@Injectable()
export class UserService {
  constructor(
    @InjectRepository(UserEntity)
    private readonly _userRepo: Repository<UserEntity>,
    private readonly _fileUploadService: FileUploadService,
  ) {}

  public async usernameAvailability(username: string): Promise<boolean> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          username,
        },
      });

      return user ? true : false;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom provere korisnickog imena.',
      );
    }
  }

  public async emailAvailability(email: string): Promise<boolean> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          email,
        },
      });

      return user ? true : false;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom provere e-mail adrese.',
      );
    }
  }

  public async updateUsername(
    id: string,
    body: UpdateUsernameDto,
  ): Promise<UserEntity> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          id,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Korisnik ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      const usernameExist = await this._userRepo.findOne({
        where: { username: body.username },
      });

      if (usernameExist) {
        functions.throwHttpException(
          false,
          'Korisnik sa ovim korisnickim imenom vec postoji.',
          HttpStatus.CONFLICT,
        );
      }

      user.username = body.username;

      return await this._userRepo.save(user);
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom izmene korisnickog imena.',
      );
    }
  }

  public async updateFirstName(
    id: string,
    body: UpdateFirstNameDto,
  ): Promise<UserEntity> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          id,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Korisnik ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      user.firstName = body.firstName;

      return await this._userRepo.save(user);
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom izmene imena.',
      );
    }
  }

  public async updateLastName(
    id: string,
    body: UpdateLastNameDto,
  ): Promise<UserEntity> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          id,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Korisnik ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      user.lastName = body.lastName;

      return await this._userRepo.save(user);
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom izmene prezimena.',
      );
    }
  }

  public async updatePassword(
    id: string,
    body: UpdatePasswordDto,
  ): Promise<UserEntity> {
    try {
      if (body.password !== body.confirmPassword) {
        functions.throwHttpException(
          false,
          'Lozinke se ne podudaraju.',
          HttpStatus.BAD_REQUEST,
        );
      }

      if (body.currentPassword === body.password) {
        functions.throwHttpException(
          false,
          'Lozinke ne smeju biti iste.',
          HttpStatus.BAD_REQUEST,
        );
      }

      const user = await this._userRepo.findOne({
        where: {
          id,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Korisnik ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (!(await bcrypt.compare(body.currentPassword, user.password))) {
        functions.throwHttpException(
          false,
          'Trenutna lozinka nije tacna.',
          HttpStatus.NOT_FOUND,
        );
      }

      const salt = await bcrypt.genSalt(10);
      const password = await bcrypt.hash(body.password, salt);

      user.password = password;

      return await this._userRepo.save(user);
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom izmene lozinke.',
      );
    }
  }

  public async updateProfession(
    id: string,
    body: UpdateProfessionDto,
  ): Promise<UserEntity> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          id,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Korisnik ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      user.bio = body.bio;
      user.profession = body.profession;

      return await this._userRepo.save(user);
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom izmene podataka.',
      );
    }
  }

  public async updateAvatar(
    id: string,
    avatar: fileUpload.UploadedFile,
  ): Promise<UserEntity> {
    let filename = '';
    try {
      const user = await this._userRepo.findOne({
        where: {
          id,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Korisnik ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      filename = await this._fileUploadService.uploadImage('avatar', avatar);

      if (!filename) {
        functions.throwHttpException(
          false,
          'Doslo je do greske prilikom otpremanja slike.',
          HttpStatus.INTERNAL_SERVER_ERROR,
        );
      }

      user.avatar = filename;

      return await this._userRepo.save(user);
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom izmene avatara.',
      );
    }
  }
}
