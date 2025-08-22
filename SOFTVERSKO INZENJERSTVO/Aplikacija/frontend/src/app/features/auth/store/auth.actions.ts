import { createAction, props } from '@ngrx/store';
import { ILoginStatus } from '../../../shared/interfaces/user.interface';

export const saveUser = createAction(
  '[Auth Page] Save User Data',
  props<{ user: ILoginStatus }>()
);
