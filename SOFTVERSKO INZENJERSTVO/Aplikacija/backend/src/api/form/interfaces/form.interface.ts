import { FormEntity } from '../entities/form.entity';
import { PlanStatus } from '../enums/form.enum';

export interface ISendFormAccept {
  accepted_client: boolean;
  accepted_client_chat: boolean;
  accepted_expert: boolean;
  user_type: 'client' | 'expert';
  msg: string;
  status: PlanStatus;
}

export interface FormEntityExtended extends FormEntity {
  userType: 'client' | 'expert';
}
