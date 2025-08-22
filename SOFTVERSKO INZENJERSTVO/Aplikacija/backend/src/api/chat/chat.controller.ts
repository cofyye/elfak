import {
  Body,
  Controller,
  Get,
  HttpCode,
  HttpStatus,
  Param,
  Post,
  Req,
  UseGuards,
} from '@nestjs/common';
import { ChatSevice } from './chat.service';
import { AuthenticatedGuard } from 'src/shared/guards/authenticated.guard';
import { functions } from 'src/shared/utils/functions';
import {
  IDataSendResponse,
  ISendResponse,
} from 'src/shared/interfaces/response.interface';
import { Request } from 'express';
import { UserEntity } from 'src/shared/entities/user.entity';
import { CreateMessageDto } from './dtos/create-message.dto';
import { ChatEntity } from './entities/chat.entity';

@Controller('/chat')
@UseGuards(AuthenticatedGuard)
export class ChatController {
  constructor(private readonly _chatService: ChatSevice) {}

  /**
   * Ova ruta se koristi kako bi korisnik pogledao sve korisnike sa kim se dopisuje.
   * Korisnik poziva ovu rutu.
   * Uzimaju se sve poruke iz baze kome pripadaju logovanom korisniku i vracaju se.
   */
  @Get('/get/own')
  @HttpCode(HttpStatus.OK)
  public async getConversationList(
    @Req() req: Request,
  ): Promise<IDataSendResponse<ChatEntity[]>> {
    try {
      const user: UserEntity = req.user as UserEntity;

      const conversations = await this._chatService.getConversationList(
        user.id,
      );

      return {
        success: true,
        data: conversations,
        message: 'Uspesno.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja konverzacija.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi korisnik pogledao poruke od jednog korisnika.
   * Korisnik poziva ovu rutu.
   * Uzimaju se sve poruke iz baze kome pripadaju logovanom korisniku i vracaju se.
   */
  @Get('/get/:userId/messages')
  @HttpCode(HttpStatus.OK)
  public async getMessagesFromUser(
    @Req() req: Request,
    @Param('userId') userId: string,
  ): Promise<IDataSendResponse<ChatEntity[]>> {
    try {
      const user: UserEntity = req.user as UserEntity;

      if (user.id == userId) {
        functions.throwHttpException(
          false,
          'Ne mozete vratiti svoje poruke.',
          HttpStatus.CONFLICT,
        );
      }

      const messages = await this._chatService.getMessagesFromUser(
        user.id,
        userId,
      );

      return {
        success: true,
        data: messages,
        message: 'Uspesno.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja poruka.',
      );
    }
  }

  /**
   * Ova ruta se koristi kako bi korisnik poslao poruku drugom korisniku.
   * Korisnik poziva ovu rutu i salje body.
   * Ukoliko je poruka poslata, vraca true, u suprotnom false.
   */
  @Post('/create')
  @HttpCode(HttpStatus.CREATED)
  public async createMessage(
    @Body() body: CreateMessageDto,
    @Req() req: Request,
  ): Promise<ISendResponse> {
    try {
      const user: UserEntity = req.user as UserEntity;

      if (user.id == body.receiverId) {
        functions.throwHttpException(
          false,
          'Ne mozete poslati poruku sami sebi.',
          HttpStatus.CONFLICT,
        );
      }

      const message = await this._chatService.createMessage(user, body);

      if (!message) {
        functions.throwHttpException(
          false,
          'Doslo je do greske prilikom slanja poruke.',
          HttpStatus.INTERNAL_SERVER_ERROR,
        );
      }

      return {
        success: true,
        message: 'Uspesno.',
      };
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom slanja poruke.',
      );
    }
  }
}
