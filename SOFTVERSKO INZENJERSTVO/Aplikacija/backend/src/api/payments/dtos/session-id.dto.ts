import { IsNotEmpty } from 'class-validator';

export class SessionIdDto {
  @IsNotEmpty({ message: 'Nedostaje session_id u parametru.' })
  public readonly session_id: string;
}
