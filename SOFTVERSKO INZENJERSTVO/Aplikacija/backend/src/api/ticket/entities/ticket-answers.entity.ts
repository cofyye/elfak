import { UserEntity } from 'src/shared/entities/user.entity';
import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';
import { TicketEntity } from './ticket.entity';

@Entity({
  name: 'tickets_answers',
})
export class TicketAnswersEntity {
  @PrimaryGeneratedColumn('uuid')
  public id: string;

  @ManyToOne(() => TicketEntity, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'ticket_id' })
  @Column({ name: 'ticket_id', nullable: false })
  public ticketId: string;

  @Column({ name: 'answered_id', nullable: false })
  public answeredId: string;

  @Column({ name: 'message', nullable: false, length: 2000 })
  public message: string;

  @Column({
    name: 'created_at',
    type: 'bigint',
    nullable: false,
  })
  public createdAt: number | string;

  @ManyToOne(() => UserEntity, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'answered_id' })
  public user?: UserEntity;
}
