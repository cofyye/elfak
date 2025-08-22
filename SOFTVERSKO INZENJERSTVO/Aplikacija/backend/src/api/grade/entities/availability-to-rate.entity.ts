import { AdvertExpertEntity } from 'src/api/advert/entities/advert-expert.entity';
import { UserEntity } from 'src/shared/entities/user.entity';
import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';

@Entity({
  name: 'availability_to_rate',
})
export class AvailabilityToRateEntity {
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

  @ManyToOne(() => AdvertExpertEntity, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'ad_id' })
  @Column({ name: 'ad_id', nullable: false })
  public adId: string;

  @Column({
    name: 'created_at',
    type: 'bigint',
    nullable: false,
  })
  public createdAt: number | string;
}
