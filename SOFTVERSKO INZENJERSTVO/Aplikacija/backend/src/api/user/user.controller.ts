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
import {
  IDataSendResponse,
  ISendResponse,
} from 'src/shared/interfaces/response.interface';
import { UserService } from './user.service';
import { UserEntity } from '../../shared/entities/user.entity';
import { Request } from 'express';
import { CheckSessionGuard } from 'src/shared/guards/check-session.guard';
import { UpdateUsernameDto } from './dtos/update-username.dto';
import { AuthenticatedGuard } from 'src/shared/guards/authenticated.guard';
import { functions } from 'src/shared/utils/functions';
import { UpdateFirstNameDto } from './dtos/update-first-name.dto';
import { UpdateLastNameDto } from './dtos/update-last-name.dto';
import { UpdatePasswordDto } from './dtos/update-password.dto';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { UpdateProfessionDto } from './dtos/update-profession.dto';
import { ExpertGuard } from 'src/shared/guards/expert.guard';
import fileUpload from 'express-fileupload';
import * as moment from 'moment';
import { EmailListingEntity } from 'src/shared/entities/email-listing.entity';
import { IPaginationData } from 'src/shared/interfaces/pagination.interface';
import { PaginationQueryDto } from 'src/shared/dtos/pagination-query.dto';
import { GradeEntity } from '../grade/entities/grade.entity';
import { AdvertExpertEntity } from '../advert/entities/advert-expert.entity';
import { PaginationService } from 'src/shared/services/pagination/pagination.service';

@Controller('/users')
export class UserController {
  constructor(
    @InjectRepository(UserEntity)
    private readonly _userRepo: Repository<UserEntity>,
    @InjectRepository(EmailListingEntity)
    private readonly _emailListingRepo: Repository<EmailListingEntity>,
    @InjectRepository(GradeEntity)
    private readonly _gradeRepo: Repository<GradeEntity>,
    private readonly _userService: UserService,
    private readonly _paginationService: PaginationService,
  ) {}

  /**
   * Ova ruta se koristi kako bi iz frontenda proverilo da li postoji username u bazi.
   * Potrebno je da korisnik unese username.
   * Ukoliko je username zauzet, vratice vrednost true, u suprotnom, false.
   */
  @Post('/availability/username/:username')
  @HttpCode(HttpStatus.OK)
  public async usernameAvailability(
    @Param('username') username: string,
  ): Promise<IDataSendResponse<boolean>> {
    return {
      success: true,
      data: await this._userService.usernameAvailability(username),
      message: 'Uspesno.',
    };
  }

