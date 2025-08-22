import { HttpClient } from '@angular/common/http';
import { AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { environment } from '../../../environments/environment';
import {
  debounceTime,
  distinctUntilChanged,
  first,
  map,
  switchMap,
} from 'rxjs';
import { IDataAcceptResponse } from '../interfaces/response.interface';

export function emailAvailabilityValidator(
  _httpClient: HttpClient
): AsyncValidatorFn {
  return (control: AbstractControl) => {
    return control.valueChanges.pipe(
      debounceTime(500),
      distinctUntilChanged(),
      switchMap((value) =>
        _httpClient.post<IDataAcceptResponse<boolean>>(
          `${environment.API_URL}/users/availability/email/${value}`,
          {}
        )
      ),
      map((data) =>
        data.success ? (!data.data ? null : { emailAvailable: true }) : null
      ),
      first()
    );
  };
}
