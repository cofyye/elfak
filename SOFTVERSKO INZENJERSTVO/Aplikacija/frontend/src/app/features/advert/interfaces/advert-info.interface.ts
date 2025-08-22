import { IExpert } from './expert-info.interface';

export interface IAdvertInfo {
  id: string;
  averageGrade: number;
  canGrade: boolean;
  expertId: string;
  title: string;
  description: string;
  active: boolean;
  createdAt: string;
  image_one: string;
  image_two: string;
  video: string;
  plans: string;
  expert: IExpert;
}
