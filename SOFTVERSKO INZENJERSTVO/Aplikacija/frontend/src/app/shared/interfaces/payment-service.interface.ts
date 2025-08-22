export interface IPayment {
  id: string;
  userId: string;
  title: string;
  tokens: string;
  paymentDate: string;
}

export interface IPaginationMeta {
  page: number;
  take: number;
  itemCount: number;
  pageCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface IPaginatedPayments {
  data: IPayment[];
  meta: IPaginationMeta;
}

export interface IApiResponse {
  success: boolean;
  data: IPaginatedPayments;
  message: string;
}
