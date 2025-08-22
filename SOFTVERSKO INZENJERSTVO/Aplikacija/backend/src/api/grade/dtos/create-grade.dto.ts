import { IsEnum, IsNotEmpty, IsUUID } from 'class-validator';
import { Grade } from 'src/shared/enums/grade.enum';

export class CreateGradeDto {
  @IsUUID('4', { message: 'Niste uneli validan ID za oglas.' })
  @IsNotEmpty({ message: 'Molimo Vas da unesete ID oglasa.' })
  public readonly adId: string;

  @IsEnum(Grade, { message: 'Ocena moze biti 1, 2, 3, 4 ili 5.' })
  @IsNotEmpty({ message: 'Polje za ocenu ne sme biti prazno.' })
  public readonly grade: Grade;

  @IsNotEmpty({ message: 'Polje za komentar ne sme biti prazno.' })
  public readonly comment: string;
}
