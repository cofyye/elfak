import {
  Component,
  OnInit,
  ViewEncapsulation,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { environment } from '../../../../../../../environments/environment';
import { functions } from '../../../../../../shared/utils/functions';
import { AdvertService } from '../../../services/advert.service';
import { IExpertAdvert } from '../../../interfaces/ad-list.interface';
import { ITag } from '../../../interfaces/tag.interface';

@Component({
  selector: 'app-ad-edit',
  templateUrl: './ad-edit.component.html',
  styleUrls: ['./ad-edit.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class AdEditComponent implements OnInit {
  @ViewChild('imageOneTag') imageOneTag!: ElementRef<HTMLImageElement>;
  @ViewChild('imageTwoTag') imageTwoTag!: ElementRef<HTMLImageElement>;
  @ViewChild('videoTag') videoTag!: ElementRef<HTMLVideoElement>;

  videoFile: File | null = null;
  imageOneFile: File | null = null;
  imageTwoFile: File | null = null;

  categories: ITag[] = [];
  suggestedCategories: ITag[] = [];
  selectedCategories: ITag[] = [];

  checked: boolean = true;
  adId: string | null = null;
  adData: IExpertAdvert | null = null;

  title: string = '';
  description: string = '';

  imageOneUrl: string = '';
  imageTwoUrl: string = '';
  videoUrl: string = '';

  predefinedTags: string[] = ['Osnovni', 'Srednji', 'Napredni'];
  tags: string[] = ['Osnovni'];
  tabs: {
    title: string;
    description: string;
    items: { name: string; addedInPlan: string }[];
    time: string;
    repeat: string;
  }[] = [
      { title: 'Osnovni', description: '', items: [], time: '', repeat: '' },
    ];
  newItem: string = '';
  activeIndex: number = 0;

  constructor(
    private readonly _messageService: MessageService,
    private readonly _advertService: AdvertService,
    private readonly _route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.adId = this._route.snapshot.paramMap.get('add_id');

    if (this.adId) {
      this._advertService.getAdvertById(this.adId).subscribe({
        next: (response) => {
          if (response.success && response.data) {
            this.adData = response.data;
            this.populateFormFields();
          } else {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail: 'Neuspesno pribavljanje podataka oglasa.',
            });
          }
        },
        error: (error) => {
          console.error('Error fetching ad data:', error);
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: 'Greska prilikom pribavljanja podataka oglasa.',
          });
        },
      });
    }
  }

  async populateFormFields() {
    if (this.adData) {
      this.title = this.adData.title;
      this.description = this.adData.description;
      this.checked = this.adData.active;

      if (this.adData.image_one) {
        this.imageOneUrl = `${environment.API_URL}/uploads/${this.adData.image_one}`;
      }
      if (this.adData.image_two) {
        this.imageTwoUrl = `${environment.API_URL}/uploads/${this.adData.image_two}`;
      }
      if (this.adData.video) {
        this.videoUrl = `${environment.API_URL}/uploads/${this.adData.video}`;
      }
      this.selectedCategories = this.adData.tags;
      this.populatePlans(this.adData.plans);
    }
  }

  populatePlans(plansData: any) {
    const selectedItems = plansData.selected;
    const plans: Array<'basic' | 'standard' | 'premium'> = [
      'basic',
      'standard',
      'premium',
    ];
    const planLabels: { [key in (typeof plans)[number]]: string } = {
      basic: 'Osnovni',
      standard: 'Srednji',
      premium: 'Napredni',
    };

    this.tabs = plans
      .filter((plan) => plansData[plan])
      .map((plan) => ({
        title: planLabels[plan],
        description: plansData[plan]?.description || '',
        items: selectedItems
          .filter((item: any) => item[plan] === true)
          .map((item: any) => ({
            name: item.name,
            addedInPlan: planLabels[plan],
          })),
        time: `${plansData[plan]?.consultation_time_minutes || ''}`,
        repeat: `${plansData[plan]?.consultation_number || ''}`,
      }));
    this.tags = this.tabs.map((tab) => tab.title);
  }

  handleImageOne(imageInput: HTMLInputElement) {
    imageInput.click();
  }

  handleImageTwo(imageInput: HTMLInputElement) {
    imageInput.click();
  }

  handleVideo(videoInput: HTMLInputElement) {
    videoInput.click();
  }

  onVideoChange(videoInput: HTMLInputElement, videoTag: HTMLVideoElement) {
    const file = videoInput.files?.[0] as File;

    if (!file) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Greska prilikom ocitavanja videa.',
      });
      videoTag.src = '';
      videoTag.classList.remove('show');
      this.videoFile = null;
      return;
    }

    if (
      !environment.VIDEO_EXTENSIONS.includes(
        file.name.split('.').pop() as string
      )
    ) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Ekstenzija ovog fajla nije podrzana.',
      });
      return;
    }

    if (file.size > environment.VIDEO_MAX_UPLOAD_SIZE) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: `Velicina Vaseg fajla je ${functions.formatBytes(
          file.size
        )}, maksimalna velicina je ${functions.formatBytes(
          environment.VIDEO_MAX_UPLOAD_SIZE
        )}.`,
      });
      return;
    }

    this.videoFile = file;

    videoTag.src = URL.createObjectURL(file);
    videoTag.classList.add('show');
  }

  onImageOneChange(imageInput: HTMLInputElement, imageTag: HTMLImageElement) {
    const file = imageInput.files?.[0] as File;

    if (!file) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Greska prilikom ocitavanja prve slike.',
      });
      imageTag.src = '';
      imageTag.classList.remove('show');
      this.imageOneFile = null;
      return;
    }

    if (
      !environment.IMAGE_EXTENSIONS.includes(
        file.name.split('.').pop() as string
      )
    ) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Ekstenzija ovog fajla nije podrzana.',
      });
      return;
    }

    if (file.size > environment.PICTURE_MAX_UPLOAD_SIZE) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: `Velicina Vaseg fajla je ${functions.formatBytes(
          file.size
        )}, maksimalna velicina je ${functions.formatBytes(
          environment.PICTURE_MAX_UPLOAD_SIZE
        )}.`,
      });
      return;
    }

    this.imageOneFile = file;

    imageTag.src = URL.createObjectURL(file);
    imageTag.classList.add('show');
  }

  onImageTwoChange(imageInput: HTMLInputElement, imageTag: HTMLImageElement) {
    const file = imageInput.files?.[0] as File;

    if (!file) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Greska prilikom ocitavanja druge slike.',
      });
      imageTag.src = '';
      imageTag.classList.remove('show');
      this.imageTwoFile = null;
      return;
    }

    if (
      !environment.IMAGE_EXTENSIONS.includes(
        file.name.split('.').pop() as string
      )
    ) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Ekstenzija ovog fajla nije podrzana.',
      });
      return;
    }

    if (file.size > environment.PICTURE_MAX_UPLOAD_SIZE) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: `Velicina Vaseg fajla je ${functions.formatBytes(
          file.size
        )}, maksimalna velicina je ${functions.formatBytes(
          environment.PICTURE_MAX_UPLOAD_SIZE
        )}.`,
      });
      return;
    }

    this.imageTwoFile = file;

    imageTag.src = URL.createObjectURL(file);
    imageTag.classList.add('show');
  }

  toggleChecked(newValue: boolean) {
    this.checked = newValue;
  }

  search(event: any) {
    const query = event.query;
    this.suggestedCategories = this.categories.filter((category) =>
      category.name.toLowerCase().includes(query.toLowerCase())
    );
  }

  addTag() {
    if (this.tags.length < this.predefinedTags.length) {
      const newTag = this.predefinedTags[this.tags.length];
      this.tags.push(newTag);
      this.tabs.push({
        title: newTag,
        description: '',
        items: this.tabs[0].items,
        time: '',
        repeat: '',
      });
      this.activeIndex = this.tabs.length - 1;
    }
  }

  removeTag(index: number) {
    if (this.tags.length > 1 && index > 0) {
      this.tags.splice(index, 1);
      this.tabs.splice(index, 1);
      if (this.activeIndex >= index) {
        this.activeIndex = this.activeIndex - 1;
      }
    }
  }

  selectTab(index: number) {
    this.activeIndex = index;
  }

  addItem(tabIndex: number) {
    if (this.newItem.trim()) {
      if (
        !this.tabs[tabIndex].items.find((item) => item.name === this.newItem)
      ) {
        this.tabs[tabIndex].items.push({
          name: this.newItem,
          addedInPlan: this.tabs[tabIndex].title,
        });
      }
      this.newItem = '';
    }
  }

  removeItem(itemIndex: number) {
    this.tabs.forEach((tab) => {
      tab.items.splice(itemIndex, 1);
    });
  }

  editAd() {
    if (!this.adId) {
      return;
    }

    if (!this.title || !this.description) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Morate uneti naslov i opis oglasa.',
      });
      return;
    }

    if (!this.imageOneFile) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Morate uneti prvu sliku oglasa.',
      });
      return;
    }

    if (!this.imageTwoFile) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Morate uneti drugu sliku oglasa.',
      });
      return;
    }

    if (!this.videoFile) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Morate uneti video oglasa.',
      });
      return;
    }

    const formData = new FormData();
    formData.append('title', this.title);
    formData.append('description', this.description);
    formData.append('active', this.checked.toString());
    formData.append('image_one', this.imageOneFile);
    formData.append('image_two', this.imageTwoFile);
    formData.append('video', this.videoFile);
    formData.append('tags', JSON.stringify(this.selectedCategories));
    formData.append('plans', JSON.stringify(this.tabs.map((tab) => ({
      title: tab.title,
      description: tab.description,
      items: tab.items,
      consultation_time_minutes: tab.time,
      consultation_number: tab.repeat,
    }))));

    this._advertService.editAdvert(this.adId, formData).subscribe({
      next: (response) => {
        if (response.success) {
          this._messageService.add({
            severity: 'success',
            summary: 'Uspesno',
            detail: 'Uspesno ste izmenili oglas.',
          });
        } else {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: 'Greska prilikom izmene oglasa.',
          });
        }
      },
      error: (error) => {
      }
    });
  }
}
