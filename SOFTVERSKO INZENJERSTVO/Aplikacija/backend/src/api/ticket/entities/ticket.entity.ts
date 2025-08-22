import { UserEntity } from 'src/shared/entities/user.entity';
import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';
import { TicketStatus, TicketType } from '../enums/ticket.enum';
import { TicketAnswersEntity } from './ticket-answers.entity';

@Entity({
  name: 'tickets',
})
export class TicketEntity {
  @PrimaryGeneratedColumn('uuid')
  public id: string;

  @Column({ name: 'user_id', nullable: false })
  public userId: string;

  @Column({ name: 'title', nullable: false, length: 100 })
  public title: string;

  @Column({ name: 'message', nullable: false, length: 2000 })
  public message: string;

  @Column({ name: 'username', nullable: true, length: 12 })
  public username: string;

  @Column({
    name: 'status',
    nullable: false,
    type: 'enum',
    enum: TicketStatus,
    default: TicketStatus.OPEN,
  })
  public status: TicketStatus;

  @Column({ name: 'type', nullable: false, type: 'enum', enum: TicketType })
  public type: TicketType;

  @Column({
    name: 'created_at',
    type: 'bigint',
    nullable: false,
  })
  public createdAt: number | string;

  answers?: TicketAnswersEntity[];

  @ManyToOne(() => UserEntity, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'user_id' })
  public user?: UserEntity;
}
