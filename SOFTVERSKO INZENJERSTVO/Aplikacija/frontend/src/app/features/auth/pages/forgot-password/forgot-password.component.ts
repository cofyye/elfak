import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Store } from '@ngrx/store';
import { MessageService } from 'primeng/api';
import { AppState } from '../../../../app.state';
import {
  hideLoading1,
  showLoading1,
} from '../../../../shared/store/loading.actions';
import { AuthService } from '../../services/auth.service';
import { IEmail } from '../../interfaces/auth.interface';
import { Subscription } from 'rxjs';
import { getLoading1 } from '../../../../shared/store/loading.selectors';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css',
})
export class ForgotPasswordComponent implements OnInit, OnDestroy {
  private isLoadingSub: Subscription = new Subscription();

  public isLoading: boolean = false;

  constructor(
    private readonly _messageService: MessageService,
    private readonly _authService: AuthService,
    private readonly _store: Store<AppState>,
    private readonly _fb: FormBuilder
  ) {}

  public forgotForm: FormGroup = this._fb.group({
    email: new FormControl<string>('', [
      Validators.required,
      Validators.maxLength(100),
      Validators.pattern('^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$'),
    ]),
  });

  public onForgotPassword() {
    if (this.forgotForm.invalid) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Polje za email adresu nije validno.',
      });

      return;
    }

    const email: IEmail = {
      email: this.forgotForm.get('email')?.value,
    };

    this._store.dispatch(showLoading1());

    this._authService.forgotPassword(email).subscribe({
      next: (response) => {
        if (response.success) {
          this._messageService.add({
            severity: 'success',
            summary: 'Uspesno',
            detail: response.message,
          });
        } else {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: response.message,
          });
        }

        this.forgotForm.get('email')?.setValue('');
        this.forgotForm.get('email')?.reset();
        this._store.dispatch(hideLoading1());
      },
      error: () => {
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
