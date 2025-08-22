export interface IUser {
  id: string;
  firstName: string;
  lastName: string;
  username: string;
  avatar: string;
}

export interface IAnswer {
  id: string;
  message: string;
  createdAt: string;
  username: string;
  avatar: string;
  user: IUser;
}

export interface ITicket {
  id: string;
  title: string;
  message: string;
  createdAt: string;
  username: string;
  status: string;
  userId: string;
  type: string;
  answers: IAnswer[];
  user: IUser;
}

export interface IPaginationMeta {
  page: number;
  take: number;
  itemCount: number;
  pageCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface IPaginatedTickets {
  data: ITicket[];
  meta: IPaginationMeta;
}

export interface IApiResponse {
  success: boolean;
  data: IPaginatedTickets;
  message: string;
}
export interface IMessage {
  sender: string;
  message: string;
  date: string;
  avatar: string;
}
