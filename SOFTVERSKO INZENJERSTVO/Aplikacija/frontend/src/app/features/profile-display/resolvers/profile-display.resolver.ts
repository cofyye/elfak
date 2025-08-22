import {
  ActivatedRouteSnapshot,
  ResolveFn,
  RouterStateSnapshot,
} from '@angular/router';
import { inject } from '@angular/core';
import { IDataAcceptResponse } from '../../../shared/interfaces/response.interface';
import { UserProfileService } from '../service/profile-display.service';
import { IUserProfile } from '../interface/profile-display.interface';

export const userProfileResolver: ResolveFn<
  IDataAcceptResponse<IUserProfile>
> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const userId = route.paramMap.get('expert_id')!;
  return inject(UserProfileService).getUserProfile(userId);
};
