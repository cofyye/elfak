import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  IAcceptResponse,
  IDataAcceptResponse,
} from '../interfaces/response.interface';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class GradeService {
  constructor(private http: HttpClient) {}

  checkGradeAvailibility(adId: string): Observable<IAcceptResponse> {
    const url = `${environment.API_URL}/grade/availability-to-rate/check/${adId}`;
    return this.http.get<IAcceptResponse>(url);
  }

  createGrade(
    adId: string,
    grade: number,
    comment: string
  ): Observable<IAcceptResponse> {
    const url = `${environment.API_URL}/grade/create`;
    const body = { adId, grade, comment };
    return this.http.post<IAcceptResponse>(url, body);
  }
}
