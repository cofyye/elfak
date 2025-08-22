export interface IAdvertComment {
  id: string;
  userId: string;
  adId: string;
  grade: string;
  comment: string;
  createdAt: string;
  user: {
    firstName: string;
    lastName: string;
    username: string;
    bio: string;
    profession: string;
    avatar: string;
    registrationDate: string;
  };
}
