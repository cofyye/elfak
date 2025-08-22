import {
  Body,
  Controller,
  HttpCode,
  HttpStatus,
  Param,
  Post,
  Query,
  Req,
  Res,
  UseGuards,
} from '@nestjs/common';
import {
  IDataSendResponse,
  ISendResponse,
} from 'src/shared/interfaces/response.interface';
import { AuthService } from './auth.service';
import { CreateUserDto } from './dtos/create-user.dto';
import { UserEntity } from '../../shared/entities/user.entity';
import { functions } from 'src/shared/utils/functions';
import { LoginUserDto } from './dtos/login-user.dto';
import { ChangePasswordDto } from './dtos/change-password.dto';
import { ConfirmVerificationDto } from './dtos/confirm-verification.dto';
import { EmailDto } from './dtos/email.dto';
import { JwtService } from '@nestjs/jwt';
import { IJwtPayload } from 'src/shared/interfaces/jwt.interface';
import { Request, Response } from 'express';
import { ConfigService } from '@nestjs/config';
import { Repository } from 'typeorm';
import { InjectRepository } from '@nestjs/typeorm';
import { NotAuthenticatedGuard } from 'src/shared/guards/not-authenticated.guard';
import { ResetPasswordDto } from './dtos/reset-password.dto';
import { AuthenticatedGuard } from 'src/shared/guards/authenticated.guard';

@Controller('/auth')
export class AuthController {
  constructor(
    @InjectRepository(UserEntity)
    private readonly _userRepo: Repository<UserEntity>,
    private readonly _authService: AuthService,
    private readonly _jwtService: JwtService,
    private readonly _configService: ConfigService,
  ) {}

