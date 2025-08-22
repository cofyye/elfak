import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Subject, Subscription, debounceTime, fromEvent, tap } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { usernameAvailabilityValidator } from '../../../../shared/validators/username-availability.validator';
import { HttpClient } from '@angular/common/http';
import { emailAvailabilityValidator } from '../../../../shared/validators/email-availability.validator';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { functions } from '../../../../shared/utils/functions';
import { ISignUp } from '../../interfaces/auth.interface';
import { Store } from '@ngrx/store';
import { AppState } from '../../../../app.state';
import {
  hideLoading1,
  showLoading1,
} from '../../../../shared/store/loading.actions';
import { getLoading1 } from '../../../../shared/store/loading.selectors';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  encapsulation: ViewEncapsulation.None, // Opcija za menjanje PrimeNG klase
})
export class RegisterComponent implements OnInit, OnDestroy {
  private dragLeaveSubject: Subject<boolean> = new Subject<boolean>();
  private dragLeave$ = this.dragLeaveSubject.asObservable();
  private dragOverSub: Subscription = new Subscription();
  private dragLeaveSub: Subscription = new Subscription();
  private isLoadingSub: Subscription = new Subscription();

  public fileUrl: string | undefined | null;
  public isChoosedFile: boolean = false;
  public isLoading: boolean = false;

  constructor(
    private readonly _messageService: MessageService,
    private readonly _fb: FormBuilder,
    private readonly _httpClient: HttpClient,
    private readonly _authService: AuthService,
    private readonly _router: Router,
    private readonly _store: Store<AppState>
  ) {}

