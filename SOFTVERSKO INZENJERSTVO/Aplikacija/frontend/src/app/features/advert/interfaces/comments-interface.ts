export interface IComment {
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
    bio: string | null;
    profession: string | null;
    avatar: string;
    registrationDate: string;
  };
}
