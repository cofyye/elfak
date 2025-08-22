import { IsString, MaxLength, MinLength } from 'class-validator';

export class UpdateFirstNameDto {
  @IsString({ message: 'Ime mora sadrzati samo karaktere.' })
  @MaxLength(12, { message: 'Ime mora sadrzati manje najvise 12 karaktera.' })
  @MinLength(2, { message: 'Ime mora sadrzati najmanje 2 karaktera.' })
  public readonly firstName: string;
}
