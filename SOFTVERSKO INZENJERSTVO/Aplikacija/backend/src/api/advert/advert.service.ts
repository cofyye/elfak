import { HttpStatus, Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { AdvertExpertEntity } from './entities/advert-expert.entity';
import { Repository } from 'typeorm';
import { functions } from 'src/shared/utils/functions';
import { CreateExpertAdvertDto } from './dtos/create-expert-advert.dto';
import { UserEntity } from 'src/shared/entities/user.entity';
import fileUpload from 'express-fileupload';
import { FileUploadService } from 'src/shared/services/file-upload/file-upload.service';
import { TagEntity } from 'src/shared/entities/tag.entity';
import { IPaginationData } from 'src/shared/interfaces/pagination.interface';
import { PaginationQueryDto } from 'src/shared/dtos/pagination-query.dto';
import { PaginationService } from 'src/shared/services/pagination/pagination.service';
import { GradeEntity } from '../grade/entities/grade.entity';
import * as moment from 'moment';
import { AvailabilityToRateEntity } from '../grade/entities/availability-to-rate.entity';
import { EditExpertAdvertDto } from './dtos/edit-expert-advert.dto';

@Injectable()
export class AdvertSevice {
  constructor(
    @InjectRepository(AdvertExpertEntity)
    private readonly _advertExpertRepo: Repository<AdvertExpertEntity>,
    @InjectRepository(GradeEntity)
    private readonly _gradeRepo: Repository<GradeEntity>,
    @InjectRepository(TagEntity)
    private readonly _tagRepo: Repository<TagEntity>,
    @InjectRepository(AvailabilityToRateEntity)
    private readonly _availabilityToRateRepo: Repository<AvailabilityToRateEntity>,
    private readonly _fileUploadService: FileUploadService,
    private readonly _paginationService: PaginationService,
  ) {}

  public async getAllExpertAdverts(
    paginationQuery: PaginationQueryDto,
  ): Promise<IPaginationData<AdvertExpertEntity[]>> {
    try {
      const queryBuilder = this._advertExpertRepo.createQueryBuilder('advert');

      queryBuilder
        .select([
          'advert.id AS id',
          'advert.expertId AS expertId',
          'advert.title AS title',
          'advert.description AS description',
          'advert.image_one AS image_one',
          'advert.image_two AS image_two',
          'advert.video AS video',
          'advert.active AS active',
          'advert.createdAt AS createdAt',
          `JSON_OBJECT(
            'firstName', expert.first_name,
            'lastName', expert.last_name,
            'username', expert.username,
            'bio', expert.bio,
            'profession', expert.profession,
            'avatar', expert.avatar,
            'registrationDate', expert.registration_date
          ) AS expert`,
          'COALESCE(grades.averageGrade, 0) AS averageGrade',
        ])
        .leftJoin('advert.expert', 'expert')
        .leftJoin(
          (subQuery) =>
            subQuery
              .select('grades.ad_id', 'ad_id')
              .addSelect('AVG(grades.grade)', 'averageGrade')
              .from('grades', 'grades')
              .groupBy('grades.ad_id'),
          'grades',
          'grades.ad_id = advert.id',
        )
        .where(
          'advert.active = :active AND (advert.title LIKE :search OR advert.description LIKE :search)',
          {
            active: true,
            search: `%${paginationQuery.search}%`,
          },
        )
        .orderBy(
          `${paginationQuery.sortBy === 'averageGrade' ? 'averageGrade' : 'advert.' + paginationQuery.sortBy}`,
          paginationQuery.order,
        )
        .limit(paginationQuery.take)
        .offset(paginationQuery.skip);

      const adverts = await this._paginationService.find(
        queryBuilder,
        paginationQuery,
      );

      const formattedAdverts: IPaginationData<AdvertExpertEntity[]> = {
        data: [],
        meta: adverts.meta,
      };

      adverts.data.map((advert) => {
        advert.active = !!advert.active;
        advert.expert = JSON.parse(advert.expert as any);
        advert.createdAt =
          moment.unix(+advert.createdAt).format('DD.MM.YYYY. HH:mm') + 'h';

        advert.expert.registrationDate =
          moment
            .unix(+advert.expert.registrationDate)
            .format('DD.MM.YYYY. HH:mm') + 'h';
        formattedAdverts.data.push(advert);
      });

      return formattedAdverts;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja svih oglasa.',
      );
    }
  }

  public async getSingleExpertAdvert(
    user: UserEntity,
    advertId: string,
  ): Promise<AdvertExpertEntity> {
    try {
      const rawAdvert = await this._advertExpertRepo.query(
        `
        SELECT 
          advert.id,
          advert.expert_id as expertId,
          advert.title,
          advert.description,
          advert.image_one,
          advert.image_two,
          advert.video,
          advert.active,
          advert.plans,
          advert.created_at as createdAt,
          JSON_OBJECT(
            'firstName', expert.first_name,
            'lastName', expert.last_name,
            'username', expert.username,
            'bio', expert.bio,
            'profession', expert.profession,
            'avatar', expert.avatar,
            'registrationDate', expert.registration_date
          ) AS expert,
          COALESCE(AVG(grades.grade), 0) AS averageGrade,
          JSON_ARRAYAGG(
            JSON_OBJECT(
              'id', tags.id,
              'name', tags.name
            )
          ) AS tags
        FROM 
          ads_expert as advert
        LEFT JOIN 
          users as expert ON advert.expert_id = expert.id
        LEFT JOIN 
          grades ON grades.ad_id = advert.id
        LEFT JOIN 
          ads_expert_tags as aet ON advert.id = aet.ad_id
        LEFT JOIN 
          tags ON aet.tag_id = tags.id
        WHERE 
          advert.id = ? AND advert.active = ?
        GROUP BY 
          advert.id
        `,
        [advertId, true],
      );

      const advert = rawAdvert[0];

      if (!advert) {
        functions.throwHttpException(
          false,
          'Ovaj oglas ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (!advert.active) {
        functions.throwHttpException(
          false,
          'Ovaj oglas nije aktivan.',
          HttpStatus.CONFLICT,
        );
      }

      let canGrade: boolean = false;
      if (user) {
        const givedGrade = await this._gradeRepo.findOne({
          where: {
            adId: advertId,
            userId: user.id,
          },
        });

        if (givedGrade) {
          canGrade = false;
        } else {
          const availGrade = await this._availabilityToRateRepo.findOne({
            where: {
              adId: advertId,
              userId: user.id,
            },
          });

          if (availGrade) {
            canGrade = true;
          }
        }
      }

      const formattedAdvert: AdvertExpertEntity = {
        id: advert.id,
        expertId: advert.expertId,
        title: advert.title,
        description: advert.description,
        image_one: advert.image_one,
        image_two: advert.image_two,
        video: advert.video,
        active: !!advert.active,
        plans: JSON.parse(advert.plans),
        createdAt: moment.unix(advert.createdAt).format('DD.MM.YYYY.'),
        averageGrade: advert.averageGrade,
        canGrade: canGrade,
        expert: {
          ...JSON.parse(advert.expert),
          registrationDate: moment
            .unix(JSON.parse(advert.expert).registrationDate)
            .format('DD.MM.YYYY.'),
        },
        tags: JSON.parse(advert.tags),
      };

      return formattedAdvert;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja oglasa.',
      );
    }
  }

  public async getCommentsOfExpertAd(
    advertId: string,
    paginationQuery: PaginationQueryDto,
  ): Promise<IPaginationData<GradeEntity[]>> {
    try {
      const advert = await this._advertExpertRepo.findOne({
        where: {
          id: advertId,
        },
      });

      if (!advert) {
        functions.throwHttpException(
          false,
          'Ovaj oglas ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (!advert.active) {
        functions.throwHttpException(
          false,
          'Ovaj oglas nije aktivan.',
          HttpStatus.CONFLICT,
        );
      }

      const queryBuilder = this._gradeRepo.createQueryBuilder('grade');

      queryBuilder
        .select([
          'grade.id AS id',
          'grade.userId AS userId',
          'grade.adId AS adId',
          'grade.grade AS grade',
          'grade.comment AS comment',
          'grade.createdAt AS createdAt',
          `JSON_OBJECT(
            'firstName', user.first_name,
            'lastName', user.last_name,
            'username', user.username,
            'bio', user.bio,
            'profession', user.profession,
            'avatar', user.avatar,
            'registrationDate', user.registration_date
          ) AS user`,
        ])
        .leftJoin('grade.user', 'user')
        .where('grade.adId = :advertId', { advertId })
        .orderBy(`grade.createdAt`, paginationQuery.order)
        .limit(paginationQuery.take)
        .offset(paginationQuery.skip);

      const comments = await this._paginationService.find(
        queryBuilder,
        paginationQuery,
      );

      const formattedComments: IPaginationData<GradeEntity[]> = {
        data: [],
        meta: comments.meta,
      };

      comments.data.map((comment) => {
        comment.user = JSON.parse(comment.user as any);
        comment.user.registrationDate =
          moment
            .unix(+comment.user.registrationDate)
            .format('DD.MM.YYYY. HH:mm') + 'h';
        comment.createdAt =
          moment.unix(+comment.createdAt).format('DD.MM.YYYY. HH:mm') + 'h';

        formattedComments.data.push(comment);
      });

      return formattedComments;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja komentara.',
      );
    }
  }

  public async createExpertAdvert(
    user: UserEntity,
    body: CreateExpertAdvertDto,
    image_one: fileUpload.UploadedFile,
    image_two: fileUpload.UploadedFile,
    video: fileUpload.UploadedFile,
  ): Promise<AdvertExpertEntity> {
    let filename_image_one: string = '';
    let filename_image_two: string = '';
    let filename_video: string = '';

    try {
      const expertAdvert = new AdvertExpertEntity();
      expertAdvert.title = body.title;
      expertAdvert.description = body.description;
      expertAdvert.plans = body.plans;
      expertAdvert.expert = user;

      const tags: TagEntity[] = [];

      for (const item of JSON.parse(body.tags)) {
        const tag = await this._tagRepo.findOne({
          where: {
            id: item,
          },
        });

        if (!tag) {
          functions.throwHttpException(
            false,
            'Jedan od tagova koje ste izabrali nije pronadjen u bazi.',
            HttpStatus.NOT_FOUND,
          );
        }

        if (tags.findIndex((p) => p.id == tag.id) != -1) {
          functions.throwHttpException(
            false,
            'Ne mozete da stavite dva ista taga. Tagovi moraju da budu razliciti.',
            HttpStatus.BAD_REQUEST,
          );
        }

        tags.push(tag);
      }

      expertAdvert.tags = tags;

      if (image_one) {
        filename_image_one = await this._fileUploadService.uploadImage(
          'expert_ad',
          image_one,
        );

        if (!filename_image_one) {
          functions.throwHttpException(
            false,
            'Doslo je do greske prilikom otpremanja prve slike.',
            HttpStatus.INTERNAL_SERVER_ERROR,
          );
        }

        expertAdvert.image_one = filename_image_one;
      }

      if (image_two) {
        filename_image_two = await this._fileUploadService.uploadImage(
          'expert_ad',
          image_two,
        );

        if (!filename_image_two) {
          functions.throwHttpException(
            false,
            'Doslo je do greske prilikom otpremanja druge slike.',
            HttpStatus.INTERNAL_SERVER_ERROR,
          );
        }

        expertAdvert.image_two = filename_image_two;
      }

      if (video) {
        filename_video = await this._fileUploadService.uploadVideo(
          'expert_ad',
          video,
        );

        if (!filename_video) {
          functions.throwHttpException(
            false,
            'Doslo je do greske prilikom otpremanja video zapisa.',
            HttpStatus.INTERNAL_SERVER_ERROR,
          );
        }

        expertAdvert.video = filename_video;
      }

      expertAdvert.createdAt = moment().unix();

      return await this._advertExpertRepo.save(expertAdvert);
    } catch (err) {
      this._fileUploadService.deleteFile(filename_image_one);
      this._fileUploadService.deleteFile(filename_image_two);
      this._fileUploadService.deleteFile(filename_video);

      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom dodavanja oglasa.',
      );
    }
  }
  public async editExpertAdvert(
    user: UserEntity,
    body: EditExpertAdvertDto,
    image_one: fileUpload.UploadedFile,
    image_two: fileUpload.UploadedFile,
    video: fileUpload.UploadedFile,
    advert: AdvertExpertEntity,
  ): Promise<AdvertExpertEntity> {
    let filename_image_one: string = '';
    let filename_image_two: string = '';
    let filename_video: string = '';

    try {
      advert.title = body.title;
      advert.description = body.description;
      advert.plans = body.plans;
      advert.expert = user;

      const tags: TagEntity[] = [];

      for (const item of JSON.parse(body.tags)) {
        const tag = await this._tagRepo.findOne({
          where: {
            id: item,
          },
        });

        if (!tag) {
          functions.throwHttpException(
            false,
            'Jedan od tagova koje ste izabrali nije pronadjen u bazi.',
            HttpStatus.NOT_FOUND,
          );
        }

        if (tags.findIndex((p) => p.id == tag.id) != -1) {
          functions.throwHttpException(
            false,
            'Ne mozete da stavite dva ista taga. Tagovi moraju da budu razliciti.',
            HttpStatus.BAD_REQUEST,
          );
        }

        tags.push(tag);
      }

      advert.tags = tags;

      if (image_one && image_one.name != advert.image_one) {
        filename_image_one = await this._fileUploadService.uploadImage(
          'expert_ad',
          image_one,
        );

        if (!filename_image_one) {
          functions.throwHttpException(
            false,
            'Doslo je do greske prilikom otpremanja prve slike.',
            HttpStatus.INTERNAL_SERVER_ERROR,
          );
        }

        advert.image_one = filename_image_one;
      }

      if (image_two && image_two.name != advert.image_two) {
        filename_image_two = await this._fileUploadService.uploadImage(
          'expert_ad',
          image_two,
        );

        if (!filename_image_two) {
          functions.throwHttpException(
            false,
            'Doslo je do greske prilikom otpremanja druge slike.',
            HttpStatus.INTERNAL_SERVER_ERROR,
          );
        }

        advert.image_two = filename_image_two;
      }

      if (video && video.name != advert.video) {
        filename_video = await this._fileUploadService.uploadVideo(
          'expert_ad',
          video,
        );

        if (!filename_video) {
          functions.throwHttpException(
            false,
            'Doslo je do greske prilikom otpremanja video zapisa.',
            HttpStatus.INTERNAL_SERVER_ERROR,
          );
        }

        advert.video = filename_video;
      }

      return await this._advertExpertRepo.save(advert);
    } catch (err) {
      this._fileUploadService.deleteFile(filename_image_one);
      this._fileUploadService.deleteFile(filename_image_two);
      this._fileUploadService.deleteFile(filename_video);

      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom izmene oglasa.',
      );
    }
  }

  public async deleteExpertAdvert(id: string, userId: string): Promise<void> {
    try {
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

      if (advert.expertId != userId) {
        functions.throwHttpException(
          false,
          'Vi niste vlasnik ovog oglasa.',
          HttpStatus.FORBIDDEN,
        );
      }

      this._fileUploadService.deleteFile(advert.image_one);
      this._fileUploadService.deleteFile(advert.image_two);
      this._fileUploadService.deleteFile(advert.video);

      if (!(await this._advertExpertRepo.delete({ id: advert.id })).affected) {
        functions.throwHttpException(
          false,
          'Doslo je do greske prilikom brisanja oglasa.',
          HttpStatus.INTERNAL_SERVER_ERROR,
        );
      }
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom brisanja oglasa.',
      );
    }
  }

  public async getAllTags(): Promise<TagEntity[]> {
    try {
      return await this._tagRepo.find({
        select: ['id', 'name'],
      });
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja svih tagova.',
      );
    }
  }
}
