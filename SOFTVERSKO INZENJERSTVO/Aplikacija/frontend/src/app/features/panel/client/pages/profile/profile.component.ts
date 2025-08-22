import {
  Component,
  ElementRef,
  OnInit,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import { IProfileInfo } from '../../interfaces/profile-info.interface';
import { IPasswordInfo } from '../../interfaces/password-info.interface';
import { IProfessionInfo } from '../../interfaces/profession-info.interface';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ProfileService } from '../../services/profile.service';
import {
  IFirstName,
  ILastName,
  IPassword,
  IProfession,
  IProfileGet,
  IUsername,
} from '../../interfaces/profile.interface';
import { ActivatedRoute } from '@angular/router';
import { IDataAcceptResponse } from '../../../../../shared/interfaces/response.interface';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Subject } from 'rxjs';
import { environment } from '../../../../../../environments/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class ProfileComponent implements OnInit {
  @ViewChild('inputFieldRef') inputFieldRef!: ElementRef;
  file: string =
    'https://primefaces.org/cdn/primeng/images/demo/avatar/amyelsner.png';

  valForm: FormGroup = this._fb.group({
    password: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(32),
    ]),
  });

  passwordInformation: IPasswordInfo[] = [
    { label: 'Trenutna sifra', type: false },
    { label: 'Nova sifra', type: true },
    { label: 'Potvrdite sifru', type: false },
  ];

  userInformation: IProfileInfo[] = [
    {
      label: 'Korisnicko ime',
      value: '',
      previousValue: '',
      inputVisible: false,
    },
    { label: 'Ime', value: '', previousValue: '', inputVisible: false },
    { label: 'Prezime', value: '', previousValue: '', inputVisible: false },
  ];
  passwordValues: string[] = [];

  constructor(
    private readonly _fb: FormBuilder,
    private _profileService: ProfileService,
    private _activatedRoute: ActivatedRoute,
    private _messageService: MessageService,
    private _confirmation: ConfirmationService
  ) {}

  ngOnInit(): void {
    const responseData = (
      this._activatedRoute.snapshot.data[
        'profile'
      ] as IDataAcceptResponse<IProfileGet>
    ).data;
    this.userInformation[0].value = responseData.username;
    this.userInformation[1].value = responseData.firstName;
    this.userInformation[2].value = responseData.lastName;

    this.file = `${environment.API_URL}/uploads/${responseData.avatar}`;
  }

  edit(index: number) {
    this.userInformation.forEach((info, i) => {
      if (i !== index) {
        info.inputVisible = false;
      }
    });
    this.userInformation[index].previousValue =
      this.userInformation[index].value;
    this.userInformation[index].inputVisible =
      !this.userInformation[index].inputVisible;

    if (this.userInformation[index].inputVisible) {
      setTimeout(() => {
        const inputField = this.inputFieldRef.nativeElement as HTMLInputElement;
        if (inputField) {
          inputField.focus();
        }
      });
    }
  }

  saveInput(index: number) {
    this.userInformation[index].inputVisible = false;

    if (
      this.userInformation[index].value ===
      this.userInformation[index].previousValue
    )
      return;

    if (this.userInformation[index].label === 'Ime') {
      const firstName: IFirstName = {
        firstName: this.userInformation[index].value,
      };
      this._profileService.changeName(firstName).subscribe({
        next: (response) => {},
        error: (error) => {
          this.userInformation[index].value =
            this.userInformation[index].previousValue;
        },
      });
    } else if (this.userInformation[index].label === 'Prezime') {
      const lastName: ILastName = {
        lastName: this.userInformation[index].value,
      };
      this._profileService.changeLastName(lastName).subscribe({
        next: (response) => {},
        error: (error) => {
          this.userInformation[index].value =
            this.userInformation[index].previousValue;
        },
      });
    } else if (this.userInformation[index].label === 'Korisnicko ime') {
      const username: IUsername = {
        username: this.userInformation[index].value,
      };
      this._profileService.changeUsername(username).subscribe({
        next: (response) => {},
        error: (error) => {
          this.userInformation[index].value =
            this.userInformation[index].previousValue;
        },
      });
    }
  }

  savePassword() {
    const passwordData: IPassword = {
      currentPassword: this.passwordValues[0],
      password: this.valForm.controls['password'].value,
      confirmPassword: this.passwordValues[2],
    };
    this._profileService.changePassword(passwordData).subscribe({
      next: (response) => {
        this.passwordValues[0] = '';
        this.passwordValues[2] = '';
        this.valForm.controls['password'].reset();
        this._messageService.add({
          severity: 'success',
          summary: 'Uspesno',
          detail: 'Uspesno ste promenili sifru.',
        });
      },
      error: (error) => {},
    });
  }

  clearPassword() {
    this.passwordValues[0] = '';
    this.passwordValues[2] = '';
    this.valForm.controls['password'].reset();
  }

  cancelEdit(index: number) {
    this.userInformation[index].inputVisible = false;
  }

  onFileChange(event: any) {
    const files = event.target.files as FileList;

    if (files.length > 0) {
      const _file = URL.createObjectURL(files[0]);
      this.file = _file;
      this.resetInput();
    }
  }

  resetInput() {
    const input = document.getElementById(
      'avatar-input-file'
    ) as HTMLInputElement;
    if (input) {
      input.value = '';
    }
  }

  canDeactivate() {
    if (
      !this.passwordValues[0] &&
      !this.passwordValues[2] &&
      !this.valForm.controls['password'].value
    )
      return true;

    const deactivateSubject = new Subject<boolean>();
    this._confirmation.confirm({
      message:
        'Imate nesacuvane promene. Da li ste sigurni da zelite da napustite ovu stranicu?',
      header: 'Potvrda',
      icon: 'pi pi-exclamation-triangle',
      acceptIcon: 'none',
      rejectIcon: 'none',
      rejectButtonStyleClass: 'p-button-text',
      acceptLabel: 'Da',
      rejectLabel: 'Ne',
      accept: () => {
        deactivateSubject.next(true);
      },
      reject: () => {
        deactivateSubject.next(false);
      },
    });
    return deactivateSubject;
  }
}
