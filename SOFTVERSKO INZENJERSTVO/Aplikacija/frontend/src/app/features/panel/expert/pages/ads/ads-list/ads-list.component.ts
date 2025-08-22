import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IExpertAdvert } from '../../../interfaces/ad-list.interface';
import { environment } from '../../../../../../../environments/environment';
import { AdvertService } from '../../../services/advert.service';

@Component({
  selector: 'app-ads-list',
  templateUrl: './ads-list.component.html',
  styleUrls: ['./ads-list.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class AdsListComponent implements OnInit {
  ads: IExpertAdvert[] = [];
  apiUrl: string = environment.API_URL;
  confirmDialogVisible = false;
  adToDelete: string | null = null;
  adTitleToDelete: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private advertService: AdvertService
  ) {}

  ngOnInit() {
    this.ads = this.route.snapshot.data['ads'].data;
    console.log(this.ads);
  }

  openConfirmDialog(adId: string, adTitle: string) {
    this.confirmDialogVisible = true;
    this.adToDelete = adId;
    this.adTitleToDelete = adTitle;
  }

  confirmDelete() {
    if (this.adToDelete) {
      this.advertService.deleteAdvert(this.adToDelete).subscribe((response) => {
        if (response.success) {
          this.ads = this.ads.filter((ad) => ad.id !== this.adToDelete);
        } else {
          console.error('Error deleting ad:', response.message);
        }
        this.closeConfirmDialog();
      });
    }
  }

  cancelDelete() {
    this.closeConfirmDialog();
  }

  closeConfirmDialog() {
    this.confirmDialogVisible = false;
    this.adToDelete = null;
    this.adTitleToDelete = null;
  }

  getFormattedDate(dateString: string): string {
    return dateString.split('. ')[0];
  }
}
