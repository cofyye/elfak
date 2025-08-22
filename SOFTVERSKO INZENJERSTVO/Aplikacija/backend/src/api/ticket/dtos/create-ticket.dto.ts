import { IsEnum, IsNotEmpty, IsOptional, MaxLength } from 'class-validator';
import { TicketType } from '../enums/ticket.enum';

export class CreateTicketDto {
  @IsEnum(TicketType, {
    message:
      'Tip tiketa moze biti user_problem, payment_problem, suggestion_improvement, other.',
  })
  @IsNotEmpty({ message: 'Tip tiketa ne sme biti prazan.' })
  public readonly type: TicketType;

  @MaxLength(2000, {
    message: 'Opis problema moze imati maksimalno 2000 karaktera.',
  })
  @IsNotEmpty({ message: 'Opis problema ne sme biti prazno.' })
  public readonly message: string;

  @MaxLength(100, {
    message: 'Naslov moze imati maksimalno 100 karaktera.',
  })
  @IsNotEmpty({ message: 'Naslov ne sme biti prazan.' })
  public readonly title: string;

  @MaxLength(12, {
    message: 'Korisnicko ime moze imati maksimalno 12 karaktera.',
  })
  @IsOptional()
  public readonly username: string;
}