  /**
   * Ova ruta se koristi kako bi se iz frontenda proverilo da li postoji email u bazi.
   * Potrebno je da korisnik unese email.
   * Ukoliko je email zauzet, vratice vrednost true, u suprotnom, false.
   */
  @Post('/availability/email/:email')
  @HttpCode(HttpStatus.OK)
  public async emailAvailability(
    @Param('email') email: string,
  ): Promise<IDataSendResponse<boolean>> {
    return {
      success: true,
      data: await this._userService.emailAvailability(email),
      message: 'Uspesno.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik izmenio korisnicko ime.
   * Potrebno je da korisnik prosledi novo korisnicko ime.
   * Ukoliko je korisnicko ime slobodno, i promeni se, vratice true, u suprotnom false.
   */
  @UseGuards(AuthenticatedGuard)
  @Post('/update/username')
  @HttpCode(HttpStatus.OK)
  public async updateUsername(
    @Body() body: UpdateUsernameDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const loggedUser: UserEntity = req.user as UserEntity;

    const isUserChanged: UserEntity = await this._userService.updateUsername(
      loggedUser.id,
      body,
    );

    if (!isUserChanged) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom izmene korisnickog imena.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste izmenili korisnicko ime.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik izmenio ime.
   * Potrebno je da korisnik prosledi novo ime.
   * Ukoliko se ime promeni, vratice true, u suprotnom false.
   */
  @UseGuards(AuthenticatedGuard)
  @Post('/update/firstname')
  @HttpCode(HttpStatus.OK)
  public async updateFirstName(
    @Body() body: UpdateFirstNameDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const loggedUser: UserEntity = req.user as UserEntity;

    const isUserChanged: UserEntity = await this._userService.updateFirstName(
      loggedUser.id,
      body,
    );

    if (!isUserChanged) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom izmene imena.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste izmenili ime.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik izmenio prezime.
   * Potrebno je da korisnik prosledi novo prezime.
   * Ukoliko se prezime promeni, vratice true, u suprotnom false.
   */
  @UseGuards(AuthenticatedGuard)
  @Post('/update/lastname')
  @HttpCode(HttpStatus.OK)
  public async updateLastName(
    @Body() body: UpdateLastNameDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const loggedUser: UserEntity = req.user as UserEntity;

    const isUserChanged: UserEntity = await this._userService.updateLastName(
      loggedUser.id,
      body,
    );

    if (!isUserChanged) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom izmene prezimena.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste izmenili prezime.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik izmenio lozinku.
   * Potrebno je da korisnik prosledi novu lozinku.
   * Ukoliko se lozinka promeni, vratice true, u suprotnom false.
   */
  @UseGuards(AuthenticatedGuard)
  @Post('/update/password')
  @HttpCode(HttpStatus.OK)
  public async updatePassword(
    @Body() body: UpdatePasswordDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const loggedUser: UserEntity = req.user as UserEntity;

    const isUserChanged: UserEntity = await this._userService.updatePassword(
      loggedUser.id,
      body,
    );

    if (!isUserChanged) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom izmene lozinke.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste izmenili lozinku.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik izmenio profesiju.
   * Potrebno je da korisnik prosledi profesiju i biografiju.
   * Ukoliko se profesija promeni, vratice true, u suprotnom false.
   */
  @UseGuards(ExpertGuard)
  @UseGuards(AuthenticatedGuard)
  @Post('/update/profession')
  @HttpCode(HttpStatus.OK)
  public async updateProfession(
    @Body() body: UpdateProfessionDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    const loggedUser: UserEntity = req.user as UserEntity;

    const isUserChanged: UserEntity = await this._userService.updateProfession(
      loggedUser.id,
      body,
    );

    if (!isUserChanged) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom izmene podataka.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste izmenili podatke.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik izmenio avatar.
   * Potrebno je da korisnik prosledi avatar.
   * Ukoliko se avatar promeni, vratice true, u suprotnom false.
   */
  @UseGuards(AuthenticatedGuard)
  @Post('/update/avatar')
  @HttpCode(HttpStatus.OK)
  public async updateAvatar(@Req() req: Request): Promise<ISendResponse> {
    const loggedUser: UserEntity = req.user as UserEntity;

    let avatar = req.files?.avatar as fileUpload.UploadedFile;

    if (!avatar) {
      functions.throwHttpException(
        false,
        'Polje za sliku ne sme biti prazno.',
        HttpStatus.BAD_REQUEST,
      );
    }

    const isUserChanged: UserEntity = await this._userService.updateAvatar(
      loggedUser.id,
      avatar,
    );

    if (!isUserChanged) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom izmene avatara.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste izmenili avatar.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik zatrazio informacije o svom profilu.
   * Potrebno je da korisnik pozove ovu rutu.
   * Ruta vraca informacije o profilu.
   */
  @UseGuards(AuthenticatedGuard)
  @Get('/profile/info')
  @HttpCode(HttpStatus.OK)
  public async getProfileInfo(
    @Req() req: Request,
  ): Promise<IDataSendResponse<UserEntity>> {
    const loggedUser: UserEntity = req.user as UserEntity;

    const user = await this._userRepo.findOne({
      where: {
        id: loggedUser.id,
      },
      select: [
        'firstName',
        'lastName',
        'username',
        'email',
        'role',
        'avatar',
        'isExpert',
        'profession',
        'bio',
        'registrationDate',
        'tokens',
      ],
    });

    if (!user) {
      functions.throwHttpException(
        false,
        'Korisnik ne postoji.',
        HttpStatus.NOT_FOUND,
      );
    }

    return {
      success: true,
      data: {
        ...user,
        registrationDate: moment
          .unix(+user.registrationDate)
          .format('DD.MM.YYYY.'),
      },
      message: 'Uspesno.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisnik zatrazio informacije o necijem profilu.
   * Potrebno je da korisnik pozove ovu rutu.
   * Ruta vraca informacije o profilu.
   */
  @Get('/profile/info/:id')
  @HttpCode(HttpStatus.OK)
  public async getProfileById(
    @Param('id') id: string,
  ): Promise<IDataSendResponse<UserEntity>> {
    try {
      const rawQuery = `
        SELECT
          u.first_name AS firstName,
          u.last_name AS lastName,
          u.username,
          u.email,
          u.role,
          u.avatar,
          u.is_expert AS isExpert,
          u.profession,
          u.bio,
          u.registration_date AS registrationDate,
          (
            SELECT AVG(g.grade)
            FROM grades g
            LEFT JOIN ads_expert ae ON g.ad_id = ae.id
            WHERE ae.expert_id = u.id
          ) AS averageGrade,
          JSON_ARRAYAGG(
            JSON_OBJECT(
              'title', ae.title,
              'description', ae.description,
              'createdAt', ae.created_at,
              'image_one', ae.image_one,
              'image_two', ae.image_two,
              'video', ae.video
            )
          ) AS expert_ads
        FROM users u
        LEFT JOIN ads_expert ae ON u.id = ae.expert_id
        WHERE u.id = ?
        GROUP BY u.id
      `;

      const user = await this._userRepo.query(rawQuery, [id]);

      if (!user) {
        functions.throwHttpException(
          false,
          'Korisnik ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      const usr = user[0];

      usr.isExpert = !!usr.isExpert;
      usr.registrationDate = moment
        .unix(usr.registrationDate)
        .format('DD.MM.YYYY.');
      usr.expert_ads = JSON.parse(usr.expert_ads);

      usr.expert_ads = usr.expert_ads.map((item: any) => {
        item.createdAt = moment.unix(item.createdAt).format('DD.MM.YYYY.');

        item.grades = item.grades?.map((grade: any) => {
          grade.createdAt =
            moment.unix(grade.createdAt).format('DD.MM.YYYY.') + 'h';

          return grade;
        });

        return item;
      });

      return {
        success: true,
        data: usr,
        message: 'Uspesno.',
      };
    } catch (err) {
      console.log(err);
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja profila.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi korisnik zatrazio komentare svih oglasa o necijem profilu.
   * Potrebno je da korisnik pozove ovu rutu.
   * Ruta vraca informacije o profilu.
   */
  @Get('/profile/info/:id/comments')
  @HttpCode(HttpStatus.OK)
  public async getProfileCommentsById(
    @Param('id') id: string,
    @Query() paginationQuery: PaginationQueryDto,
  ): Promise<IDataSendResponse<IPaginationData<any>>> {
    try {
      const queryBuilder = this._gradeRepo
        .createQueryBuilder('g')
        .select([
          'g.grade',
          'u.first_name',
          'u.last_name',
          'u.username',
          'u.avatar',
          'a.image_one',
          'a.image_two',
          'a.video',
          'a.title',
          'g.comment',
          'g.created_at',
        ])
        .innerJoin(UserEntity, 'u', 'g.userId = u.id')
        .innerJoin(AdvertExpertEntity, 'a', 'g.adId = a.id')
        .where('g.userId = :userId', { userId: id })
        .orderBy('g.createdAt', 'DESC')
        .limit(paginationQuery.take)
        .offset(paginationQuery.skip);

      const grades = await this._paginationService.find(
        queryBuilder,
        paginationQuery,
      );

      const formattedGrades = grades.data.map((item: any) => {
        item.created_at = moment.unix(item.created_at).format('DD.MM.YYYY.');

        return item;
      });

      return {
        success: true,
        data: {
          data: formattedGrades,
          meta: grades.meta,
        },
        message: 'Uspesno.',
      };
    } catch (err) {
      console.log(err);
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja profila.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi se iz frontenda proverilo da li korisnik logovan.
   * Potrebno je da korisnik pozove ovu rutu.
   * Ukoliko je logovan, vratice true i informacije, ukoliko nije, false.
   */
  @UseGuards(CheckSessionGuard)
  @Post('/check/session')
  @HttpCode(HttpStatus.OK)
  public async checkUserSession(
    @Req() req: Request,
  ): Promise<IDataSendResponse<Partial<UserEntity>>> {
    const loggedUser: Partial<UserEntity> = req.user;

    if (loggedUser) {
      return {
        success: true,
        data: {
          id: loggedUser.id,
          isExpert: loggedUser.isExpert,
          role: loggedUser.role,
        },
        message: 'Uspesno.',
      };
    } else {
      return {
        success: false,
        data: {},
        message: 'Uspesno.',
      };
    }
  }

  /**
   * Ova ruta se koristi kako bi se korisnik pretplatio u email-listing.
   * Potrebno je da korisnik unese email.
   * Ukoliko je email dodat, vratice vrednost true, u suprotnom, false.
   */
  @Post('/email-listing/:email/add')
  @HttpCode(HttpStatus.CREATED)
  public async addEmailIntoListing(
    @Param('email') email: string,
  ): Promise<ISendResponse> {
    try {
      const isInDb = await this._emailListingRepo.findOne({
        where: {
          email,
        },
      });

      if (isInDb) {
        functions.throwHttpException(
          false,
          'Ovaj email vec postoji u listu.',
          HttpStatus.CONFLICT,
        );
      }

      const emailListing = new EmailListingEntity();
      emailListing.email = email;
      emailListing.createdAt = moment().unix();

      await this._emailListingRepo.save(
        this._emailListingRepo.create(emailListing),
      );

      return {
        success: true,
        message: 'Uspesno ste dodali email u listu.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom dodavanja emaila u listu.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi se vratili 7 strucnjaka sa najvecom ocenom.
   * Potrebno je da korisnik pozove rutu.
   * Ukoliko je vraceno, vratice vrednost true, u suprotnom, false.
   */
  @Get('/get/best-experts')
  @HttpCode(HttpStatus.CREATED)
  public async getExpertsWithBestGrade(): Promise<IDataSendResponse<any>> {
    try {
      const rawQuery = `    
        SELECT 
            g.user_id,
            u.first_name,
            u.last_name,
            u.username,
            u.email,
            u.avatar,
            u.profession,
            u.bio,
            g.grade
        FROM 
            grades g
        JOIN 
            users u ON g.user_id = u.id
        ORDER BY 
            g.grade DESC, g.created_at DESC
        LIMIT 7;
      `;

      const experts = await this._userRepo.query(rawQuery);

      return {
        success: true,
        data: experts,
        message: 'Uspesno.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja strucnjaka.',
      );
    }
  }
}
