import { AdvertExpertEntity } from 'src/api/advert/entities/advert-expert.entity';
import { GradeEntity } from 'src/api/grade/entities/grade.entity';
import { UserRole } from 'src/shared/enums/role.enum';
import {
  Column,
  CreateDateColumn,
  Entity,
  OneToMany,
  PrimaryGeneratedColumn,
} from 'typeorm';

@Entity({
  name: 'users',
})
export class UserEntity {
  @PrimaryGeneratedColumn('uuid')
  public id: string;

  @Column({ name: 'first_name', nullable: false, length: 12 })
  public firstName: string;

  @Column({ name: 'last_name', nullable: false, length: 12 })
  public lastName: string;

  @Column({ name: 'username', unique: true, nullable: false, length: 12 })
  public username: string;

  @Column({ name: 'email', unique: true, nullable: false, length: 100 })
  public email: string;

  @Column({ name: 'password', nullable: false, length: 100 })
  public password: string;

  @Column({ name: 'verification_token', length: 6, nullable: true })
  public verificationToken: string;

  @Column({ name: 'verification_exp_date', type: 'datetime', nullable: true })
  public verificationExpDate: Date;

  @Column({ name: 'password_token', length: 12, nullable: true })
  public passwordToken: string;

  @Column({ name: 'password_exp_date', type: 'datetime', nullable: true })
  public passwordExpDate: Date;

  @Column({ name: 'active', default: false, nullable: false })
  public active: boolean;

  @Column({
    name: 'role',
    default: UserRole.USER,
    nullable: false,
    type: 'enum',
    enum: UserRole,
  })
  public role: UserRole;

  @Column({ name: 'is_expert', default: false, nullable: false })
  public isExpert: boolean;

  @Column({ name: 'profession', nullable: true, length: 50 })
  public profession: string;

  @Column({ name: 'bio', nullable: true, type: 'text' })
  public bio: string;

  @Column({ name: 'tokens', nullable: true, length: 10, default: '0' })
  public tokens: string;

  @Column({
    name: 'avatar',
    nullable: false,
    default: 'default-avatar.png',
    length: 100,
  })
  public avatar: string;

  @Column({
    name: 'registration_date',
    type: 'bigint',
    nullable: false,
  })
  public registrationDate: number | string;

  @OneToMany(() => AdvertExpertEntity, (adExpert) => adExpert.expert)
  public expert_ads?: AdvertExpertEntity[];

  @OneToMany(() => GradeEntity, (grade) => grade.user)
  public grades?: GradeEntity[];
}
