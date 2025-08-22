import { IsNotEmpty } from 'class-validator';

export class CapturePaymentDto {
  @IsNotEmpty({ message: 'Nedostaje token u parametru.' })
  public readonly token: string;

  @IsNotEmpty({ message: 'Nedostaje PayerID u parametru.' })
  public readonly PayerID: string;
}
