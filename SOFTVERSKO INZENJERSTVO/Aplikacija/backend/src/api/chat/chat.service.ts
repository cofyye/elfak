import { HttpStatus, Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { ChatEntity } from './entities/chat.entity';
import { Repository } from 'typeorm';
import { UserEntity } from 'src/shared/entities/user.entity';
import { functions } from 'src/shared/utils/functions';
import { CreateMessageDto } from './dtos/create-message.dto';
import * as moment from 'moment';
import { FormSevice } from '../form/form.service';
import { FormEntity } from '../form/entities/form.entity';

@Injectable()
export class ChatSevice {
  constructor(
    @InjectRepository(ChatEntity)
    private readonly _chatRepo: Repository<ChatEntity>,
    @InjectRepository(UserEntity)
    private readonly _userRepo: Repository<UserEntity>,
    @InjectRepository(FormEntity)
    private readonly _formRepo: Repository<FormEntity>,
    private readonly _formService: FormSevice,
  ) {}

  public async getConversationList(userId: string): Promise<ChatEntity[]> {
    try {
      const rawQuery = `
      SELECT
          sub.conversationUserId,
          JSON_OBJECT(
              'id', u.id,
              'firstName', u.first_name,
              'lastName', u.last_name,
              'username', u.username,
              'avatar', u.avatar
          ) AS user,
          sub.id,
          sub.message,
          sub.status,
          sub.createdAt,
          sub.sentBy
      FROM (
          SELECT
              IF(m.sender_id = ?, m.receiver_id, m.sender_id) AS conversationUserId,
              m.id,
              m.message,
              m.status,
              m.created_at AS createdAt,
              IF(m.sender_id = ?, 'me', 'other') AS sentBy
          FROM chats m
          WHERE (m.sender_id = ? OR m.receiver_id = ?)
          
          UNION ALL
          
          SELECT
              IF(f.client_id = ?, a.expert_id, f.client_id) AS conversationUserId,
              f.id,
              'Nova konsultacija' AS message,
              NULL AS status,
              f.created_at AS createdAt,
              IF(f.client_id = ?, 'me', 'other') AS sentBy
          FROM forms f
          JOIN ads_expert a ON a.id = f.ad_id
          WHERE (f.client_id = ? OR a.expert_id = ?)
      ) sub
      JOIN users u ON u.id = sub.conversationUserId
      INNER JOIN (
          SELECT
              conversationUserId,
              MAX(createdAt) AS maxCreatedAt
          FROM (
              SELECT
                  IF(m.sender_id = ?, m.receiver_id, m.sender_id) AS conversationUserId,
                  m.created_at AS createdAt
              FROM chats m
              WHERE (m.sender_id = ? OR m.receiver_id = ?)
              
              UNION ALL
              
              SELECT
                  IF(f.client_id = ?, a.expert_id, f.client_id) AS conversationUserId,
                  f.created_at AS createdAt
              FROM forms f
              JOIN ads_expert a ON a.id = f.ad_id
              WHERE (f.client_id = ? OR a.expert_id = ?)
          ) latest
          GROUP BY conversationUserId
      ) latest
      ON sub.conversationUserId = latest.conversationUserId AND sub.createdAt = latest.maxCreatedAt
      ORDER BY sub.createdAt DESC;
  `;

      const results = await this._chatRepo.query(rawQuery, [
        userId,
        userId,
        userId,
        userId,
        userId,
        userId,
        userId,
        userId,
        userId,
        userId,
        userId,
        userId,
        userId,
        userId,
        userId,
      ]);

      return results.map((item: any) => ({
        ...item,
        user: JSON.parse(item.user),
        createdAt:
          moment.unix(item.createdAt).format('DD.MM.YYYY. HH:mm') + 'h',
      }));
    } catch (err) {
      console.log(err);
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja konverzacija.',
      );
    }
  }

  public async getMessagesFromUser(
    senderId: string,
    receiverId: string,
  ): Promise<ChatEntity[]> {
    try {
      const user = await this._userRepo.findOne({
        where: {
          id: receiverId,
        },
      });

      if (!user) {
        functions.throwHttpException(
          false,
          'Korisnik ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      const query = `
        SELECT 
          message.id,
          message.message,
          message.status,
          message.created_at AS createdAt,
          JSON_OBJECT(
            "id", sender.id,
            "firstName", sender.first_name,
            "lastName", sender.last_name,
            "username", sender.username,
            "avatar", sender.avatar
          ) AS sender,
          JSON_OBJECT(
            "id", receiver.id,
            "firstName", receiver.first_name,
            "lastName", receiver.last_name,
            "username", receiver.username,
            "avatar", receiver.avatar
          ) AS receiver,
          CASE
            WHEN message.sender_id = '${senderId}' THEN 'me'
            ELSE 'other'
          END AS sentBy
        FROM 
          chats AS message
        LEFT JOIN 
          users AS sender ON sender.id = message.sender_id
        LEFT JOIN 
          users AS receiver ON receiver.id = message.receiver_id
        WHERE
          (message.sender_id = '${senderId}' AND message.receiver_id = '${receiverId}') OR (message.sender_id = '${receiverId}' AND message.receiver_id = '${senderId}')
        ORDER BY 
          message.created_at ASC
        LIMIT 
          50
      `;

      const results = await this._chatRepo.query(query);

      return results.map((item: any) => ({
        ...item,
        createdAt:
          moment.unix(item.createdAt).format('DD.MM.YYYY. HH:mm') + 'h',
        sender: JSON.parse(item.sender),
        receiver: JSON.parse(item.receiver),
      }));
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom vracanja poruka.',
      );
    }
  }

  public async createMessage(
    user: UserEntity,
    body: CreateMessageDto,
  ): Promise<ChatEntity> {
    try {
      const validReceiver = await this._userRepo.findOne({
        where: {
          id: body.receiverId,
        },
      });

      if (!validReceiver) {
        functions.throwHttpException(
          false,
          'Korisnik kome zelite da posaljete poruku ne postoji.',
          HttpStatus.NOT_FOUND,
        );
      }

      const form = await this._formService.getForm(user.id, body.receiverId);

      if (!form || (form.clientId == user.id && !form.accepted_client_chat)) {
        functions.throwHttpException(
          false,
          'Jos uvek ne mozete da saljete poruke.',
          HttpStatus.NOT_FOUND,
        );
      }

      if (form.clientId != user.id) {
        form.accepted_client = !!form.accepted_client;
        form.accepted_expert = !!form.accepted_expert;
        form.accepted_client_chat = true;
        await this._formRepo.save(form);
      }

      const message = new ChatEntity();
      message.receiverId = body.receiverId;
      message.message = body.message;
      message.senderId = user.id;
      message.createdAt = moment().unix();

      return await this._chatRepo.save(this._chatRepo.create(message));
    } catch (err) {
      functions.handleHttpException(
        err,
        false,
        'Doslo je do greske prilikom slanja poruke.',
      );
    }
  }
}
