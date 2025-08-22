export interface ISignUp {
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  password: string;
  isExpert: boolean;
  avatar: File;
}

export interface ISignIn {
  email: string;
  password: string;
}

export interface ISignInData {
  id: string;
  isExpert: boolean;
  role: string;
}

export interface IEmail {
  email: string;
}

export interface ITokenValidation {
  email: string | null;
  token: string | null;
}

export interface IResetPassword extends ITokenValidation {
  password: string;
  confirmPassword: string;
}
