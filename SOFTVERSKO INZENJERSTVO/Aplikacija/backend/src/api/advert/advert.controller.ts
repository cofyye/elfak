import {
  Body,
  Controller,
  Get,
  HttpCode,
  HttpStatus,
  Param,
  Post,
  Query,
  Req,
  UseGuards,
} from '@nestjs/common';
import { AdvertSevice } from './advert.service';
import { AdvertExpertEntity } from './entities/advert-expert.entity';
import { CreateExpertAdvertDto } from './dtos/create-expert-advert.dto';
import {
  IDataSendResponse,
  ISendResponse,
} from 'src/shared/interfaces/response.interface';
import { functions } from 'src/shared/utils/functions';
import { AuthenticatedGuard } from 'src/shared/guards/authenticated.guard';
import { Request } from 'express';
import { UserEntity } from 'src/shared/entities/user.entity';
import fileUpload from 'express-fileupload';
import { ExpertGuard } from 'src/shared/guards/expert.guard';
import { TagEntity } from 'src/shared/entities/tag.entity';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { PaginationQueryDto } from 'src/shared/dtos/pagination-query.dto';
import { IPaginationData } from 'src/shared/interfaces/pagination.interface';
import { GradeEntity } from '../grade/entities/grade.entity';
import { IAdvertPlans } from './interfaces/plans.interface';
import * as moment from 'moment';
import { CheckSessionGuard } from 'src/shared/guards/check-session.guard';
import { EditExpertAdvertDto } from './dtos/edit-expert-advert.dto';

@Controller('/advert')
export class AdvertController {
  constructor(
    @InjectRepository(AdvertExpertEntity)
    private readonly _advertExpertRepo: Repository<AdvertExpertEntity>,
    private readonly _advertService: AdvertSevice,
  ) {}

  /**
   * Ova ruta se koristi kako bi strucnjak pogledao sve njegove oglase.
   * Strucnjak poziva ovu rutu.
   * Uzimaju se svi oglasi iz baze kome pripadaju logovanom strucnjaku i vracaju se.
   */
  @UseGuards(ExpertGuard)
  @UseGuards(AuthenticatedGuard)
  @Get('/expert/get/own')
  @HttpCode(HttpStatus.OK)
  public async getAllOwnExpertAdverts(
    @Req() req: Request,
  ): Promise<IDataSendResponse<AdvertExpertEntity[]>> {
    const loggedUser: UserEntity = req.user as UserEntity;

    const expertAds = await this._advertExpertRepo.find({
      where: {
        expertId: loggedUser.id,
      },
    });

    const formattedAds: AdvertExpertEntity[] = expertAds.map((ad) => {
      ad.plans = JSON.parse(ad.plans);

      ad.createdAt =
        moment.unix(+ad.createdAt).format('DD.MM.YYYY. HH:mm') + 'h';

      return ad;
    });

    return {
      success: true,
      data: formattedAds,
      message: 'Uspesno.',
    };
  }

