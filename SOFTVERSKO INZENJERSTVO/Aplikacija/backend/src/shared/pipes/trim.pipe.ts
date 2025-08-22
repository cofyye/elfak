import { Injectable, PipeTransform } from '@nestjs/common';

@Injectable()
export class TrimPipe implements PipeTransform {
  transform(obj: unknown) {
    if (typeof obj === 'object') {
      for (const [key, value] of Object.entries(obj)) {
        if (typeof value === 'string') {
          obj[key] = value.trim();
        }
      }

      return obj;
    } else {
      return obj;
    }
  }
}
