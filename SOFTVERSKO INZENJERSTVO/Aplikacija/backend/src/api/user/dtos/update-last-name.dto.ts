import { IsString, MaxLength, MinLength } from 'class-validator';

export class UpdateLastNameDto {
  @IsString({ message: 'Prezime mora sadrzati samo karaktere.' })
  @MaxLength(12, { message: 'Prezime mora sadrzati najvise 12 karaktera.' })
  @MinLength(2, { message: 'Prezime mora sadrzati najmanje 2 karaktera.' })
  public readonly lastName: string;
}
