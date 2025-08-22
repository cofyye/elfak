import {
  Body,
  Controller,
  Get,
  HttpCode,
  HttpStatus,
  Param,
  Post,
  Req,
  UseGuards,
} from '@nestjs/common';
import { ISendResponse } from 'src/shared/interfaces/response.interface';
import { functions } from 'src/shared/utils/functions';
import { AuthenticatedGuard } from 'src/shared/guards/authenticated.guard';
import { Request } from 'express';
import { UserEntity } from 'src/shared/entities/user.entity';
import { GradeEntity } from './entities/grade.entity';
import { GradeSevice } from './grade.service';
import { CreateGradeDto } from './dtos/create-grade.dto';
import { EditGradeDto } from './dtos/edit-grade.dto';
import { AvailabilityToRateEntity } from './entities/availability-to-rate.entity';

@Controller('/grade')
@UseGuards(AuthenticatedGuard)
export class GradeController {
  constructor(private readonly _gradeService: GradeSevice) {}

  /**
   * Ova ruta se koristi kako bi se strucnjaku dodala ocena za oglas.
   * Korisnik unosi podatke o oceni.
   * Vraca true ako je ocena dodata, u suprotnom false.
   */
  @Post('/create')
  @HttpCode(HttpStatus.CREATED)
  public async createGrade(
    @Body() body: CreateGradeDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const user: UserEntity = req.user as UserEntity;

    const grade: GradeEntity = await this._gradeService.createGrade(user, body);

    if (!grade) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom davanja ocene.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste dali ocenu.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik imao pristup da da ocenu oglasu.
   * Poziva se ova ruta da doda dostupnost, od parametra userId i adId.
   * Vraca true ako je dostupnost dodata, u suprotnom false.
   */
  @Post('/availability-to-rate/create/:adId')
  @HttpCode(HttpStatus.CREATED)
  public async availabilityToRateCreate(
    @Param('adId') adId: string,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const user: UserEntity = req.user as UserEntity;

    const availabilityToRate: AvailabilityToRateEntity =
      await this._gradeService.createAvailabilityToRate(user.id, adId);

    if (!availabilityToRate) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom dodavanja pristupa za ocenu.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste dodali pristup korisniku da da ocenu.',
    };
  }

  /**
   * Ova ruta se koristi kako bi se obavila provera da li se moze dati ocena.
   * Poziva se ova ruta sa ID oglasa.
   * Vraca true ako je dostupnost true, u suprotnom false.
   */
  @Get('/availability-to-rate/check/:adId')
  @HttpCode(HttpStatus.OK)
  public async checkAvailabilityToRate(
    @Param('adId') adId: string,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const user: UserEntity = req.user as UserEntity;

    const availabilityToRate: boolean =
      await this._gradeService.checkAvailabilityToRate(user.id, adId);

    if (!availabilityToRate) {
      functions.throwHttpException(
        false,
        'Nemate pristup da date ocenu za ovaj oglas.',
        HttpStatus.CONFLICT,
      );
    }

    return {
      success: true,
      message: 'Uspesno.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik izmenio ocenu.
   * Korisnik unosi id ocene.
   * Vraca true ako je ocena izmenjena, u suprotnom false.
   */
  @Post('/edit/:id')
  @HttpCode(HttpStatus.OK)
  public async editGrade(
    @Param('id') id: string,
    @Body() body: EditGradeDto,
  ): Promise<ISendResponse> {
    const updatedGrade = await this._gradeService.editGrade(id, body);

    if (!updatedGrade) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom izmene ocene.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste izmenili ocenu.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik izbrisao ocenu.
   * Korisnik unosi id ocene.
   * Vraca true ako je ocena izbrisana, u suprotnom false.
   */
  @Post('/delete/:id')
  @HttpCode(HttpStatus.OK)
  public async deleteGrade(@Param('id') id: string): Promise<ISendResponse> {
    await this._gradeService.deleteGrade(id);

    return {
      success: true,
      message: 'Uspesno ste izbrisali ocenu.',
    };
  }
}
