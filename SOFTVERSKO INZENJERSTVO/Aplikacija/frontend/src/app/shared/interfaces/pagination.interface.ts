export interface IPaginationMetaData {
  page?: number;
  take?: number;
  itemCount?: number;
  pageCount?: number;
  hasPreviousPage?: boolean;
  hasNextPage?: boolean;
}

export interface IPaginationData<T> {
  data?: T;
  meta?: IPaginationMetaData;
}
