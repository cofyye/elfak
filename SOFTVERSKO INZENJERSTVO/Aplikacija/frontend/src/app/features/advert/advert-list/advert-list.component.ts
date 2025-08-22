import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IPaginationData } from '../../../shared/interfaces/pagination.interface';
import { IAdvertInfo } from '../interfaces/advert-info.interface';
import { AdvertService } from '../services/advert.service';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-advert-list',
  templateUrl: './advert-list.component.html',
  styleUrls: ['./advert-list.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class AdvertListComponent implements OnInit {
  environment = environment;
  options = [
    {
      name: 'Oceni',
      value: 'averageGrade',
      order: 'DESC',
      icon: 'fa-regular fa-star-shooting',
    },
    {
      name: 'Naslovu (A-Z)',
      value: 'title',
      order: 'ASC',
      icon: 'fas fa-arrow-up-a-z',
    },
    {
      name: 'Naslovu (Z-A)',
      value: 'title',
      order: 'DESC',
      icon: 'fas fa-arrow-down-a-z',
    },
    {
      name: 'Datumu kreiranja (Najnoviji)',
      value: 'createdAt',
      order: 'DESC',
      icon: 'fas fa-arrow-up',
    },
    {
      name: 'Datumu kreiranja (Najstariji)',
      value: 'createdAt',
      order: 'ASC',
      icon: 'fas fa-arrow-down',
    },
  ];
  rowsPerPageOptions = [12, 24, 48];
  selectedOption: any;
  adverts: IPaginationData<IAdvertInfo[]> = {};
  first = 0;
  rows = 10;
  reportMessage = '';
  searchTerm: string = '';

  constructor(
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _advertService: AdvertService
  ) {}

  onPageChange(event: any) {
    this.first = event.first;
    this.rows = event.rows;

    this.fetchAdverts();
  }

  filterAds() {
    this.fetchAdverts();
  }

  filterAdsDropdown() {
    this.fetchAdverts();
  }

  ngOnInit(): void {
    this.first = 0;
    this.rows = this.rowsPerPageOptions[0];
    this.fetchAdverts();
  }

  private fetchAdverts() {
    this._advertService
      .getAdverts(
        (this.first / this.rows + 1).toString(),
        this.rows.toString(),
        this.searchTerm.trim(),
        this.selectedOption?.value ?? '',
        this.selectedOption?.order ?? ''
      )
      .subscribe((response) => {
        this.adverts = response.data;
        window.scrollTo({ top: 0, behavior: 'smooth' });
        this.updateReportMessage();
        console.log(this.adverts.data);
      });
  }

  private updateReportMessage() {
    const page = this.first / this.rows + 1;
    const end = Math.min(page * this.rows, this.adverts.meta?.itemCount ?? 1);
    this.reportMessage = `Prikazano ${this.first + 1} do ${end} od ukupno ${
      this.adverts.meta?.itemCount ?? 1
    } oglasa`;
  }
}
