import { UserEntity } from 'src/shared/entities/user.entity';
import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';

@Entity({
  name: 'payments_form_history',
})
export class PaymentFormHistoryEntity {
  @PrimaryGeneratedColumn('uuid')
  public id: string;

  @ManyToOne(() => UserEntity, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'user_id' })
  @Column({ name: 'user_id', nullable: false })
  public userId: string;

  @Column({ name: 'title', nullable: false })
  public title: string;

  @Column({ name: 'tokens', nullable: false })
  public tokens: string;

  @Column({
    name: 'payment_date',
    type: 'bigint',
    nullable: false,
  })
  public paymentDate: number | string;
}
