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

interface TicketType {
  name: string;
  type: string;
}

export interface IMessage {
  sender: string;
  message: string;
  date: string;
  avatar: string;
}

export interface ITicketInput {
  userId: string;
  title: string;
  message: string;
  id?: string;
  username?: string;
  type: string;
}