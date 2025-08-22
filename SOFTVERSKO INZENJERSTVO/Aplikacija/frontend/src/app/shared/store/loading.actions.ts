import { createAction } from '@ngrx/store';

export const showLoading1 = createAction('[Loading] Show 1');
export const hideLoading1 = createAction('[Loading] Hide 1');
export const showLoading2 = createAction('[Loading] Show 2');
export const hideLoading2 = createAction('[Loading] Hide 2');
export const showLoadingPage = createAction('[Loading] Show Page');
export const hideLoadingPage = createAction('[Loading] Hide Page');
