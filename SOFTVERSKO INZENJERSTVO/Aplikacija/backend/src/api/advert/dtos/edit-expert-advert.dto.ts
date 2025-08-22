import { IsNotEmpty, MaxLength } from 'class-validator';

export class EditExpertAdvertDto {
  @MaxLength(100, {
    message: 'Naslov mora imati najvise 100 karaktera.',
  })
  @IsNotEmpty({ message: 'Polje za naslov ne sme biti prazno.' })
  public title: string;

  @IsNotEmpty({ message: 'Polje za deskripciju ne sme biti prazno.' })
  public description: string;

  @IsNotEmpty({ message: 'Polje da li je oglas aktivan ne sme biti prazno.' })
  public active: boolean;

  @IsNotEmpty({ message: 'Polje za planove ne sme biti prazno.' })
  public plans: string;

  @IsNotEmpty({ message: 'Polje za tagove ne sme biti prazno.' })
  public tags: string;
}
