import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { AuthRoutingModule } from './auth-routing.module';
import { RouterModule } from '@angular/router';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { CardModule } from 'primeng/card';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { StepperModule } from 'primeng/stepper';
import { PasswordModule } from 'primeng/password';
import { TooltipModule } from 'primeng/tooltip';
import { ResetPasswordComponent } from './pages/reset-password/reset-password.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './services/auth.service';
import { DialogModule } from 'primeng/dialog';
import { ConfirmEmailComponent } from './pages/confirm-email/confirm-email.component';

@NgModule({
  declarations: [
    RegisterComponent,
    LoginComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    ConfirmEmailComponent,
  ],
  imports: [
    AuthRoutingModule,
    CommonModule,
    HttpClientModule,
    RouterModule,
    CardModule,
    ButtonModule,
    InputTextModule,
    InputIconModule,
    IconFieldModule,
    RippleModule,
    StepperModule,
    PasswordModule,
    TooltipModule,
    DialogModule,
    ReactiveFormsModule,
  ],
  providers: [AuthService],
  exports: [],
})
export class AuthModule {}
