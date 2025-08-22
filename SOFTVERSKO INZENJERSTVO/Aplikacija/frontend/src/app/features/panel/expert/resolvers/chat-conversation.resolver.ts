import { Injectable, inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  ResolveFn,
  Router,
} from '@angular/router';
import { Observable, catchError, mergeMap, of } from 'rxjs';
import { ITag } from '../interfaces/tag.interface';
import { MessageService } from 'primeng/api';
import { ChatService } from '../services/chat.service';
import { IChatResponse } from '../interfaces/chat.interface';

@Injectable({
  providedIn: 'root',
})
class ChatConversationService {
  constructor(
    private readonly _chatService: ChatService,
    private readonly _messageService: MessageService,
    private readonly _router: Router
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    _: RouterStateSnapshot
  ): Observable<IChatResponse[]> {
    return this._chatService.getConversationList().pipe(
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

export const chatConversationResolver: ResolveFn<IChatResponse[]> = (
  route,
  state
) => {
  return inject(ChatConversationService).resolve(route, state);
};
