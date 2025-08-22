import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AuthService } from '../../services/auth.service';
import { AppState } from '../../../../app.state';
import { Store } from '@ngrx/store';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Subscription } from 'rxjs';
import { getLoading1 } from '../../../../shared/store/loading.selectors';
import { IResetPassword } from '../../interfaces/auth.interface';
import {
  hideLoading1,
  showLoading1,
} from '../../../../shared/store/loading.actions';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css',
  encapsulation: ViewEncapsulation.None, // Opcija za menjanje PrimeNG klase
})
export class ResetPasswordComponent implements OnInit, OnDestroy {
  private isLoadingSub: Subscription = new Subscription();

  public isLoading: boolean = false;

  constructor(
    private readonly _messageService: MessageService,
    private readonly _authService: AuthService,
    private readonly _store: Store<AppState>,
    private readonly _router: Router,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _fb: FormBuilder
  ) {}

  public resetPasswordForm: FormGroup = this._fb.group({
    password: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(32),
    ]),
    confirmPassword: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(32),
    ]),
  });

  public onResetPassword() {
    if (this.resetPasswordForm.invalid) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Polja moraju biti validna.',
      });

      return;
    }

    const passwords: IResetPassword = {
      password: this.resetPasswordForm.get('password')?.value,
      confirmPassword: this.resetPasswordForm.get('confirmPassword')?.value,
      email: this._activatedRoute.snapshot.queryParamMap.get('email'),
      token: this._activatedRoute.snapshot.queryParamMap.get('token'),
    };

    if (passwords.password != passwords.confirmPassword) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Lozinke se ne podudaraju.',
      });

      return;
    }

    this._store.dispatch(showLoading1());

    this._authService.changePassword(passwords).subscribe({
      next: (response) => {
        if (response.success) {
          this._messageService.add({
            severity: 'success',
            summary: 'Uspesno',
            detail: response.message,
          });

          this._router.navigate(['/auth/login']);
        } else {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: response.message,
          });
        }

        this._store.dispatch(hideLoading1());
      },
      error: (err: HttpErrorResponse) => {
        if (err.status != 400) {
          this._router.navigate(['/auth/login']);
        }

        this._store.dispatch(hideLoading1());
      },
    });
  }

  public ngOnInit(): void {
    this.isLoadingSub = this._store
      .select(getLoading1)
      .subscribe((isLoading) => {
        this.isLoading = isLoading;
      });
  }

  public ngOnDestroy(): void {
    if (this.isLoadingSub) {
      this.isLoadingSub.unsubscribe();
    }
  }
}
