import { IsEmail, IsNotEmpty, MaxLength, MinLength } from 'class-validator';

export class ResetPasswordDto {
  @IsEmail({}, { message: 'Unesite ispravnu e-mail adresu.' })
  @MaxLength(100, {
    message: 'E-mail adresa mora imati najvise 100 karaktera.',
  })
  @IsNotEmpty({ message: 'Polje za e-mail adresu ne sme biti prazno.' })
  public readonly email: string;

  @MaxLength(12, {
    message: 'Token mora imati 12 karaktera.',
  })
  @MinLength(12, {
    message: 'Token mora imati 12 karaktera.',
  })
  @IsNotEmpty({ message: 'Polje za token ne sme biti prazno.' })
  public readonly token: string;
}
