import { MaxLength, MinLength } from 'class-validator';

export class UpdatePasswordDto {
  @MaxLength(32, { message: 'Sifra mora sadrzati najvise 32 karaktera.' })
  @MinLength(8, { message: 'Sifra mora sadrzati najmanje 8 karaktera.' })
  public readonly currentPassword: string;

  @MaxLength(32, { message: 'Sifra mora sadrzati najvise 32 karaktera.' })
  @MinLength(8, { message: 'Sifra mora sadrzati najmanje 8 karaktera.' })
  public readonly password: string;

  @MaxLength(32, { message: 'Sifra mora sadrzati najvise 32 karaktera.' })
  @MinLength(8, { message: 'Sifra mora sadrzati najmanje 8 karaktera.' })
  public readonly confirmPassword: string;
}
