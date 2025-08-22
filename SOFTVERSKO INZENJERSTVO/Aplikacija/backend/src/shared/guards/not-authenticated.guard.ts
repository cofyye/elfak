import { ExecutionContext, HttpStatus, Injectable } from '@nestjs/common';
import { AuthGuard } from '@nestjs/passport';
import { functions } from '../utils/functions';
import { Request } from 'express';

@Injectable()
export class NotAuthenticatedGuard extends AuthGuard('jwt') {
  constructor() {
    super();
  }

  canActivate(context: ExecutionContext) {
    const req: Request = context.switchToHttp().getRequest();

    if (req.cookies['access_token'] || req.cookies['refresh_token']) {
      functions.throwHttpException(
        false,
        'Morate biti izlogovani da biste pristupili ovoj stranici.',
        HttpStatus.FORBIDDEN,
      );
    }

    return true;
  }
}
