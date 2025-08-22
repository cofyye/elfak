import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './pages/reset-password/reset-password.component';
import { ConfirmEmailComponent } from './pages/confirm-email/confirm-email.component';
import { confirmEmailResolver } from './resolvers/confirm-email.resolver';
import { resetPasswordResolver } from './resolvers/reset-password.resolver';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent,
    pathMatch: 'full',
  },
  {
    path: 'register',
    component: RegisterComponent,
    pathMatch: 'full',
  },
  {
    path: 'password/forgot',
    component: ForgotPasswordComponent,
    pathMatch: 'full',
  },
  {
    path: 'password/reset',
    component: ResetPasswordComponent,
    pathMatch: 'full',

    resolve: { isAllowed: resetPasswordResolver },
  },
  {
    path: 'verification/confirm',
    component: ConfirmEmailComponent,
    pathMatch: 'full',

    resolve: { isAllowed: confirmEmailResolver },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}
