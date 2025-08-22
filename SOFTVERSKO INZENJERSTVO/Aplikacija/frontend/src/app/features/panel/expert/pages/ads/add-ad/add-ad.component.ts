import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ITag } from '../../../interfaces/tag.interface';
import { MessageService } from 'primeng/api';
import { environment } from '../../../../../../../environments/environment';
import { functions } from '../../../../../../shared/utils/functions';
import { AdvertService } from '../../../services/advert.service';
import { ICreateExpertAdvert } from '../../../interfaces/ad.interface';

@Component({
  selector: 'app-add-ad',
  templateUrl: './add-ad.component.html',
  styleUrls: ['./add-ad.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class AddAdComponent implements OnInit {
  videoFile: File | null = null;
  imageOneFile: File | null = null;
  imageTwoFile: File | null = null;

  categories: ITag[] = [];
  suggestedCategories: ITag[] = [];
  selectedCategories: ITag[] = [];

  checked: boolean = true;

  constructor(
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _messageService: MessageService,
    private readonly _advertService: AdvertService,
    private readonly _router: Router
  ) {}

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
        file.name.split(file.name[file.name.lastIndexOf('.')])[1]
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
        file.name.split(file.name[file.name.lastIndexOf('.')])[1]
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
        file.name.split(file.name[file.name.lastIndexOf('.')])[1]
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

  onAddAd() {
    if (!this.imageOneFile && !this.imageTwoFile && !this.videoFile) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Morate dodati bar jednu sliku ili video.',
      });
      return;
    }

    const title = document.getElementById('title') as HTMLInputElement;
    const description = document.getElementById(
      'description'
    ) as HTMLInputElement;

    if (!title.value) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Naslov ne sme biti prazan.',
      });
      return;
    }

    if (title.value.length > 100) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Naslov ne sme biti duzi od 100 karaktera.',
      });
      return;
    }

    if (!description.value) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Opis ne sme biti prazan.',
      });
      return;
    }

    if (description.value.length > 2500) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Opis ne sme biti duzi od 2500 karaktera.',
      });
      return;
    }

    if (this.selectedCategories.length < 1) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Morate izabrati barem jednu kategoriju.',
      });
      return;
    }

    let json: any = { selected: [] };

    for (let tab of this.tabs) {
      switch (tab.title) {
        case 'Osnovni': {
          if (!tab.description) {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail: 'Opis za osnovni plan ne sme biti prazan.',
            });
            return;
          }

          if (!tab.time) {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail:
                'Vreme trajanja konsultacija za osnovni plan ne sme biti prazno.',
            });
            return;
          }

          if (!tab.repeat) {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail: 'Broj konsultacija za osnovni plan ne sme biti prazno.',
            });
            return;
          }

          json['basic'] = {
            description: tab.description,
            consultation_number: tab.repeat,
            consultation_time_minutes: tab.time,
          };
          break;
        }
        case 'Srednji': {
          if (!tab.description) {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail: 'Opis za srednji plan ne sme biti prazan.',
            });
            return;
          }

          if (!tab.time) {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail:
                'Vreme trajanja konsultacija za srednji plan ne sme biti prazno.',
            });
            return;
          }

          if (!tab.repeat) {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail: 'Broj konsultacija za srednji plan ne sme biti prazno.',
            });
            return;
          }

          json['standard'] = {
            description: tab.description,
            consultation_number: tab.repeat,
            consultation_time_minutes: tab.time,
          };
          break;
        }
        case 'Napredni': {
          if (!tab.description) {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail: 'Opis za napredni plan ne sme biti prazan.',
            });
            return;
          }

          if (!tab.time) {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail:
                'Vreme trajanja konsultacija za napredni plan ne sme biti prazno.',
            });
            return;
          }

          if (!tab.repeat) {
            this._messageService.add({
              severity: 'error',
              summary: 'Greska',
              detail: 'Broj konsultacija za napredni plan ne sme biti prazno.',
            });
            return;
          }

          json['premium'] = {
            description: tab.description,
            consultation_number: tab.repeat,
            consultation_time_minutes: tab.time,
          };
          break;
        }
      }
    }

    if (this.tabs[0].items.length < 1) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Morate imati barem jednu stavku za neki plan.',
      });
      return;
    }

    this.tabs[0].items.forEach((item) => {
      json['selected'].push({
        name: item.name,
        basic: item.addedInPlan === 'Osnovni',
        standard:
          item.addedInPlan === 'Srednji' || item.addedInPlan === 'Osnovni',
        premium:
          item.addedInPlan === 'Osnovni' ||
          item.addedInPlan === 'Srednji' ||
          item.addedInPlan === 'Napredni',
      });
    });

    const data: ICreateExpertAdvert = {
      title: title.value,
      description: description.value,
      image_one: this.imageOneFile,
      image_two: this.imageTwoFile,
      video: this.videoFile,
      tags: JSON.stringify(this.selectedCategories.map((item) => item.id)),
      plans: JSON.stringify(json),
    };

    this._advertService.createExpertAdvert(data).subscribe({
      next: (response) => {
        if (response.success) {
          this._messageService.add({
            severity: 'success',
            summary: 'Uspesno',
            detail: response.message,
          });

          this._router.navigate(['/panel/expert/ads']);
        } else {
          this._messageService.add({
            severity: 'error',
            summary: 'Greska',
            detail: response.message,
          });
        }
      },
    });
  }

  ngOnInit(): void {
    const data = this._activatedRoute.snapshot.data['data'] as ITag[];
    this.categories = data;
  }
}
