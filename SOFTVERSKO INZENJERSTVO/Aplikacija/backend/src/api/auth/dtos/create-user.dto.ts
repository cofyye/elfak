import {
  IsEmail,
  IsNotEmpty,
  IsString,
  Matches,
  MaxLength,
  MinLength,
} from 'class-validator';

export class CreateUserDto {
  @IsString({ message: 'Ime mora sadrzati samo karaktere.' })
  @MaxLength(12, { message: 'Ime mora sadrzati manje najvise 50 karaktera.' })
  @MinLength(2, { message: 'Ime mora sadrzati najmanje 2 karaktera.' })
  public readonly firstName: string;

  @IsString({ message: 'Prezime mora sadrzati samo karaktere.' })
  @MaxLength(12, { message: 'Prezime mora sadrzati najvise 50 karaktera.' })
  @MinLength(2, { message: 'Prezime mora sadrzati najmanje 2 karaktera.' })
  public readonly lastName: string;

  @Matches(new RegExp('^[a-z0-9._]+([._]?[a-z0-9]+)*$', 'gm'), {
    message:
      'Korisnicko ime nije validno. Dozvoljeni karakteri su: a-z(samo mala slova), 0-9, _ i .(tacka).',
  })
  @MaxLength(12, {
    message: 'Korisnicko ime mora sadrzati najvise 12 karaktera.',
  })
  @MinLength(3, {
    message: 'Korisnicko ime mora sadrzati najmanje 3 karaktera.',
  })
  public readonly username: string;

  @IsEmail({}, { message: 'Unesite ispravnu e-mail adresu.' })
  @MaxLength(100, {
    message: 'E-mail adresa mora imati najvise 100 karaktera.',
  })
  @IsNotEmpty({ message: 'Polje za e-mail adresu ne sme biti prazno.' })
  public readonly email: string;

  @MaxLength(32, { message: 'Sifra mora sadrzati najvise 32 karaktera.' })
  @MinLength(8, { message: 'Sifra mora sadrzati najmanje 8 karaktera.' })
  public readonly password: string;

  @IsNotEmpty({ message: 'Molimo Vas da unesete tip naloga.' })
  public isExpert: string;

  public avatar: string;
}
