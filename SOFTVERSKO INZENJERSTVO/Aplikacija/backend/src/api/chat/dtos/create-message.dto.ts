import { IsNotEmpty, MaxLength } from 'class-validator';

export class CreateMessageDto {
  @IsNotEmpty({
    message: 'Molimo Vas da unesete korisnika kome saljete poruku.',
  })
  public receiverId: string;

  @MaxLength(1500, {
    message: 'Poruka mora biti maksimalno 1500 karaktera.',
  })
  @IsNotEmpty({ message: 'Poruka ne sme biti prazna.' })
  public message: string;
}
