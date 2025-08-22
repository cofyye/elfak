import { HttpException, HttpStatus } from '@nestjs/common';
import {
  IDataSendResponse,
  ISendResponse,
} from '../interfaces/response.interface';

const handleHttpException = (
  err: unknown,
  success: boolean,
  message: string,
): void => {
  if (err instanceof HttpException) {
    throw err;
  }

  throwHttpException(success, message, HttpStatus.INTERNAL_SERVER_ERROR);
};

const throwHttpException = (
  success: boolean,
  message: string,
  httpStatus: HttpStatus,
) => {
  throw new HttpException(
    {
      success,
      message,
    } as ISendResponse,
    httpStatus,
  );
};

const throwHttpExceptionData = <T>(
  success: boolean,
  data: T,
  message: string,
  httpStatus: HttpStatus,
) => {
  throw new HttpException(
    {
      success,
      data,
      message,
    } as IDataSendResponse<T>,
    httpStatus,
  );
};

function generateCode(length: number = 10): string {
  const characters: string =
    '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
  const charactersLength: number = characters.length;
  let randomString: string = '';

  for (let i: number = 0; i < length; i++) {
    randomString += characters.charAt(
      Math.floor(Math.random() * charactersLength),
    );
  }

  return randomString;
}

function generateRandomString(length: number = 15): string {
  const array = new Uint16Array(length / 2);
  crypto.getRandomValues(array);

  return Array.from(array, (dec) => dec.toString(16).padStart(2, '0')).join('');
}

function formatBytes(bytes: number, decimals = 2): string {
  if (bytes === 0) return '0 Bytes';

  const k = 1024;
  const dm = decimals < 0 ? 0 : decimals;
  const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

  const i = Math.floor(Math.log(bytes) / Math.log(k));

  return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
}

export const functions = {
  handleHttpException,
  throwHttpException,
  throwHttpExceptionData,
  generateCode,
  generateRandomString,
  formatBytes,
};
