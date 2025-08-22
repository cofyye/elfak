import {
  ActivatedRouteSnapshot,
  ResolveFn,
  RouterStateSnapshot,
} from '@angular/router';
import { inject } from '@angular/core';
import { IDataAcceptResponse } from '../../../shared/interfaces/response.interface';
import { AdvertService } from '../services/advert.service';
import { IAdvertInfo } from '../interfaces/advert-info.interface';

export const advertSingleResolver: ResolveFn<
  IDataAcceptResponse<IAdvertInfo>
> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const advertId = route.paramMap.get('advert_id')!;
  return inject(AdvertService).getAdvert(advertId);
};
