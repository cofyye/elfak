import { Injectable, inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  ResolveFn,
} from '@angular/router';
import { Observable, catchError, mergeMap, of } from 'rxjs';
import { MessageService } from 'primeng/api';
import { AdvertService } from '../services/advert.service';
import { IPaginationData } from '../../../shared/interfaces/pagination.interface';
import { IAdvertInfo } from '../interfaces/advert-info.interface';

@Injectable({
  providedIn: 'root',
})
class AdvertAllService {
  constructor(
    private readonly _advertService: AdvertService,
    private readonly _messageService: MessageService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    _: RouterStateSnapshot
  ): Observable<IPaginationData<IAdvertInfo[]>> {
    const page = route.queryParamMap.get('page') ?? '1';
    const take = route.queryParamMap.get('take') ?? '10';
    const search = route.queryParamMap.get('search') ?? '';
    const sortBy = route.queryParamMap.get('sortBy') ?? 'createdAt';
    const sortOrder = route.queryParamMap.get('order') ?? 'DESC';

    return this._advertService
      .getAdverts(page, take, search, sortBy, sortOrder)
      .pipe(
        mergeMap((response) => {
          if (response.success) {
            return of(response.data);
          } else {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail: response.message,
            });

            return of(response.data);
          }
        }),
        catchError(() => {
          return of();
        })
      );
  }
}

export const advertAllResolver: ResolveFn<IPaginationData<IAdvertInfo[]>> = (
  route,
  state
) => {
  return inject(AdvertAllService).resolve(route, state);
};
