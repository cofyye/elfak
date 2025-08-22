import { AdvertExpertEntity } from 'src/api/advert/entities/advert-expert.entity';
import { UserEntity } from 'src/shared/entities/user.entity';
import { Grade } from 'src/shared/enums/grade.enum';
import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';

@Entity({
  name: 'grades',
})
export class GradeEntity {
  @PrimaryGeneratedColumn('uuid')
  public id: string;

  @Column({ name: 'user_id', nullable: false })
  public userId: string;

  @Column({ name: 'ad_id', nullable: false })
  public adId: string;

  @Column({ name: 'grade', nullable: false, type: 'enum', enum: Grade })
  public grade: Grade;

  @Column({ name: 'comment', nullable: false, type: 'text' })
  public comment: string;

  @Column({
    name: 'created_at',
    type: 'bigint',
    nullable: false,
  })
  public createdAt: number | string;

  @ManyToOne(() => UserEntity, (user) => user.grades, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'user_id' })
  public user?: UserEntity;

  @ManyToOne(() => AdvertExpertEntity, (adExpert) => adExpert.grades, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'ad_id' })
  public ad?: AdvertExpertEntity;
}
