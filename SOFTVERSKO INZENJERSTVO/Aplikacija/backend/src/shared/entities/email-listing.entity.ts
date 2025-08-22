import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';

@Entity({
  name: 'email_listing',
})
export class EmailListingEntity {
  @PrimaryGeneratedColumn('uuid')
  public id: string;

  @Column({ name: 'email', unique: true, nullable: false, length: 100 })
  public email: string;

  @Column({
    name: 'created_at',
    type: 'bigint',
    nullable: false,
  })
  public createdAt: number | string;
}
