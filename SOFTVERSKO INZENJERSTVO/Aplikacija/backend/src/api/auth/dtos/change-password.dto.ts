import { IsEmail, IsNotEmpty, MaxLength, MinLength } from 'class-validator';

export class ChangePasswordDto {
  @MaxLength(32, { message: 'Sifra mora sadrzati najvise 32 karaktera.' })
  @MinLength(8, { message: 'Sifra mora sadrzati najmanje 8 karaktera.' })
  public readonly password: string;

  @MaxLength(32, { message: 'Sifra mora sadrzati najvise 32 karaktera.' })
  @MinLength(8, { message: 'Sifra mora sadrzati najmanje 8 karaktera.' })
  public readonly confirmPassword: string;

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
