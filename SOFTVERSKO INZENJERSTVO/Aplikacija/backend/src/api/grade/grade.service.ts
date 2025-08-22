import { HttpStatus, Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { functions } from 'src/shared/utils/functions';
import { UserEntity } from 'src/shared/entities/user.entity';
import { GradeEntity } from './entities/grade.entity';
import { CreateGradeDto } from './dtos/create-grade.dto';
import { AdvertExpertEntity } from '../advert/entities/advert-expert.entity';
import { EditGradeDto } from './dtos/edit-grade.dto';
import { AvailabilityToRateEntity } from './entities/availability-to-rate.entity';
import * as moment from 'moment';

@Injectable()
export class GradeSevice {
  constructor(
    @InjectRepository(GradeEntity)
    private readonly _gradeRepo: Repository<GradeEntity>,
    @InjectRepository(AdvertExpertEntity)
    private readonly _advertExpertRepo: Repository<AdvertExpertEntity>,
    @InjectRepository(AvailabilityToRateEntity)
    private readonly _availabilityToRateRepo: Repository<AvailabilityToRateEntity>,
  ) {}

  public async createGrade(
    user: UserEntity,
    body: CreateGradeDto,
  ): Promise<GradeEntity> {
    try {
      const avaliable = await this._availabilityToRateRepo.findOne({
        where: {
          adId: body.adId,
          userId: user.id,
        },
      });

      if (!avaliable) {
        functions.throwHttpException(
          false,
          'Nemate pristup da date ocenu za ovaj oglas.',
          HttpStatus.CONFLICT,
        );
      }

      const ad = await this._advertExpertRepo.findOne({
        where: {
          id: body.adId,
        },
      });

      if (!ad) {
        functions.throwHttpException(
          false,
          'Ovaj oglas ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (ad.expertId == user.id) {
        functions.throwHttpException(
          false,
          'Ne mozete dati sami sebi ocenu.',
          HttpStatus.CONFLICT,
        );
      }

      const givedGrade = await this._gradeRepo.findOne({
        where: {
          adId: body.adId,
          userId: user.id,
        },
      });

      if (givedGrade) {
        functions.throwHttpException(
          false,
          'Vec ste dali ocenu za ovaj oglas.',
          HttpStatus.CONFLICT,
        );
      }

      let grade = new GradeEntity();
      grade.adId = body.adId;
      grade.comment = body.comment;
      grade.grade = body.grade;
      grade.userId = user.id;
      grade.createdAt = moment().unix();

      grade = this._gradeRepo.create(grade);

      return await this._gradeRepo.save(grade);
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom davanja ocene.',
      );
    }
  }

  public async createAvailabilityToRate(
    userId: string,
    adId: string,
  ): Promise<AvailabilityToRateEntity> {
    try {
      const avaliable = await this._availabilityToRateRepo.findOne({
        where: {
          adId,
          userId,
        },
      });

      if (avaliable) {
        functions.throwHttpException(
          false,
          'Vec imate pristup da date ocenu za ovaj oglas.',
          HttpStatus.CONFLICT,
        );
      }

      const ad = await this._advertExpertRepo.findOne({
        where: {
          id: adId,
        },
      });

      if (!ad) {
        functions.throwHttpException(
          false,
          'Ovaj oglas ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (ad.expertId == userId) {
        functions.throwHttpException(
          false,
          'Ne mozete dati pristup da sami sebi date ocenu.',
          HttpStatus.CONFLICT,
        );
      }

      return await this._availabilityToRateRepo.save(
        this._availabilityToRateRepo.create({
          adId,
          userId,
          createdAt: moment().unix(),
        }),
      );
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom dodavanja pristupa za ocenu.',
      );
    }
  }

  public async editGrade(id: string, body: EditGradeDto): Promise<GradeEntity> {
    try {
      const grade = await this._gradeRepo.findOne({
        where: {
          id,
        },
      });

      if (!grade) {
        functions.throwHttpException(
          false,
          'Ova ocena ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      grade.grade = body.grade;
      grade.comment = body.comment;

      return await this._gradeRepo.save(grade);
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom izmene ocene.',
      );
    }
  }

  public async deleteGrade(id: string): Promise<void> {
    try {
      if (!(await this._gradeRepo.delete({ id })).affected) {
        functions.throwHttpException(
          false,
          'Doslo je do greske prilikom brisanja ocene.',
          HttpStatus.INTERNAL_SERVER_ERROR,
        );
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom brisanja ocene.',
      );
    }
  }

  public async checkAvailabilityToRate(userId: string, adId: string) {
    try {
      const avaliable = await this._availabilityToRateRepo.findOne({
        where: {
          adId: adId,
          userId: userId,
        },
      });

      if (!avaliable) {
        functions.throwHttpException(
          false,
          'Nemate pristup da date ocenu za ovaj oglas.',
          HttpStatus.CONFLICT,
        );
      }

      const ad = await this._advertExpertRepo.findOne({
        where: {
          id: adId,
        },
      });

      if (!ad) {
        functions.throwHttpException(
          false,
          'Ovaj oglas ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (ad.expertId == userId) {
        functions.throwHttpException(
          false,
          'Ne mozete dati sami sebi ocenu.',
          HttpStatus.CONFLICT,
        );
      }

      return true;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom provere dostupnosti za ocenu.',
      );
    }
  }
}
