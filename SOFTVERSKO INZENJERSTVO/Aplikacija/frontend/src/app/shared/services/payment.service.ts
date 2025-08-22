import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPaymentInfo } from '../interfaces/payment.interface';
import {
  IAcceptResponse,
  IDataAcceptResponse,
} from '../interfaces/response.interface';
import { IPaginationData } from '../interfaces/pagination.interface';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  constructor(private http: HttpClient) {}

  getPayments(
    page = '1',
    take = '1',
    order = 'DESC'
  ): Observable<IDataAcceptResponse<IPaginationData<IPaymentInfo[]>>> {
    const url = `${environment.API_URL}/payment/get/own?page=${page}&take=${take}&order=${order}`;
    return this.http.get<IDataAcceptResponse<IPaginationData<IPaymentInfo[]>>>(
      url
    );
  }

  getPaymentsServices(
    page = '1',
    take = '9',
    order = 'DESC'
  ): Observable<IDataAcceptResponse<IPaginationData<IPaymentInfo[]>>> {
    const url = `${environment.API_URL}/payment/get/form/own?page=${page}&take=${take}&order=${order}`;
    return this.http.get<IDataAcceptResponse<IPaginationData<IPaymentInfo[]>>>(
      url
    );
  }

  createPayPalPayment(
    tokens: string,
    returnUrlPrefix: string
  ): Observable<IDataAcceptResponse<string>> {
    const url = `${environment.API_URL}/payment/paypal/create`;
    return this.http.post<IDataAcceptResponse<string>>(url, {
      tokens,
      returnUrlPrefix,
    });
  }

  createStripePayment(
    tokens: string,
    returnUrlPrefix: string
  ): Observable<IDataAcceptResponse<string>> {
    const url = `${environment.API_URL}/payment/stripe/create`;
    return this.http.post<IDataAcceptResponse<string>>(url, {
      tokens,
      returnUrlPrefix,
    });
  }

  successPayPalPayment(query: string): Observable<IAcceptResponse> {
    const url = `${environment.API_URL}/payment/paypal/success${query}`;
    return this.http.post<IAcceptResponse>(url, {});
  }

  successStripePayment(query: string): Observable<IAcceptResponse> {
    const url = `${environment.API_URL}/payment/stripe/success${query}`;
    return this.http.post<IAcceptResponse>(url, {});
  }
}
