import { ITag } from './tag.interface';

export interface IExpertAdvert {
  id: string;
  expertId: string;
  title: string;
  description: string;
  image_one: string;
  image_two: string;
  video: string;
  plans: string;
  active: boolean;
  createdAt: string;
  tags: ITag[];
}
