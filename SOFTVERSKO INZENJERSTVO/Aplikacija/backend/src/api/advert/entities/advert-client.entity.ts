import { UserEntity } from 'src/shared/entities/user.entity';
import {
  Column,
  CreateDateColumn,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';

@Entity({
  name: 'ads_client',
})
export class AdvertClientEntity {
  @PrimaryGeneratedColumn('uuid')
  public id: string;

  @ManyToOne(() => UserEntity, (user) => user.id, {
    lazy: true,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinColumn({ name: 'client_id' })
  public expert: Promise<UserEntity>;

  @Column({ name: 'title', nullable: false, length: 100 })
  public title: string;

  @Column({ name: 'description', nullable: false, type: 'text' })
  public description: string;

  @Column({ name: 'image_one', nullable: true, length: 100 })
  public image_one: string;

  @Column({ name: 'image_two', nullable: true, length: 100 })
  public image_two: string;

  @Column({ name: 'active', default: true, nullable: false })
  public active: boolean;

  @CreateDateColumn({
    name: 'created_at',
    type: 'datetime',
    nullable: false,
  })
  public createdAt: Date;
}
