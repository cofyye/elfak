import { Type } from 'class-transformer';
import {
  IsDate,
  IsInt,
  IsNotEmpty,
  Max,
  MaxLength,
  Min,
} from 'class-validator';

export class AcceptFormDto {
  @IsNotEmpty({
    message: 'ID oglasa ne sme biti prazan.',
  })
  public adId: string;

  @IsNotEmpty({ message: 'Client ID ne sme biti prazan.' })
  public clientId: string;

  @Type(() => Number)
  @Max(100000, { message: 'Cena mora biti maksimalno 100000 tokena.' })
  @Min(1, { message: 'Cena mora biti minimalno 1 token.' })
  @IsInt({ message: 'Cena mora biti numerickog formata.' })
  @IsNotEmpty({ message: 'Cena ne sme biti prazna.' })
  public price: number;

  @IsDate({ message: 'Molimo Vas da unesete validan datum.' })
  @IsNotEmpty({ message: 'Datum ne sme biti prazan.' })
  public dateTime: Date;

  @IsNotEmpty({ message: 'Polje za planove ne sme biti prazno.' })
  public plans: string;

  @MaxLength(1500, {
    message: 'Deskripcija mora biti maksimalno 1500 karaktera.',
  })
  @IsNotEmpty({ message: 'Deskripcija ne sme biti prazna.' })
  public description: string;
}
