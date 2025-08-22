import {
  IsEnum,
  IsInt,
  IsOptional,
  Max,
  MaxLength,
  Min,
} from 'class-validator';
import { SortBy, SortOrder } from '../enums/sort.enum';
import { Type } from 'class-transformer';

export class PaginationQueryDto {
  @IsEnum(SortOrder, { message: 'Sortiranje mora biti ASC ili DESC.' })
  @IsOptional()
  public readonly order?: SortOrder = SortOrder.ASC;

  @IsEnum(SortBy, {
    message:
      'Sortiranje mora biti title, description, grade, createdAt, averageGrade, id.',
  })
  @IsOptional()
  public readonly sortBy?: SortBy = SortBy.CREATED_AT;

  @Type(() => Number)
  @IsInt({ message: 'Stranica mora biti numerickog formata.' })
  @Min(1, { message: 'Minimalni broj za stranicu mora biti 1.' })
  @IsOptional()
  public readonly page?: number = 1;

  @Type(() => Number)
  @IsInt({ message: 'Uzimanje mora biti numerickog formata.' })
  @Min(1, { message: 'Minimalni broj koji se mora uzeti je 1.' })
  @Max(50, { message: 'Maksimalni broj koji se moze uzeti je 50.' })
  @IsOptional()
  public readonly take?: number = 10;

  @MaxLength(50, { message: 'Najvise mozete da pretrazite 50 karaktera.' })
  @IsOptional()
  public readonly search?: string = '';

  get skip(): number {
    return (this.page - 1) * this.take;
  }
}
