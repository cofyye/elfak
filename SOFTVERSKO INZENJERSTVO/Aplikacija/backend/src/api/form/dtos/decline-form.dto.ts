import { IsNotEmpty } from 'class-validator';

export class DeclineFormDto {
  @IsNotEmpty({
    message: 'ID oglasa ne sme biti prazan.',
  })
  public adId: string;

  @IsNotEmpty({ message: 'Client ID ne sme biti prazan.' })
  public clientId: string;
}
