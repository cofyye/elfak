import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { ITag } from '../interfaces/tag.interface';
import {
  IAcceptResponse,
  IDataAcceptResponse,
} from '../../../../shared/interfaces/response.interface';
import { ICreateExpertAdvert } from '../interfaces/ad.interface';
import { IExpertAdvert } from '../interfaces/ad-list.interface';

@Injectable()
export class AdvertService {
  constructor(private readonly _httpClient: HttpClient) { }

  getAllTags() {
    return this._httpClient.get<IDataAcceptResponse<ITag[]>>(
      `${environment.API_URL}/advert/tag/all`
    );
  }

  createExpertAdvert(data: ICreateExpertAdvert) {
    const formData = new FormData();

    formData.append('title', data.title);
    formData.append('description', data.description);
    formData.append('active', '1');

    if (data.image_one) {
      formData.append('image_one', data.image_one);
    }
    if (data.image_two) {
      formData.append('image_two', data.image_two);
    }
    if (data.video) {
      formData.append('video', data.video);
    }

    formData.append('plans', data.plans);
    formData.append('tags', data.tags);

    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/advert/expert/create`,
      formData
    );
  }

  getExpertAds() {
    return this._httpClient.get<IDataAcceptResponse<IExpertAdvert[]>>(
      `${environment.API_URL}/advert/expert/get/own`
    );
  }

  deleteAdvert(id: string) {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/advert/expert/delete/${id}`,
      {}
    );
  }

  getAdvertById(adId: string) {
    return this._httpClient.get<IDataAcceptResponse<IExpertAdvert>>(
      `${environment.API_URL}/advert/expert/get/${adId}`
    );
  }

  editAdvert(adId: string, data: any) {
    return this._httpClient.post<IAcceptResponse>(
      `${environment.API_URL}/advert/expert/edit/${adId}`,
      data
    );
  }
}
