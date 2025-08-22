export interface ISocketMessage {
  text: string;
  senderId: string;
  receiverId: string;
  timestamp: string;
  profilePic: string;
  error?: string;
  success?: string;
  receiverName?: string;
  senderName?: string;
  profilePicReceiver?: string;
  profilePicSender?: string;
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
  status?: 'in_progress' | 'finished' | 'declined' | 'accepted';
}
