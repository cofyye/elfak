import { ExecutionContext, HttpStatus, Injectable } from '@nestjs/common';
import { AuthGuard } from '@nestjs/passport';
import { functions } from '../utils/functions';
import { Request, Response } from 'express';
import { JwtService, TokenExpiredError } from '@nestjs/jwt';
import { ConfigService } from '@nestjs/config';
import { IJwtPayload } from '../interfaces/jwt.interface';
import { InjectRepository } from '@nestjs/typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';
import { Repository } from 'typeorm';

@Injectable()
export class AuthenticatedGuard extends AuthGuard('jwt') {
  constructor(
    @InjectRepository(UserEntity)
    private readonly _userRepo: Repository<UserEntity>,
    private readonly _jwtService: JwtService,
    private readonly _configService: ConfigService,
  ) {
    super();
  }

  canActivate(context: ExecutionContext) {
    return super.canActivate(context);
  }

  async handleRequest<UserEntity>(
    _: unknown, // Error
    user: UserEntity, // User Entity
    info: unknown, // Info
    context: ExecutionContext, // Context for access request
  ): Promise<UserEntity> {
    const res: Response = context.switchToHttp().getResponse();
    try {
      if (info instanceof TokenExpiredError) {
        const req: Request = context.switchToHttp().getRequest();
        if (req.cookies['refresh_token']) {
          const payload: IJwtPayload = await this._jwtService.verifyAsync(
            req.cookies['refresh_token'],
            {
              secret: this._configService.get<string>('JWT_REFRESH_TOKEN'),
            },
          );

          let userr = await this._userRepo.findOne({
            where: { id: payload.id },
          });

          if (!userr) {
            functions.throwHttpException(
              false,
              'Morate biti ulogovani da biste pristupili ovoj stranici.',
              HttpStatus.UNAUTHORIZED,
            );
          }

          const accessToken = await this._jwtService.signAsync({
            id: payload.id,
          });
          res.cookie('access_token', accessToken, {
            httpOnly: true,
            sameSite: 'none',
            secure: true,
          });

          user = userr as UserEntity;
        } else {
          functions.throwHttpException(
            false,
            'Morate biti ulogovani da biste pristupili ovoj stranici.',
            HttpStatus.UNAUTHORIZED,
          );
        }
      }

      if (!user) {
        functions.throwHttpException(
          false,
          'Morate biti ulogovani da biste pristupili ovoj stranici.',
          HttpStatus.UNAUTHORIZED,
        );
      }

      return user;
    } catch (err) {
      console.log(err);
      res.clearCookie('access_token', {
        httpOnly: true,
        sameSite: 'none',
        secure: true,
      });
      res.clearCookie('refresh_token', {
        httpOnly: true,
        sameSite: 'none',
        secure: true,
      });

      functions.handleHttpException(
        err,
        false,
        'Morate biti ulogovani da biste pristupili ovoj stranici.',
      );
    }
  }
}
