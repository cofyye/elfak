import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { IDataAcceptResponse } from '../../../../shared/interfaces/response.interface';
import {
  IChatResponse,
  IChatResponseUser,
  IFormResponseUser,
  ISocketMessage,
} from '../interfaces/chat.interface';
import { Socket } from 'ngx-socket-io';

@Injectable()
export class ChatService {
  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _socket: Socket
  ) {}

  getConversationList() {
    return this._httpClient.get<IDataAcceptResponse<IChatResponse[]>>(
      `${environment.API_URL}/chat/get/own`
    );
  }

  getMessagesFromUser(userId: string) {
    return this._httpClient.get<IDataAcceptResponse<IChatResponseUser[]>>(
      `${environment.API_URL}/chat/get/${userId}/messages`
    );
  }

  getForm(userId: string) {
    return this._httpClient.get<IDataAcceptResponse<IFormResponseUser>>(
      `${environment.API_URL}/form/get/${userId}`
    );
  }

  sendNewMessage(data: ISocketMessage) {
    this._socket.emit('SEND_MESSAGE', data);
  }

  sendNewForm(data: ISocketMessage) {
    this._socket.emit('SEND_FORM', data);
  }

  sendDeclineForm(data: Partial<ISocketMessage>) {
    this._socket.emit('SEND_DECLINE_FORM', data);
  }

  sendAcceptForm(data: Partial<ISocketMessage>) {
    this._socket.emit('SEND_ACCEPT_FORM', data);
  }

  getNewMessage() {
    return this._socket.fromEvent<ISocketMessage>('NEW_MESSAGE');
  }

  getNewMessageChatPart() {
    return this._socket.fromEvent<ISocketMessage>('NEW_MESSAGE_CHAT_PART');
  }

  getNewDeclineForm() {
    return this._socket.fromEvent<ISocketMessage>('NEW_DECLINE_FORM');
  }

  getNewAcceptForm() {
    return this._socket.fromEvent<ISocketMessage>('NEW_ACCEPT_FORM');
  }
}
