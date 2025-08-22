import {
  CanActivate,
  ExecutionContext,
  HttpStatus,
  Injectable,
} from '@nestjs/common';
import { UserRole } from '../enums/role.enum';
import { Request } from 'express';
import { UserEntity } from 'src/shared/entities/user.entity';
import { functions } from '../utils/functions';

@Injectable()
export class RoleGuard implements CanActivate {
  constructor(private readonly roles: UserRole[]) {}

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

      if (!this.roles.includes(user.role)) {
        functions.throwHttpException(
          false,
          'Nemate privilegiju da pristupite ovoj stranici.',
          HttpStatus.FORBIDDEN,
        );
      }

      return true;
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Nemate privilegiju da pristupite ovoj stranici.',
      );
    }
  }
}