  public registerForm: FormGroup = this._fb.group({
    isExpert: new FormControl<boolean | null>(null),
    firstName: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(12),
      Validators.pattern("^[a-zA-Z -']+"),
    ]),
    lastName: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(12),
      Validators.pattern("^[a-zA-Z -']+"),
    ]),
    username: new FormControl<string>('', {
      validators: [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(12),
        Validators.pattern('^[a-z0-9._]+([._]?[a-z0-9]+)*$'),
      ],
      asyncValidators: [usernameAvailabilityValidator(this._httpClient)],
    }),
    email: new FormControl<string>('', {
      validators: [
        Validators.required,
        Validators.maxLength(100),
        Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$'),
      ],
      asyncValidators: [emailAvailabilityValidator(this._httpClient)],
    }),
    password: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(32),
    ]),
    avatar: new FormControl<File | null>(null, [Validators.required]),
  });

  public isAllInputsValid(): boolean {
    if (this.registerForm.controls['firstName'].invalid) return false;
    if (this.registerForm.controls['lastName'].invalid) return false;
    if (this.registerForm.controls['username'].invalid) return false;
    if (this.registerForm.controls['email'].invalid) return false;
    if (this.registerForm.controls['password'].invalid) return false;

    return true;
  }

  public handleUserTypes(nextCallback: any) {
    if (this.registerForm.controls['isExpert'].value === null) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Molimo Vas da izaberete tip naloga.',
      });
      return;
    }

    nextCallback.emit();
  }

  public handleUserInfo(nextCallback: any) {
    if (this.registerForm.controls['firstName'].invalid)
      this.registerForm.controls['firstName'].markAsTouched();
    if (this.registerForm.controls['lastName'].invalid)
      this.registerForm.controls['lastName'].markAsTouched();
    if (this.registerForm.controls['username'].invalid)
      this.registerForm.controls['username'].markAsTouched();
    if (this.registerForm.controls['email'].invalid)
      this.registerForm.controls['email'].markAsTouched();
    if (this.registerForm.controls['password'].invalid)
      this.registerForm.controls['password'].markAsTouched();

    if (!this.isAllInputsValid()) return;

    nextCallback.emit();
    this.initEvents();
  }

  public handleChooseTypeExpert(
    currentEl: HTMLElement,
    oppositeEl: HTMLElement
  ) {
    currentEl.classList.toggle('active');
    oppositeEl.classList.remove('active');

    if (currentEl.classList.contains('active')) {
      this.registerForm.controls['isExpert'].setValue(true);
    } else {
      this.registerForm.controls['isExpert'].setValue(null);
    }
  }

  public handleChooseTypeUser(currentEl: HTMLElement, oppositeEl: HTMLElement) {
    currentEl.classList.toggle('active');
    oppositeEl.classList.remove('active');

    if (currentEl.classList.contains('active')) {
      this.registerForm.controls['isExpert'].setValue(false);
    } else {
      this.registerForm.controls['isExpert'].setValue(null);
    }
  }

  public onDrop(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();

    const file = event.dataTransfer?.files[0];

    if (!file) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Niste izabrali sliku.',
      });
      return;
    }

    if (
      !environment.IMAGE_EXTENSIONS.includes(
        file.name.split(file.name[file.name.lastIndexOf('.')])[1]
      )
    ) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Ekstenzija ovog fajla nije podrzana.',
      });
      return;
    }

    if (file.size > environment.PICTURE_MAX_UPLOAD_SIZE) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: `Velicina Vaseg fajla je ${functions.formatBytes(
          file.size
        )}, maksimalna velicina je ${functions.formatBytes(
          environment.PICTURE_MAX_UPLOAD_SIZE
        )}.`,
      });
      return;
    }

    this.fileUrl = window.URL.createObjectURL(file ?? new Blob());
    this.isChoosedFile = true;
    this.registerForm.controls['avatar'].setValue(file);
  }

  public onChooseFile(el: HTMLInputElement) {
    const file = el.files?.[0];

    if (!file) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Niste izabrali sliku.',
      });
      return;
    }

    if (
      !environment.IMAGE_EXTENSIONS.includes(
        file.name.split(file.name[file.name.lastIndexOf('.')])[1]
      )
    ) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Ekstenzija ovog fajla nije podrzana.',
      });
      return;
    }

    if (file.size > environment.PICTURE_MAX_UPLOAD_SIZE) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: `Velicina Vaseg fajla je ${functions.formatBytes(
          file.size
        )}, maksimalna velicina je ${functions.formatBytes(
          environment.PICTURE_MAX_UPLOAD_SIZE
        )}.`,
      });
      return;
    }

    this.fileUrl = window.URL.createObjectURL(file ?? new Blob());
    this.isChoosedFile = true;
    this.registerForm.controls['avatar'].setValue(file);
    el.value = '';
  }

  public onUnChooseFile() {
    this.fileUrl = null;
    this.isChoosedFile = false;
    this.registerForm.controls['avatar'].setValue(null);
  }

  public initEvents() {
    new Promise<HTMLElement>((resolve, reject) => {
      setTimeout(() => {
        const dragArea = document.getElementById('dragArea');
        if (!dragArea) return reject(null);
        return resolve(dragArea);
      }, 1);
    }).then((dragArea: HTMLElement) => {
      let isDragging = false;
      this.dragOverSub = fromEvent<DragEvent>(dragArea, 'dragover')
        .pipe(
          tap((evt: DragEvent) => {
            evt.preventDefault();
            evt.stopPropagation();
            if (!isDragging) {
              isDragging = true;
              dragArea.classList.add('active');
            }
          }),
          debounceTime(200)
        )
        .subscribe(() => {
          this.dragLeaveSubject.next(true);
        });
      this.dragLeaveSub = this.dragLeave$.subscribe(() => {
        isDragging = false;
        dragArea.classList.remove('active');
      });
    });
  }

  public onRegister() {
    if (this.registerForm.controls['avatar'].value === null) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Molimo Vas da izaberete sliku.',
      });
      return;
    }

    const user: ISignUp = {
      firstName: this.registerForm.get('firstName')?.value,
      lastName: this.registerForm.get('lastName')?.value,
      username: this.registerForm.get('username')?.value,
      email: this.registerForm.get('email')?.value,
      password: this.registerForm.get('password')?.value,
      isExpert: this.registerForm.get('isExpert')?.value,
      avatar: this.registerForm.get('avatar')?.value,
    };

    this._store.dispatch(showLoading1());

    this._authService.register(user).subscribe({
      next: (response) => {
        if (response.success) {
          this._messageService.add({
            severity: 'success',
            summary: 'Uspesno',
            detail: response.message,
          });

          this._router.navigateByUrl('/auth/login');
        } else {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: response.message,
          });
        }

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
    if (this.dragOverSub) {
      this.dragOverSub.unsubscribe();
    }
    if (this.dragLeaveSub) {
      this.dragLeaveSub.unsubscribe();
    }
    if (this.isLoadingSub) {
      this.isLoadingSub.unsubscribe();
    }
  }
}
