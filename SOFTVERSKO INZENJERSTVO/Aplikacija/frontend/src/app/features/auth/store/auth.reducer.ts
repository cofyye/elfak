import { createReducer, on } from '@ngrx/store';
import { initialState } from './auth.state';
import { saveUser } from './auth.actions';

export const authReducer = createReducer(
  initialState,
  on(saveUser, (state, action) => {
    const newUser = { ...state.user, ...action.user };

    return {
      ...state,
      user: newUser,
    };
  })
);
