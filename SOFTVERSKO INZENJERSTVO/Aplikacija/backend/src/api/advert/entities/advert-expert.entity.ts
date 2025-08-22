import { GradeEntity } from 'src/api/grade/entities/grade.entity';
import { TagEntity } from 'src/shared/entities/tag.entity';
import { UserEntity } from 'src/shared/entities/user.entity';
import {
  Column,
  Entity,
  JoinColumn,
  JoinTable,
  ManyToMany,
  ManyToOne,
  OneToMany,
  PrimaryGeneratedColumn,
} from 'typeorm';

@Entity({
  name: 'ads_expert',
})
export class AdvertExpertEntity {
  @PrimaryGeneratedColumn('uuid')
  public id: string;

  @Column({ name: 'expert_id', nullable: false })
  public expertId: string;

  @Column({ name: 'title', nullable: false, length: 100 })
  public title: string;

  @Column({ name: 'description', nullable: false, type: 'text' })
  public description: string;

  @Column({ name: 'image_one', nullable: true, type: 'text' })
  public image_one: string;

  @Column({ name: 'image_two', nullable: true, type: 'text' })
  public image_two: string;

  @Column({ name: 'video', nullable: true, type: 'text' })
  public video: string;

  @Column({ name: 'plans', nullable: false, type: 'longtext' })
  public plans: string;

  @Column({ name: 'active', default: true, nullable: false })
  public active: boolean;

  @Column({
    name: 'created_at',
    type: 'bigint',
    nullable: false,
  })
  public createdAt: number | string;

  @ManyToOne(() => UserEntity, (user) => user.expert_ads, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'expert_id' })
  public expert?: UserEntity;

  @OneToMany(() => GradeEntity, (grade) => grade.ad)
  public grades?: GradeEntity[];

  @ManyToMany(() => TagEntity, (tag) => tag.expert_ads, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinTable({
    name: 'ads_expert_tags',
    joinColumn: {
      name: 'ad_id',
      referencedColumnName: 'id',
    },
    inverseJoinColumn: {
      name: 'tag_id',
      referencedColumnName: 'id',
    },
  })
  tags?: TagEntity[];

  averageGrade?: number;
  canGrade?: boolean;
}
