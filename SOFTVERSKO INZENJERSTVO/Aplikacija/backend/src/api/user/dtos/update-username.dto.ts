import { Matches, MaxLength, MinLength } from 'class-validator';

export class UpdateUsernameDto {
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
}
