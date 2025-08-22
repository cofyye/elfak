export interface ISendResponse {
  success: boolean;
  message: string;
}

export interface IDataSendResponse<T> extends ISendResponse {
  data: T;
}

export interface IClassValidatorResponse {
  error: string;
  message: string[];
  statusCode: number;
}
