import { UserEntity } from 'src/shared/entities/user.entity';
import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';
import { MessageStatus } from '../enums/chat.enum';

@Entity({
  name: 'chats',
})
export class ChatEntity {
  @PrimaryGeneratedColumn('uuid')
  public id: string;

  @ManyToOne(() => UserEntity, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'sender_id' })
  @Column({ name: 'sender_id', nullable: false })
  public senderId: string;

  @ManyToOne(() => UserEntity, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'receiver_id' })
  @Column({ name: 'receiver_id', nullable: false })
  public receiverId: string;

  @Column({ name: 'message', nullable: false })
  public message: string;

  @Column({
    name: 'status',
    nullable: false,
    default: MessageStatus.UNREAD,
    type: 'enum',
    enum: MessageStatus,
  })
  public status: MessageStatus;

  @Column({
    name: 'created_at',
    type: 'bigint',
    nullable: false,
  })
  public createdAt: number | string;
}
