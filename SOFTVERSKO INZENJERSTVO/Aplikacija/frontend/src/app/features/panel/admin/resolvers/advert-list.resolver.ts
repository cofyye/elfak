import {
  ActivatedRouteSnapshot,
  ResolveFn,
  RouterStateSnapshot,
} from '@angular/router';
import { inject } from '@angular/core';
import { IDataAcceptResponse } from '../../../../shared/interfaces/response.interface';
import { AdvertService } from '../services/advert.service';
import { IExpertAdvert } from '../interfaces/ad-list.interface';

export const expertAdsResolver: ResolveFn<
  IDataAcceptResponse<IExpertAdvert[]>
> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  return inject(AdvertService).getExpertAds();
};
