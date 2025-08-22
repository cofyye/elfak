export interface IExpertAd {
  title: string;
  description: string;
  createdAt: string;
}

export interface IUserProfile {
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  role: string;
  avatar: string;
  isExpert: boolean;
  profession: string;
  bio: string;
  registrationDate: string;
  averageGrade: number;
  expert_ads: IExpertAd[];
}
