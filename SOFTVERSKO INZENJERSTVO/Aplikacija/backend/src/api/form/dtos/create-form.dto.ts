import { IsDate, IsEnum, IsNotEmpty, MaxLength } from 'class-validator';
import { ChoosedPlan } from '../enums/form.enum';

export class CreateFormDto {
  @IsNotEmpty({
    message: 'Molimo Vas da unesete oglas za koji zelite konsultaciju.',
  })
  public adId: string;

  @MaxLength(1500, {
    message: 'Deskripcija mora biti maksimalno 1500 karaktera.',
  })
  @IsNotEmpty({ message: 'Deskripcija ne sme biti prazna.' })
  public description: string;

  @IsNotEmpty({ message: 'Polje za planove ne sme biti prazno.' })
  public plans: string;

  @IsEnum(ChoosedPlan, {
    message: 'Izabrani plan moze biti basic, standard, premium ili custom.',
  })
  @IsNotEmpty({ message: 'Molimo Vas da izaberete plan.' })
  public readonly choosedPlan: ChoosedPlan;

  @IsDate({ message: 'Molimo Vas da unesete validan datum.' })
  @IsNotEmpty({ message: 'Datum ne sme biti prazan.' })
  public readonly dateTime: Date;
}
