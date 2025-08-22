import { ExtractJwt, Strategy } from 'passport-jwt';
import { PassportStrategy } from '@nestjs/passport';
import { HttpStatus, Injectable } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import { Request } from 'express';
import { IJwtPayload } from 'src/shared/interfaces/jwt.interface';
import { Repository } from 'typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';
import { InjectRepository } from '@nestjs/typeorm';
import { functions } from 'src/shared/utils/functions';

@Injectable()
export class JwtStrategy extends PassportStrategy(Strategy, 'jwt') {
  constructor(
    @InjectRepository(UserEntity)
    private readonly _userRepo: Repository<UserEntity>,
    configService: ConfigService,
  ) {
    super({
      jwtFromRequest: ExtractJwt.fromExtractors([
        (request: Request) => {
          return request?.cookies['access_token'];
        },
      ]),
      ignoreExpiration: false,
      secretOrKey: configService.get<string>('JWT_ACCESS_TOKEN'),
    });
  }

  async validate(payload: IJwtPayload) {
    try {
      const user = await this._userRepo.findOne({
        where: { id: payload.id },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Zao nam je, nemate dozvolu da pristupite ovoj stranici.',
          HttpStatus.UNAUTHORIZED,
        );
      }

      return user;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Zao nam je, nemate dozvolu da pristupite ovoj stranici.',
      );
    }
  }
}
