import { HttpStatus, Injectable } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import { UploadedFile } from 'express-fileupload';
import { functions } from 'src/shared/utils/functions';
import * as path from 'path';
import * as fs from 'fs';

@Injectable()
export class FileUploadService {
  private readonly fullPath = path.join(__dirname, '../../../../uploads');

  constructor(private readonly _configService: ConfigService) {}

  private checkImageSize(image: UploadedFile): void {
    if (
      image.size > this._configService.get<number>('PICTURE_MAX_UPLOAD_SIZE')
    ) {
      functions.throwHttpException(
        false,
        `Velicina Vaseg fajla ${image.name} je ${functions.formatBytes(image.size)}, maksimalna velicina je ${functions.formatBytes(this._configService.get<number>('PICTURE_MAX_UPLOAD_SIZE'))}.`,
        HttpStatus.PAYLOAD_TOO_LARGE,
      );
    }
  }

  private checkImageExtension(image: UploadedFile): void {
    const extensions: Array<string> = this._configService
      .get<string>('PICTURE_EXTENSIONS')
      .split(', ');

    if (!extensions.includes(path.extname(image.name))) {
      functions.throwHttpException(
        false,
        `Ekstenzija slike ${image.name} nije podrzana.`,
        HttpStatus.UNSUPPORTED_MEDIA_TYPE,
      );
    }
  }

  private checkImageInFolder(filename: string): void {
    try {
      if (fs.existsSync(`${this.fullPath}/${filename}`)) {
        functions.throwHttpException(
          false,
          `Slika sa istim imenom vec postoji na serveru.`,
          HttpStatus.CONFLICT,
        );
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom provere duplikata fajla na serveru.',
      );
    }
  }

  private checkVideoSize(video: UploadedFile): void {
    if (video.size > this._configService.get<number>('VIDEO_MAX_UPLOAD_SIZE')) {
      functions.throwHttpException(
        false,
        `Velicina Vaseg fajla ${video.name} je ${functions.formatBytes(video.size)}, maksimalna velicina je ${functions.formatBytes(this._configService.get<number>('VIDEO_MAX_UPLOAD_SIZE'))}.`,
        HttpStatus.PAYLOAD_TOO_LARGE,
      );
    }
  }

  private checkVideoExtension(video: UploadedFile): void {
    const extensions: Array<string> = this._configService
      .get<string>('VIDEO_EXTENSIONS')
      .split(', ');

    if (!extensions.includes(path.extname(video.name))) {
      functions.throwHttpException(
        false,
        `Ekstenzija video zapisa ${video.name} nije podrzana.`,
        HttpStatus.UNSUPPORTED_MEDIA_TYPE,
      );
    }
  }

  private checkVideoInFolder(filename: string): void {
    try {
      if (fs.existsSync(`${this.fullPath}/${filename}`)) {
        functions.throwHttpException(
          false,
          `Video sa istim imenom vec postoji na serveru.`,
          HttpStatus.CONFLICT,
        );
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom provere duplikata fajla na serveru.',
      );
    }
  }

  public async uploadImage(
    prefix: string = '',
    image: UploadedFile | UploadedFile[],
  ): Promise<string | null> {
    try {
      if (Array.isArray(image)) {
        functions.throwHttpException(
          false,
          'Mozete otpremiti samo jednu sliku odjednom.',
          HttpStatus.BAD_REQUEST,
        );
      } else {
        this.checkImageExtension(image);
        this.checkImageSize(image);

        const filename =
          prefix +
          '_' +
          functions.generateRandomString() +
          path.extname(image.name);

        this.checkImageInFolder(filename);

        image.mv(`${this.fullPath}/${filename}`, (err: unknown) => {
          if (err) {
            functions.throwHttpException(
              false,
              `Dogodila se greska prilikom otpremanja ${image.name}.`,
              HttpStatus.INTERNAL_SERVER_ERROR,
            );
          }
        });

        return filename;
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom otpremanja fajla.',
      );
    }
  }

  public async uploadVideo(
    prefix: string = '',
    video: UploadedFile | UploadedFile[],
  ): Promise<string | null> {
    try {
      if (Array.isArray(video)) {
        functions.throwHttpException(
          false,
          'Mozete otpremiti samo jedan video odjednom.',
          HttpStatus.BAD_REQUEST,
        );
      } else {
        this.checkVideoExtension(video);
        this.checkVideoSize(video);

        const filename =
          prefix +
          '_' +
          functions.generateRandomString() +
          path.extname(video.name);

        this.checkVideoInFolder(filename);

        video.mv(`${this.fullPath}/${filename}`, (err: unknown) => {
          if (err) {
            functions.throwHttpException(
              false,
              `Dogodila se greska prilikom otpremanja ${video.name}.`,
              HttpStatus.INTERNAL_SERVER_ERROR,
            );
          }
        });

        return filename;
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        `Doslo je do greske prilikom otpremanja fajla.`,
      );
    }
  }

  public deleteFile(filename: string): void {
    try {
      fs.unlinkSync(`${this.fullPath}/${filename}`);
    } catch (err: unknown) {}
  }
}
