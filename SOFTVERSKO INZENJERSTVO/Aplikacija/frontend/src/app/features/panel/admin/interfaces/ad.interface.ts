export interface IAd {
  name: string;
  description: string;
  active: boolean;
  price: number;
}

export interface ICreateExpertAdvert {
  title: string;
  description: string;
  active?: boolean;
  video?: File | null;
  image_one?: File | null;
  image_two?: File | null;
  plans: string;
  tags: string;
}
