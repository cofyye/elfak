import { AdvertExpertEntity } from 'src/api/advert/entities/advert-expert.entity';
import {
  Column,
  Entity,
  JoinTable,
  ManyToMany,
  PrimaryGeneratedColumn,
} from 'typeorm';

@Entity({
  name: 'tags',
})
export class TagEntity {
  @PrimaryGeneratedColumn('uuid')
  public id: string;

  @Column({ name: 'name', unique: true, nullable: false, length: 20 })
  public name: string;

  @Column({ name: 'active', default: true, nullable: false })
  public active: boolean;

  @Column({
    name: 'created_at',
    type: 'bigint',
    nullable: false,
  })
  public createdAt: number | string;

  @ManyToMany(() => AdvertExpertEntity, (adExpert) => adExpert.tags, {
    nullable: false,
    onDelete: 'CASCADE',
    onUpdate: 'CASCADE',
  })
  @JoinTable()
  expert_ads: AdvertExpertEntity[];
}
