import { IsOptional, MaxLength } from 'class-validator';

export class UpdateProfessionDto {
  @MaxLength(50, {
    message: 'E-mail adresa mora imati najvise 50 karaktera.',
  })
  @IsOptional()
  public readonly profession: string;

  @IsOptional()
  public readonly bio: string;
}
