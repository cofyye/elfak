import { createFeatureSelector, createSelector } from '@ngrx/store';
import { LoadingState } from './loading.state';

export const getLoading1State = createFeatureSelector<LoadingState>('loading');
export const getLoading2State = createFeatureSelector<LoadingState>('loading');
export const getLoadingPageState =
  createFeatureSelector<LoadingState>('loading');

export const getLoading1 = createSelector(getLoading1State, (state) => {
  return state.loading1;
});
export const getLoading2 = createSelector(getLoading2State, (state) => {
  return state.loading2;
});
export const getLoadingPage = createSelector(getLoadingPageState, (state) => {
  return state.loadingPage;
});
