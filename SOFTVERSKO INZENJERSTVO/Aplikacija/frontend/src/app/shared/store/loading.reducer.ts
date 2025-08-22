import { createReducer, on } from '@ngrx/store';
import { initialState } from './loading.state';
import {
  hideLoading1,
  hideLoading2,
  hideLoadingPage,
  showLoading1,
  showLoading2,
  showLoadingPage,
} from './loading.actions';

export const loadingReducer = createReducer(
  initialState,
  on(showLoading1, (state) => {
    return {
      ...state,
      loading1: true,
    };
  }),
  on(hideLoading1, (state) => {
    return {
      ...state,
      loading1: false,
    };
  }),
  on(showLoading2, (state) => {
    return {
      ...state,
      loading2: true,
    };
  }),
  on(hideLoading2, (state) => {
    return {
      ...state,
      loading2: false,
    };
  }),
  on(showLoadingPage, (state) => {
    return {
      ...state,
      loadingPage: true,
    };
  }),
  on(hideLoadingPage, (state) => {
    return {
      ...state,
      loadingPage: false,
    };
  })
);
