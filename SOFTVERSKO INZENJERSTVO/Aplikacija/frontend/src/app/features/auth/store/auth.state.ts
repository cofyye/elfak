import { ILoginStatus } from '../../../shared/interfaces/user.interface';

export interface AuthState {
  user: ILoginStatus;
}

export const initialState: AuthState = {
  user: {
    id: '',
    isExpert: false,
    role: 'user',
    fetched: false,
    loggedIn: false,
  },
};
