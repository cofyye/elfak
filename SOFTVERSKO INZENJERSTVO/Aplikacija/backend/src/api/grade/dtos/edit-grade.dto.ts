import { IsEnum, IsNotEmpty } from 'class-validator';
import { Grade } from 'src/shared/enums/grade.enum';

export class EditGradeDto {
  @IsEnum(Grade, { message: 'Ocena moze biti 1, 2, 3, 4 ili 5.' })
  @IsNotEmpty({ message: 'Polje za ocenu ne sme biti prazno.' })
  public readonly grade: Grade;

  @IsNotEmpty({ message: 'Polje za komentar ne sme biti prazno.' })
  public readonly comment: string;
}
