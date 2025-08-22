import { IsNotEmpty, MaxLength } from 'class-validator';

export class CreateTagDto {
  @MaxLength(20, {
    message: 'Ime mora imati najvise 20 karaktera.',
  })
  @IsNotEmpty({ message: 'Polje za ime ne sme biti prazno.' })
  public readonly name: string;
}
