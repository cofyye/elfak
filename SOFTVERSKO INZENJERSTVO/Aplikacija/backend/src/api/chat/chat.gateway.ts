import { InjectRepository } from '@nestjs/typeorm';
import {
  OnGatewayConnection,
  OnGatewayDisconnect,
  SubscribeMessage,
  WebSocketGateway,
  WebSocketServer,
} from '@nestjs/websockets';
import { Server, Socket } from 'socket.io';
import { UserEntity } from 'src/shared/entities/user.entity';
import { Repository } from 'typeorm';
import { ChatSevice } from './chat.service';
import { ISocketMessage } from './interfaces/chat.interface';
import { CreateMessageDto } from './dtos/create-message.dto';
import { AcceptFormDto } from '../form/dtos/accept-form.dto';
import { FormSevice } from '../form/form.service';
import { HttpException, HttpStatus } from '@nestjs/common';
import { validate } from 'class-validator';
import * as moment from 'moment-timezone';
import { DeclineFormDto } from '../form/dtos/decline-form.dto';

@WebSocketGateway(1337, {
  cors: {
    origin: 'http://localhost:4200',
    credentials: true,
  },
})
export class ChatGateway implements OnGatewayConnection, OnGatewayDisconnect {
  constructor(
    @InjectRepository(UserEntity)
    private readonly _userRepo: Repository<UserEntity>,
    private readonly _chatService: ChatSevice,
    private readonly _formService: FormSevice,
  ) {}

  @WebSocketServer()
  server: Server;

  handleConnection(client: Socket, ...args: any[]) {
    // console.log(client.id);
  }

  handleDisconnect(client: Socket) {
    // console.log('disconnected');
  }

  @SubscribeMessage('SEND_MESSAGE')
  async handleNewMessage(socket: Socket, data: ISocketMessage): Promise<void> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          id: data.senderId,
        },
      });

      const dto = new CreateMessageDto();
      (dto.message = data.text), (dto.receiverId = data.receiverId);

      await this._chatService.createMessage(user, dto);

      this.server.emit('NEW_MESSAGE', {
        ...data,
        profilePic: process.env.BACKEND_URL + '/uploads/' + user.avatar,
      });

      const receiver = await this._userRepo.findOne({
        where: {
          id: data.receiverId,
        },
      });

      this.server.emit('NEW_MESSAGE_CHAT_PART', {
        ...data,
        receiverName: receiver.firstName + ' ' + receiver.lastName,
        senderName: user.firstName + ' ' + user.lastName,
        profilePicReceiver:
          process.env.BACKEND_URL + '/uploads/' + receiver.avatar,
        profilePicSender: process.env.BACKEND_URL + '/uploads/' + user.avatar,
      });
    } catch (err) {
      if (err instanceof HttpException) {
        this.server.emit('NEW_MESSAGE', {
          ...data,
          error: err.message,
        } as ISocketMessage);
      } else {
        this.server.emit('NEW_MESSAGE', {
          ...data,
          error: 'Greska prilikom slanja poruke.',
        } as ISocketMessage);
      }
    }
  }

  @SubscribeMessage('SEND_FORM')
  async handleNewForm(socket: Socket, data: ISocketMessage): Promise<void> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          id: data.senderId,
        },
      });

      const receiver = await this._userRepo.findOne({
        where: {
          id: data.receiverId,
        },
      });

      this.server.emit('NEW_MESSAGE_CHAT_PART', {
        ...data,
        receiverName: receiver.firstName + ' ' + receiver.lastName,
        senderName: user.firstName + ' ' + user.lastName,
        profilePicReceiver:
          process.env.BACKEND_URL + '/uploads/' + receiver.avatar,
        profilePicSender: process.env.BACKEND_URL + '/uploads/' + user.avatar,
      });
    } catch (err) {
      if (err instanceof HttpException) {
        this.server.emit('NEW_MESSAGE_CHAT_PART', {
          ...data,
          error: err.message,
        } as ISocketMessage);
      } else {
        this.server.emit('NEW_MESSAGE_CHAT_PART', {
          ...data,
          error: 'Greska prilikom primanja konsultacije.',
        } as ISocketMessage);
      }
    }
  }

  @SubscribeMessage('SEND_DECLINE_FORM')
  async handleDeclineForm(socket: Socket, data: ISocketMessage): Promise<void> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          id: data.senderId,
        },
      });

      const dto = new DeclineFormDto();
      dto.adId = data.adId;
      dto.clientId = data.clientId;

      const errors = await validate(dto);

      if (errors.length > 0) {
        errors.forEach((error) => {
          if (error.constraints.isNotEmpty) {
            throw new HttpException(
              error.constraints.isNotEmpty,
              HttpStatus.BAD_REQUEST,
            );
          }
        });
      }

      await this._formService.declineForm(user, dto);

      this.server.emit('NEW_DECLINE_FORM', data);
      this.server.emit('NEW_MESSAGE_CHAT_PART', data);
    } catch (err) {
      if (err instanceof HttpException) {
        this.server.emit('NEW_DECLINE_FORM', {
          ...data,
          error: err.message,
        } as ISocketMessage);
      } else {
        this.server.emit('NEW_DECLINE_FORM', {
          ...data,
          error: 'Greska prilikom odbijanja konsultacije.',
        } as ISocketMessage);
      }
    }
  }

  @SubscribeMessage('SEND_ACCEPT_FORM')
  async handleAcceptForm(socket: Socket, data: ISocketMessage): Promise<void> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          id: data.senderId,
        },
      });

      const dto = new AcceptFormDto();
      dto.adId = data.adId;
      dto.clientId = data.clientId;
      dto.dateTime = moment.tz(data.dateTime, 'Europe/Belgrade').toDate();
      dto.description = data.description;
      dto.plans = data.plans;
      dto.price = +data.price;

      const errors = await validate(dto);

      if (errors.length > 0) {
        errors.forEach((error) => {
          if (error.constraints.isNotEmpty) {
            throw new HttpException(
              error.constraints.isNotEmpty,
              HttpStatus.BAD_REQUEST,
            );
          }
          if (error.constraints.isInt) {
            throw new HttpException(
              error.constraints.isInt,
              HttpStatus.BAD_REQUEST,
            );
          }
          if (error.constraints.min) {
            throw new HttpException(
              error.constraints.min,
              HttpStatus.BAD_REQUEST,
            );
          }
          if (error.constraints.max) {
            throw new HttpException(
              error.constraints.max,
              HttpStatus.BAD_REQUEST,
            );
          }
          if (error.constraints.maxLength) {
            throw new HttpException(
              error.constraints.maxLength,
              HttpStatus.BAD_REQUEST,
            );
          }
          if (error.constraints.isDate) {
            throw new HttpException(
              error.constraints.isDate,
              HttpStatus.BAD_REQUEST,
            );
          }
        });
      }

      const acceptFormObj = await this._formService.acceptForm(user, dto);

      this.server.emit('NEW_ACCEPT_FORM', {
        ...data,
        success: acceptFormObj.msg,
        userType: acceptFormObj.user_type,
        accepted_client: acceptFormObj.accepted_client,
        accepted_expert: acceptFormObj.accepted_expert,
        status: acceptFormObj.status,
      } as ISocketMessage);
    } catch (err) {
      if (err instanceof HttpException) {
        this.server.emit('NEW_ACCEPT_FORM', {
          ...data,
          error: err.message,
        } as ISocketMessage);
      } else {
        this.server.emit('NEW_ACCEPT_FORM', {
          ...data,
          error: 'Greska prilikom prihvatanja konsultacije.',
        } as ISocketMessage);
      }
    }
  }
}
