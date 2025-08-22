import { authReducer } from './features/auth/store/auth.reducer';
import { AuthState } from './features/auth/store/auth.state';
import { loadingReducer } from './shared/store/loading.reducer';
import { LoadingState } from './shared/store/loading.state';

export interface AppState {
  user: AuthState;
  loading: LoadingState;
}

export const appReducer = {
  user: authReducer,
  loading: loadingReducer,
};
