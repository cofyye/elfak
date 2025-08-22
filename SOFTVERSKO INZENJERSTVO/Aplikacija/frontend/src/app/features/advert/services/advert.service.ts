import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import {
  IAcceptResponse,
  IDataAcceptResponse,
} from '../../../shared/interfaces/response.interface';
import { IPaginationData } from '../../../shared/interfaces/pagination.interface';
import { IAdvertInfo } from '../interfaces/advert-info.interface';
import { IAdvertComment } from '../interfaces/comment-interface';
import { ICreateForm } from '../interfaces/create-form.interface';

@Injectable()
export class AdvertService {
  constructor(private http: HttpClient) {}

  getAdvert(advert_id: string): Observable<IDataAcceptResponse<IAdvertInfo>> {
    return this.http.get<IDataAcceptResponse<IAdvertInfo>>(
      `${environment.API_URL}/advert/expert/get/${advert_id}`
    );
  }

  getAdverts(
    page = '1',
    take = '1',
    search = '',
    sortBy = 'createdAt',
    order = 'DESC'
  ): Observable<IDataAcceptResponse<IPaginationData<IAdvertInfo[]>>> {
    const validSortFields = [
      'title',
      'description',
      'grade',
      'createdAt',
      'id',
      'averageGrade',
    ];
    if (!validSortFields.includes(sortBy)) {
      sortBy = 'createdAt';
    }
    if (!['ASC', 'DESC'].includes(order)) {
      order = 'DESC';
    }

    return this.http.get<IDataAcceptResponse<IPaginationData<IAdvertInfo[]>>>(
      `${environment.API_URL}/advert/expert/get/all?page=${page}&take=${take}&search=${search}&sortBy=${sortBy}&order=${order}`
    );
  }

  getAdvertComments(
    advert_id: string
  ): Observable<IDataAcceptResponse<IPaginationData<IAdvertComment[]>>> {
    return this.http.get<
      IDataAcceptResponse<IPaginationData<IAdvertComment[]>>
    >(`${environment.API_URL}/advert/expert/get/${advert_id}/comments`);
  }

  createForm(data: ICreateForm) {
    return this.http.post<IAcceptResponse>(
      `${environment.API_URL}/form/create`,
      data
    );
  }

  getForm(
    client_id: string,
    expert_id: string
  ): Observable<IDataAcceptResponse<IAdvertInfo>> {
    return this.http.get<IDataAcceptResponse<IAdvertInfo>>(
      `${environment.API_URL}/form/get/${client_id}/${expert_id}`
    );
  }
}
