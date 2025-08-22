import {
  ActivatedRouteSnapshot,
  ResolveFn,
  RouterStateSnapshot,
} from '@angular/router';
import { inject } from '@angular/core';
import { IDataAcceptResponse } from '../../../shared/interfaces/response.interface';
import { IPaginationData } from '../../../shared/interfaces/pagination.interface';
import { IAdvertComment } from '../interfaces/comment-interface';
import { AdvertService } from '../services/advert.service';

export const advertCommentsResolver: ResolveFn<
  IDataAcceptResponse<IPaginationData<IAdvertComment[]>>
> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const advertId = route.paramMap.get('advert_id')!;
  return inject(AdvertService).getAdvertComments(advertId);
};
