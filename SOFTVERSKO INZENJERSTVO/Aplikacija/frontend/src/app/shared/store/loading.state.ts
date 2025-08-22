export interface LoadingState {
  loading1: boolean;
  loading2: boolean;
  loadingPage: boolean;
}

export const initialState: LoadingState = {
  loading1: false,
  loading2: false,
  loadingPage: false,
};
