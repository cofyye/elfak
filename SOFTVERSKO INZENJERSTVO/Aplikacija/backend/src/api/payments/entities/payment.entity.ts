import { UserEntity } from 'src/shared/entities/user.entity';
import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';

@Entity({
  name: 'payments',
})
export class PaymentEntity {
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

  @Column({ name: 'payment_id', nullable: false })
  public paymentId: string;

  @Column({ name: 'payer_id', nullable: false })
  public payerId: string;

  @Column({ name: 'amount_paid', nullable: false, length: 10 })
  public amountPaid: string;

  @Column({ name: 'tokens', nullable: false, length: 10 })
  public tokens: string;

  @Column({ name: 'currency', length: 3, nullable: false })
  public currency: string;

  @Column({ name: 'method', length: 10, nullable: false })
  public method: string;

  @Column({ name: 'status', length: 15, nullable: false })
  public status: string;

  @Column({
    name: 'payment_date',
    type: 'bigint',
    nullable: false,
  })
  public paymentDate: number | string;
}
