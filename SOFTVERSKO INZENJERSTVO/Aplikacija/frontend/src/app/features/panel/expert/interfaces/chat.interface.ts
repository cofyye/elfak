export interface IChatInfo {
  userId: string;
  name: string;
  profilePicture: string;
  chat: string;
  chatStatus: string;
}

interface IChatUserInfo {
  id: string;
  firstName: string;
  lastName: string;
  username: string;
  avatar: string;
}

export interface IChatResponse {
  id: string;
  conversationUserId: string;
  user: IChatUserInfo;
  message: string;
  status: 'read' | 'unread';
  createdAt: Date;
  sentBy: 'me' | 'other';
}

export interface IChatResponseUser {
  id: string;
  message: string;
  status: 'read' | 'unread';
  createdAt: Date;
  sender: IChatUserInfo;
  receiver: IChatUserInfo;
  sentBy: 'me' | 'other';
}

export interface Message {
  sentBy: 'me' | 'other';
  text: string;
  profilePic: string;
  timestamp: string;
  sender?: IChatUserInfo;
  receiver?: IChatUserInfo;
}

export interface ISocketMessage {
  text: string;
  senderId: string;
  receiverId: string;
  timestamp: string;
  profilePic: string;
  receiverName?: string;
  senderName?: string;
  profilePicReceiver?: string;
  profilePicSender?: string;
  error?: string;
  success?: string;
  adId?: string;
  lastMessage?: string;
  isDeclinedForm?: boolean;
  clientId?: string;
  description?: string;
  price?: string;
  plans?: string;
  dateTime?: Date;
  userType?: 'client' | 'expert';
  accepted_expert?: boolean;
  accepted_client?: boolean;
  accepted_client_chat?: boolean;
  status?: 'in_progress' | 'finished' | 'declined' | 'accepted';
}

export interface IFormResponseUser {
  id: string;
  adId: string;
  clientId: string;
  description: string;
  plans: string[];
  accepted_expert: boolean;
  accepted_client: boolean;
  choosedPlan: 'basic' | 'standard' | 'premium' | 'custom';
  status: 'in_progress' | 'finished' | 'declined' | 'accepted';
  dateTime: Date;
  createdAt: string;
  price: string;
  userType: 'client' | 'expert';
  advert: {
    title: string;
  };
}

export interface IFormUser {
  id: string;
  adId: string;
  clientId: string;
  description: string;
  tasks: string[];
  accepted_expert: boolean;
  accepted_client: boolean;
  choosedPlan: string;
  status: string;
  date: string;
  time: string;
  price: string;
  userType: 'client' | 'expert';
  advert: {
    title: string;
  };
}
