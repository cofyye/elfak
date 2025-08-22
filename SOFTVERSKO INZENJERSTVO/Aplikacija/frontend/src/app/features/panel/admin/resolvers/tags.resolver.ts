import { Injectable, inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  ResolveFn,
  Router,
} from '@angular/router';
import { Observable, catchError, mergeMap, of } from 'rxjs';
import { ITag } from '../interfaces/tag.interface';
import { AdvertService } from '../services/advert.service';
import { MessageService } from 'primeng/api';
@Injectable({
  providedIn: 'root',
})
class TagService {
  constructor(
    private readonly _advertService: AdvertService,
    private readonly _messageService: MessageService,
    private readonly _router: Router
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    _: RouterStateSnapshot
  ): Observable<ITag[]> {
    return this._advertService.getAllTags().pipe(
      mergeMap((response) => {
        if (response.success) {
          return of(response.data);
        } else {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: response.message,
          });

          this._router.navigate(['/']);

          return of(response.data);
        }
      }),
      catchError(() => {
        this._router.navigate(['/']);
        return of([]);
      })
    );
  }
}

export const tagResolver: ResolveFn<ITag[]> = (route, state) => {
  return inject(TagService).resolve(route, state);
};
