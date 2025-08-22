import { IsNotEmpty, MaxLength } from 'class-validator';

export class CreateTicketAnswerDto {
  @IsNotEmpty({ message: 'Molimo Vas da unesete ID tiketa.' })
  public readonly ticketId: string;

  @MaxLength(2000, {
    message: 'Poruka moze imati maksimalno 2000 karaktera.',
  })
  @IsNotEmpty({ message: 'Poruka ne sme biti prazna.' })
  public readonly message: string;
}
