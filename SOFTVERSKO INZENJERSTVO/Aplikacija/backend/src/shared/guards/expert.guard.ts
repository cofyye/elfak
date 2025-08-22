import {
  CanActivate,
  ExecutionContext,
  HttpStatus,
  Injectable,
} from '@nestjs/common';
import { Request } from 'express';
import { UserEntity } from 'src/shared/entities/user.entity';
import { functions } from '../utils/functions';

@Injectable()
export class ExpertGuard implements CanActivate {
  constructor() {}

  public canActivate(context: ExecutionContext): boolean {
    try {
      const request: Request = context.switchToHttp().getRequest<Request>();
      const user: UserEntity = request.user as UserEntity;

      if (!user) {
        functions.throwHttpException(
          false,
          'Korisnik nije pronadjen.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (!user.isExpert) {
        functions.throwHttpException(
          false,
          'Morate biti strucnjak kako biste pristupili ovoj stranici.',
          HttpStatus.FORBIDDEN,
        );
      }

      return true;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Morate biti strucnjak kako biste pristupili ovoj stranici.',
      );
    }
  }
}