  /**
   * Ova ruta se koristi kako bi korisnik kreirao nalog.
   * Korisnik unosi potrebne podatke, te kreira nalog.
   * Nakon kreiranja naloga, korisniku na email stize verifikacioni kod
   */
  @UseGuards(NotAuthenticatedGuard)
  @Post('/signup')
  @HttpCode(HttpStatus.CREATED)
  public async createUser(
    @Body() body: CreateUserDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    let avatar = req.files?.avatar;

    if (!avatar) {
      functions.throwHttpException(
        false,
        'Polje za sliku ne sme biti prazno.',
        HttpStatus.BAD_REQUEST,
      );
    }

    const user: UserEntity = await this._authService.createUser(body, avatar);

    if (!user) {
      functions.throwHttpException(
        false,
        'Doslo je do greske prilikom registracije.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    return {
      success: true,
      message: 'Uspesno ste se registrovali.',
    };
  }

  /**
   * Ova ruta se koristi kako bi se korisnik prijavio na aplikaciji.
   * Korisnik unosi email i lozinku.
   * Nakon validacije podataka, korisnik moze koristiti aplikaciju i njene funkcije.
   */
  @UseGuards(NotAuthenticatedGuard)
  @Post('/signin')
  @HttpCode(HttpStatus.OK)
  public async loginUser(
    @Body() body: LoginUserDto,
    @Res({ passthrough: true }) response: Response,
  ): Promise<IDataSendResponse<Pick<UserEntity, 'id' | 'isExpert' | 'role'>>> {
    const user: UserEntity = await this._authService.loginUser(body);

    if (!user) {
      functions.throwHttpException(
        false,
        'Neuspesno prijavljivanje. Pokusajte ponovo.',
        HttpStatus.INTERNAL_SERVER_ERROR,
      );
    }

    try {
      const payload: IJwtPayload = { id: user.id };

      const accessToken = await this._jwtService.signAsync(payload);
      const refreshToken = await this._jwtService.signAsync(payload, {
        expiresIn: this._configService.get<string>('JWT_REFRESH_TOKEN_EXPIRE'),
        secret: this._configService.get<string>('JWT_REFRESH_TOKEN'),
      });

      response.cookie('access_token', accessToken, {
        httpOnly: true,
        sameSite: 'none',
        secure: true,
      });

      response.cookie('refresh_token', refreshToken, {
        httpOnly: true,
        sameSite: 'none',
        secure: true,
      });

      return {
        success: true,
        data: {
          id: user.id,
          isExpert: user.isExpert,
          role: user.role,
        },
        message: 'Uspesno ste se prijavili.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Dogodila se greska prilikom generisanja tokena.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi aktivirala korisnikov nalog.
   * Proverava se email i kod, ukoliko je sve validno, postavlja 'active' atribut u bazu na true.
   * Ovo znaci da je korisnikov nalog aktiviran i moze da se koriste ostale rute bez ogranicenja.
   */
  @UseGuards(NotAuthenticatedGuard)
  @Post('/verification/confirm')
  @HttpCode(HttpStatus.OK)
  public async confirmVerification(
    @Query() query: ConfirmVerificationDto,
  ): Promise<ISendResponse> {
    await this._authService.confirmVerification(query);

    return {
      success: true,
      message: 'Uspesno ste verifikovali nalog.',
    };
  }

  /**
   * Ova ruta se koristi kako bi omogucila korisniku da zatrazi novi verifikacioni kod.
   * Potrebno je da korisnik unese email adresu.
   * Ukoliko je email adresa validna, korisniku ce stici novi verifikacioni kod.
   */
  @UseGuards(NotAuthenticatedGuard)
  @Post('/verification/resend/:email')
  @HttpCode(HttpStatus.OK)
  public async resendVerification(
    @Param() param: EmailDto,
  ): Promise<ISendResponse> {
    await this._authService.resendVerification(param);

    return {
      success: true,
      message: 'Uspesno ste zatrazili novi verifikacioni kod.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisniku omogucila da resetuje svoju lozinku.
   * Potrebno je da korisnik unese email adresu.
   * Ukoliko je email adresa validna, korisniku ce stici link za resetovanje lozinke.
   */
  @UseGuards(NotAuthenticatedGuard)
  @Post('/password/forgot/:email')
  @HttpCode(HttpStatus.OK)
  public async forgotPassword(
    @Param() param: EmailDto,
  ): Promise<ISendResponse> {
    await this._authService.forgotPassword(param);

    return {
      success: true,
      message: 'Uspesno ste poslali zahtev za resetovanje lozinke.',
    };
  }

  /**
   * Ova ruta se koristi kako bi proverila email i token.
   * Nakon provere, dozvoljava korisniku na frontend da pristupi stranici za promenu lozinke.
   * Ukoliko je validacija uspesna, vraca true, u suprotnom, false.
   */
  @UseGuards(NotAuthenticatedGuard)
  @Post('/password/reset')
  @HttpCode(HttpStatus.OK)
  public async resetPassword(
    @Query() query: ResetPasswordDto,
  ): Promise<ISendResponse> {
    await this._authService.resetPassword(query);

    return {
      success: true,
      message: 'Uspesno.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisniku omogucila da promeni svoju lozinku.
   * Potrebno je da korisnik unese email adresu.
   * Ukoliko je email adresa validna, korisniku ce stici link za resetovanje lozinke.
   */
  @UseGuards(NotAuthenticatedGuard)
  @Post('/password/change')
  @HttpCode(HttpStatus.OK)
  public async changePassword(
    @Body() body: ChangePasswordDto,
  ): Promise<ISendResponse> {
    await this._authService.changePassword(body);

    return {
      success: true,
      message: 'Uspesno ste izmenili Vasu sifru.',
    };
  }

  /**
   * Ova ruta se koristi kako bi korisniku omogucila da se odjavi.
   * Potrebno je da korisnik pozove rutu.
   * Ukoliko je uspesna odjava, vraca true, u suprotnom false.
   */
  @UseGuards(AuthenticatedGuard)
  @Post('/signout')
  @HttpCode(HttpStatus.OK)
  public async signOut(
    @Res({ passthrough: true }) response: Response,
  ): Promise<ISendResponse> {
    response.clearCookie('access_token', {
      httpOnly: true,
      sameSite: 'none',
      secure: true,
    });
    response.clearCookie('refresh_token', {
      httpOnly: true,
      sameSite: 'none',
      secure: true,
    });

    return {
      success: true,
      message: 'Uspesno.',
    };
  }
}
