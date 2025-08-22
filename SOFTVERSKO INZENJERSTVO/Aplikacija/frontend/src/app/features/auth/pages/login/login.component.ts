import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MessageService } from 'primeng/api';
import { AppState } from '../../../../app.state';
import { Store } from '@ngrx/store';
import { IEmail, ISignIn } from '../../interfaces/auth.interface';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IDataAcceptResponse } from '../../../../shared/interfaces/response.interface';
import { HttpErrorResponse } from '@angular/common/http';
import {
  hideLoading1,
  hideLoading2,
  showLoading1,
  showLoading2,
} from '../../../../shared/store/loading.actions';
import {
  getLoading1,
  getLoading2,
} from '../../../../shared/store/loading.selectors';
import { saveUser } from '../../store/auth.actions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class LoginComponent implements OnInit, OnDestroy {
  private isLoading1Sub: Subscription = new Subscription();
  private isLoading2Sub: Subscription = new Subscription();

  public isVisible: boolean = false;
  public isLoading1: boolean = false;
  public isLoading2: boolean = false;

  constructor(
    private readonly _messageService: MessageService,
    private readonly _authService: AuthService,
    private readonly _fb: FormBuilder,
    private readonly _router: Router,
    private readonly _store: Store<AppState>
  ) {}

  public loginForm: FormGroup = this._fb.group({
    email: new FormControl<string>('', [
      Validators.required,
      Validators.maxLength(100),
      Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$'),
    ]),
    password: new FormControl<string>('', [Validators.required]),
  });

  public requestCodeForm: FormGroup = this._fb.group({
    req_code_email: new FormControl<string>('', [
      Validators.required,
      Validators.maxLength(100),
      Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$'),
    ]),
  });

  public onLogin() {
    const data: ISignIn = {
      email: this.loginForm.get('email')?.value,
      password: this.loginForm.get('password')?.value,
    };

    this._store.dispatch(showLoading1());

    this._authService.login(data).subscribe({
      next: (response) => {
        if (response.success) {
          this._messageService.add({
            severity: 'success',
            summary: 'Uspesno',
            detail: response.message,
          });

          this._store.dispatch(
            saveUser({
              user: {
                id: response.data.id,
                isExpert: response.data.isExpert,
                role: response.data.role,
                loggedIn: true,
                fetched: true,
              },
            })
          );

          this._router.navigateByUrl('/');
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
        const response = err.error as IDataAcceptResponse<{
          verified: boolean;
        }>;

        if (response.data?.verified === false) {
          this.isVisible = true;
          this.requestCodeForm.controls['req_code_email'].setValue(
            this.loginForm.get('email')?.value
          );
        }

        this._store.dispatch(hideLoading1());
      },
    });
  }

  public onRequestCode() {
    const data: IEmail = {
      email: this.requestCodeForm.get('req_code_email')?.value,
    };

    this._store.dispatch(showLoading2());

    this._authService.requestCode(data).subscribe({
      next: (response) => {
        if (response.success) {
          this._messageService.add({
            severity: 'success',
            summary: 'Uspesno',
            detail: response.message,
          });

          this.isVisible = false;
        } else {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: response.message,
          });
        }

        this._store.dispatch(hideLoading2());
      },
      error: () => {
        this._store.dispatch(hideLoading2());
      },
    });
  }

  public ngOnInit(): void {
    this.isLoading1Sub = this._store
      .select(getLoading1)
      .subscribe((isLoading) => {
        this.isLoading1 = isLoading;
      });
    this.isLoading2Sub = this._store
      .select(getLoading2)
      .subscribe((isLoading) => {
        this.isLoading2 = isLoading;
      });
  }

  public ngOnDestroy(): void {
    if (this.isLoading1Sub) {
      this.isLoading1Sub.unsubscribe();
    }
    if (this.isLoading2Sub) {
      this.isLoading2Sub.unsubscribe();
    }
  }
}
