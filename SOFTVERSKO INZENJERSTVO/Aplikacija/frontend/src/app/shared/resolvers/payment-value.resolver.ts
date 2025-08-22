import {
  ActivatedRouteSnapshot,
  ResolveFn,
  RouterStateSnapshot,
} from '@angular/router';
import { IProfileGet } from '../../features/panel/expert/interfaces/profile.interface';
import { inject } from '@angular/core';
import { ProfileService } from '../../features/panel/expert/services/profile.service';
import { IDataAcceptResponse } from '../interfaces/response.interface';

export const profileResolver: ResolveFn<IDataAcceptResponse<IProfileGet>> = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  return inject(ProfileService).getProfileInfo();
};
