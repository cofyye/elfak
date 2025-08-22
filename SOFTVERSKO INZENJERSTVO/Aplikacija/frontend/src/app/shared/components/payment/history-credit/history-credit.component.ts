import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IPaymentInfo } from '../../../interfaces/payment.interface';
import { IPaginationData } from '../../../interfaces/pagination.interface';
@Component({
  selector: 'app-history-credit',
  templateUrl: './history-credit.component.html',
  styleUrls: ['./history-credit.component.css'],
})
export class HistoryCreditComponent implements OnInit {
  bilList: IPaymentInfo[] = [];
  first = 0;
  BILL_PER_PAGE = 10;
  searchTerm: string = '';

  constructor(private readonly _activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    const data = this._activatedRoute.snapshot.data[
      'payments'
    ] as IPaginationData<IPaymentInfo[]>;
    if (data) {
      this.bilList = data.data || [];
      this.first = (data.meta?.page ?? 1) - 1;
    }
  }

  onPageChange(event: any) {
    this.first = event.first;
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  getDisplayedBills(): IPaymentInfo[] {
    const startIndex = this.first;
    const endIndex = this.first + this.BILL_PER_PAGE;
    return this.bilList.slice(startIndex, endIndex);
  }
}
