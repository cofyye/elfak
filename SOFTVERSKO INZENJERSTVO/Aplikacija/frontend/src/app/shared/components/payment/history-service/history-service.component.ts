import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IPaymentInfo } from '../../../interfaces/payment.interface';
import { IPaginationData } from '../../../interfaces/pagination.interface';

@Component({
  selector: 'app-history-service',
  templateUrl: './history-service.component.html',
  styleUrls: ['./history-service.component.css'],
})
export class HistoryServiceComponent implements OnInit {
  productList: IPaymentInfo[] = [];
  first = 0;
  BILL_PER_PAGE = 9;
  searchTerm: string = '';
  totalRecords: number = 0;

  constructor(private readonly _activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    const data = this._activatedRoute.snapshot.data[
      'payments'
    ] as IPaginationData<IPaymentInfo[]>;
    if (data) {
      this.productList = data.data || [];
      this.totalRecords = data.meta!.itemCount || 0;
    }
  }

  onPageChange(event: any) {
    this.first = event.first;
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  getDisplayedBills(): IPaymentInfo[] {
    const startIndex = this.first;
    const endIndex = this.first + this.BILL_PER_PAGE;
    return this.productList.slice(startIndex, endIndex);
  }
}
