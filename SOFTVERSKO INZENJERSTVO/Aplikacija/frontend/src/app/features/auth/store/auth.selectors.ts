import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AuthState } from './auth.state';

export const getUserState = createFeatureSelector<AuthState>('user');

export const getUser = createSelector(getUserState, (state) => {
  return state.user;
});
