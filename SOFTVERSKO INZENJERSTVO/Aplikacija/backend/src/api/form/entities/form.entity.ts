import { AdvertExpertEntity } from 'src/api/advert/entities/advert-expert.entity';
import { UserEntity } from 'src/shared/entities/user.entity';
import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';
import { ChoosedPlan, PlanStatus } from '../enums/form.enum';

@Entity({
  name: 'forms',
})
export class FormEntity {
  @PrimaryGeneratedColumn('uuid')
  public id: string;

  @ManyToOne(() => AdvertExpertEntity, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'ad_id' })
  @Column({ name: 'ad_id', nullable: false })
  public adId: string;

  @ManyToOne(() => UserEntity, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'client_id' })
  @Column({ name: 'client_id', nullable: false })
  public clientId: string;

  @Column({ name: 'description', nullable: false })
  public description: string;

  @Column({ name: 'plans', nullable: false, type: 'longtext' })
  public plans: string;

  @Column({ name: 'price', nullable: true, length: 10 })
  public price: string;

  @Column({ name: 'accepted_expert', default: false, nullable: false })
  public accepted_expert: boolean;

  @Column({ name: 'accepted_client', default: true, nullable: false })
  public accepted_client: boolean;

  @Column({ name: 'accepted_client_chat', default: false, nullable: false })
  public accepted_client_chat: boolean;

  @Column({
    name: 'choosed_plan',
    nullable: false,
    type: 'enum',
    enum: ChoosedPlan,
  })
  public choosedPlan: ChoosedPlan;

  @Column({
    name: 'status',
    nullable: false,
    default: PlanStatus.IN_PROGRESS,
    type: 'enum',
    enum: PlanStatus,
  })
  public status: PlanStatus;

  @Column({
    name: 'datetime',
    type: 'datetime',
    nullable: false,
  })
  public dateTime: Date;

  @Column({
    name: 'endtime',
    type: 'datetime',
    nullable: true,
  })
  public endTime: Date;

  @Column({
    name: 'created_at',
    type: 'bigint',
    nullable: false,
  })
  public createdAt: number | string;

  public advert?: string;
}
