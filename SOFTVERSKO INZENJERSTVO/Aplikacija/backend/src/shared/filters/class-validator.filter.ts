import {
  ArgumentsHost,
  BadRequestException,
  Catch,
  ExceptionFilter,
} from '@nestjs/common';
import { Response } from 'express';
import {
  IClassValidatorResponse,
  ISendResponse,
} from '../interfaces/response.interface';

@Catch(BadRequestException)
export class ClassValidatorFilter implements ExceptionFilter {
  catch(exception: BadRequestException, host: ArgumentsHost) {
    const response: Response = host.switchToHttp().getResponse<Response>();
    const status: number = exception.getStatus();

    const res = exception.getResponse() as IClassValidatorResponse;

    return response.status(status).json({
      success: false,
      message: res.message[0],
    } as ISendResponse);
  }
}
