import { IsInt, IsNotEmpty, Max, Min } from 'class-validator';
import { Type } from 'class-transformer';

export class CreatePaymentDto {
  @Type(() => Number)
  @Max(100000, { message: 'Maksimalni broj za uplatu tokena je 100000.' })
  @Min(1, { message: 'Minimalni broj za uplatu tokena je 1.' })
  @IsInt({ message: 'Tokeni moraju biti numerickog formata.' })
  @IsNotEmpty({ message: 'Molimo Vas da unesete broj tokena.' })
  public readonly tokens: number;

  @IsNotEmpty({ message: 'Molimo Vas da unesete return url.' })
  public readonly returnUrlPrefix: string;
}