  /**
   * Ova ruta se koristi kako bi vratila sve oglase od svih experata.
   * Korisnik/Gost poziva ovu rutu i vraca mu sve oglase po paginaciji.
   * Vraca true ako su oglasi vraceni, u suprotnom false.
   */
  @Get('/expert/get/all')
  @HttpCode(HttpStatus.OK)
  public async getAllExpertAdverts(
    @Query() paginationQuery: PaginationQueryDto,
  ): Promise<IDataSendResponse<IPaginationData<AdvertExpertEntity[]>>> {
    try {
      const adverts =
        await this._advertService.getAllExpertAdverts(paginationQuery);

      return {
        success: true,
        data: adverts,
        message: 'Uspesno.',
      };
    } catch (err) {
      console.log(err);
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja svih oglasa.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi vratila jedan oglas.
   * Korisnik/Gost poziva ovu rutu i vraca mu sve informacije o oglasu.
   * Vraca true ako je oglas vracen, u suprotnom false.
   */
  @UseGuards(CheckSessionGuard)
  @Get('/expert/get/:advertId')
  @HttpCode(HttpStatus.OK)
  public async getSingleExpertAdvert(
    @Param('advertId') advertId: string,
    @Req() req: Request,
  ): Promise<IDataSendResponse<AdvertExpertEntity>> {
    try {
      const user: UserEntity = req.user as UserEntity;

      const advert = await this._advertService.getSingleExpertAdvert(
        user,
        advertId,
      );

      return {
        success: true,
        data: advert,
        message: 'Uspesno.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja oglasa.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi vratila komentare za oglas.
   * Korisnik/Gost poziva ovu rutu i vraca mu sve informacije o komentaru.
   * Vraca true ako je komentar vracen, u suprotnom false.
   */
  @Get('/expert/get/:advertId/comments')
  @HttpCode(HttpStatus.OK)
  public async getCommentsOfExpertAd(
    @Param('advertId') advertId: string,
    @Query() paginationQuery: PaginationQueryDto,
  ): Promise<IDataSendResponse<IPaginationData<GradeEntity[]>>> {
    try {
      const comments = await this._advertService.getCommentsOfExpertAd(
        advertId,
        paginationQuery,
      );

      return {
        success: true,
        data: comments,
        message: 'Uspesno.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja komentara.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi vratila sve tagove odnosno kategorije.
   * Strucnjak samo poziva ovu rutu.
   * Vraca true ako su tagovi vraceni, u suprotnom false.
   */
  @UseGuards(ExpertGuard)
  @UseGuards(AuthenticatedGuard)
  @Get('/tag/all')
  @HttpCode(HttpStatus.OK)
  public async getAllTags(): Promise<IDataSendResponse<TagEntity[]>> {
    return {
      success: true,
      data: await this._advertService.getAllTags(),
      message: 'Uspesno.',
    };
  }

  /**
   * Ova ruta se koristi kako bi strucnjak dodao oglas.
   * Strucnjak unosi podatke o oglasu.
   * Vraca true ako je oglas dodat, u suprotnom false.
   */
  @UseGuards(ExpertGuard)
  @UseGuards(AuthenticatedGuard)
  @Post('/expert/create')
  @HttpCode(HttpStatus.CREATED)
  public async createExpertAdvert(
    @Body() body: CreateExpertAdvertDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const user: UserEntity = req.user as UserEntity;

    const adCount = await this._advertExpertRepo.count({
      where: {
        expertId: user.id,
      },
    });

    if (adCount >= 3) {
      functions.throwHttpException(
        false,
        'Mozete napraviti maksimalno 3 oglasa.',
        HttpStatus.FORBIDDEN,
      );
    }

    try {
      const tags = JSON.parse(body.tags) as string[];

      if (tags.length < 1) {
        functions.throwHttpException(
          false,
          'Morate izabrati minimalno jedan tag.',
          HttpStatus.BAD_REQUEST,
        );
      }

      if (tags.length > 5) {
        functions.throwHttpException(
          false,
          'Mozete izabrati maksimalno pet tagova.',
          HttpStatus.BAD_REQUEST,
        );
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Tagovi nisu prosledjeni u validnom JSON formatu.',
      );
    }

    try {
      const plans = JSON.parse(body.plans) as IAdvertPlans;

      if (!Number(plans.basic.consultation_time_minutes)) {
        functions.throwHttpException(
          false,
          'Vreme trajanja konsultacija za osnovni plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }
      if (plans.standard && !Number(plans.standard.consultation_time_minutes)) {
        functions.throwHttpException(
          false,
          'Vreme trajanja konsultacija za srednji plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }
      if (plans.premium && !Number(plans.premium.consultation_time_minutes)) {
        functions.throwHttpException(
          false,
          'Vreme trajanja konsultacija za napredni plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }

      if (!Number(plans.basic.consultation_number)) {
        functions.throwHttpException(
          false,
          'Broj konsultacija za osnovni plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }
      if (plans.standard && !Number(plans.standard.consultation_number)) {
        functions.throwHttpException(
          false,
          'Broj konsultacija za srednji plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }
      if (plans.premium && !Number(plans.premium.consultation_number)) {
        functions.throwHttpException(
          false,
          'Broj konsultacija za napredni plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Planovi nisu prosledjeni u validnom JSON formatu.',
      );
    }

    const image_one = req.files?.image_one as fileUpload.UploadedFile;
    const image_two = req.files?.image_two as fileUpload.UploadedFile;
    const video = req.files?.video as fileUpload.UploadedFile;

    if (!image_one && !image_two && !video) {
      functions.throwHttpException(
        false,
        'Morate dodati bar jednu sliku ili video.',
        HttpStatus.BAD_REQUEST,
      );
    }

    const expertAdvert: AdvertExpertEntity =
      await this._advertService.createExpertAdvert(
        user,
        body,
        image_one,
        image_two,
        video,
      );

    if (!expertAdvert) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom dodavanja oglasa.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste dodali oglas.',
    };
  }

  /**
   * Ova ruta se koristi kako bi strucnjak izmenio oglas.
   * Strucnjak unosi podatke o oglasu.
   * Vraca true ako je oglas dodat, u suprotnom false.
   */
  @UseGuards(ExpertGuard)
  @UseGuards(AuthenticatedGuard)
  @Post('/expert/edit/:id')
  @HttpCode(HttpStatus.OK)
  public async editExpertAdvert(
    @Body() body: EditExpertAdvertDto,
    @Param('id') id: string,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const user: UserEntity = req.user as UserEntity;

    const advert = await this._advertExpertRepo.findOne({
      where: {
        id,
      },
    });

    if (!advert) {
      functions.throwHttpException(
        false,
        'Ovaj oglas ne postoji.',
        HttpStatus.NOT_FOUND,
      );
    }

    if (user.id != advert.expertId) {
      functions.throwHttpException(
        false,
        'Niste vlasnik ovog oglasa.',
        HttpStatus.FORBIDDEN,
      );
    }

    try {
      const tags = JSON.parse(body.tags) as string[];

      if (tags.length < 1) {
        functions.throwHttpException(
          false,
          'Morate izabrati minimalno jedan tag.',
          HttpStatus.BAD_REQUEST,
        );
      }

      if (tags.length > 5) {
        functions.throwHttpException(
          false,
          'Mozete izabrati maksimalno pet tagova.',
          HttpStatus.BAD_REQUEST,
        );
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Tagovi nisu prosledjeni u validnom JSON formatu.',
      );
    }

    try {
      const plans = JSON.parse(body.plans) as IAdvertPlans;

      if (!Number(plans.basic.consultation_time_minutes)) {
        functions.throwHttpException(
          false,
          'Vreme trajanja konsultacija za osnovni plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }
      if (plans.standard && !Number(plans.standard.consultation_time_minutes)) {
        functions.throwHttpException(
          false,
          'Vreme trajanja konsultacija za srednji plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }
      if (plans.premium && !Number(plans.premium.consultation_time_minutes)) {
        functions.throwHttpException(
          false,
          'Vreme trajanja konsultacija za napredni plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }

      if (!Number(plans.basic.consultation_number)) {
        functions.throwHttpException(
          false,
          'Broj konsultacija za osnovni plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }
      if (plans.standard && !Number(plans.standard.consultation_number)) {
        functions.throwHttpException(
          false,
          'Broj konsultacija za srednji plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }
      if (plans.premium && !Number(plans.premium.consultation_number)) {
        functions.throwHttpException(
          false,
          'Broj konsultacija za napredni plan mora biti u numerickom formatu.',
          HttpStatus.BAD_REQUEST,
        );
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Planovi nisu prosledjeni u validnom JSON formatu.',
      );
    }

    const image_one = req.files?.image_one as fileUpload.UploadedFile;
    const image_two = req.files?.image_two as fileUpload.UploadedFile;
    const video = req.files?.video as fileUpload.UploadedFile;

    if (!image_one && !image_two && !video) {
      functions.throwHttpException(
        false,
        'Morate dodati bar jednu sliku ili video.',
        HttpStatus.BAD_REQUEST,
      );
    }

    const expertAdvert: AdvertExpertEntity =
      await this._advertService.editExpertAdvert(
        user,
        body,
        image_one,
        image_two,
        video,
        advert,
      );

    if (!expertAdvert) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom izmene oglasa.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste izmenili oglas.',
    };
  }

  /**
   * Ova ruta se koristi kako bi strucnjak izbrisao oglas.
   * Strucnjak unosi id oglasa.
   * Vraca true ako je oglas izbrisan, u suprotnom false.
   */
  @UseGuards(ExpertGuard)
  @UseGuards(AuthenticatedGuard)
  @Post('/expert/delete/:id')
  @HttpCode(HttpStatus.OK)
  public async deleteExpertAdvert(
    @Param('id') advertId: string,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const user: UserEntity = req.user as UserEntity;

    await this._advertService.deleteExpertAdvert(advertId, user.id);

    return {
      success: true,
      message: 'Uspesno ste izbrisali oglas.',
    };
  }
}
