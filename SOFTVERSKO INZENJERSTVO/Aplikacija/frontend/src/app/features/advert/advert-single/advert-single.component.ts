import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IDataAcceptResponse } from '../../../shared/interfaces/response.interface';
import { environment } from '../../../../environments/environment';
import { IAdvertInfo } from '../interfaces/advert-info.interface';
import { IAdvertComment } from '../interfaces/comment-interface';
import { IPaginationData } from '../../../shared/interfaces/pagination.interface';
import moment from 'moment-timezone';
import { MessageService } from 'primeng/api';
import { AdvertService } from '../services/advert.service';
import { ICreateForm } from '../interfaces/create-form.interface';
import { ChatService } from '../../panel/expert/services/chat.service';
import { ISocketMessage } from '../../panel/expert/interfaces/chat.interface';
import { Store } from '@ngrx/store';
import { AppState } from '../../../app.state';
import { getUser } from '../../auth/store/auth.selectors';
import { take } from 'rxjs';

@Component({
  selector: 'app-advert-single',
  templateUrl: './advert-single.component.html',
  styleUrls: ['./advert-single.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class AdvertSingleComponent implements OnInit {
  commentsToShow: number = 2;
  documentItemsToShow: { [key: number]: number } = {};

  private currentUserId: string = '';
  public canRate: boolean = false;

  minDate = moment().tz('Europe/Belgrade').add(1, 'day').toDate();

  advert: IAdvertInfo = {
    id: '',
    averageGrade: 0,
    expertId: '',
    canGrade: false,
    title: '',
    description: '',
    active: false,
    createdAt: '',
    image_one: '',
    image_two: '',
    video: '',
    expert: {
      firstName: '',
      lastName: '',
      username: '',
      bio: null,
      profession: null,
      avatar: '',
      registrationDate: '',
    },
    plans: '',
  };

  comments: IAdvertComment[] = [];

  Plans: any[] = [];
  selectedPlanName: string = 'Trenutni plan';
  padding: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private readonly _messageService: MessageService,
    private readonly _advertService: AdvertService,
    private readonly _chatService: ChatService,
    private readonly _store: Store<AppState>,
    private router: Router
  ) {
    this.Plans.forEach((_, index) => {
      this.documentItemsToShow[index] = 5;
    });
  }

  mediaArray: { type: string; src: string }[] = [];

  ngOnInit() {
    const advertData = (
      this.activatedRoute.snapshot.data[
        'data'
      ] as IDataAcceptResponse<IAdvertInfo>
    ).data;
    const commentData = (
      this.activatedRoute.snapshot.data['comments'] as IDataAcceptResponse<
        IPaginationData<IAdvertComment[]>
      >
    ).data;
    console.log(advertData);
    console.log(commentData);
    this.advert.id = advertData.id;
    this.advert.title = advertData.title;
    this.advert.canGrade = advertData.canGrade;
    this.advert.expertId = advertData.expertId;
    this.advert.description = advertData.description;
    const dateTimeString = advertData.createdAt;
    const dateOnly = dateTimeString.split(' ')[0];
    this.advert.createdAt = dateOnly;
    this.advert.averageGrade = advertData.averageGrade;

    this.advert.expertId = advertData.expertId;
    this.advert.expert.username = advertData.expert.username;
    this.advert.expert.firstName = advertData.expert.firstName;
    this.advert.expert.lastName = advertData.expert.lastName;
    this.advert.expert.profession = advertData.expert.profession ?? '';
    this.advert.expert.username = advertData.expert.username;
    this.advert.expert.avatar = `${environment.API_URL}/uploads/${advertData.expert.avatar}`;
    this.mediaArray = [];
    if (advertData.image_one) {
      this.mediaArray.push({
        type: 'image',
        src: `${environment.API_URL}/uploads/${advertData.image_one}`,
      });
    }
    if (advertData.image_two) {
      this.mediaArray.push({
        type: 'image',
        src: `${environment.API_URL}/uploads/${advertData.image_two}`,
      });
    }
    if (advertData.video) {
      this.mediaArray.push({
        type: 'video',
        src: `${environment.API_URL}/uploads/${advertData.video}`,
      });
    }

    this.comments =
      commentData?.data?.map((comment) => ({
        ...comment,
        createdAt: comment.createdAt.split(' ')[0],
      })) || [];
    this.comments.forEach((comment) => {
      comment.user.avatar = `${environment.API_URL}/uploads/${comment.user.avatar}`;
    });

    // Parse plans
    let plansData: any;
    try {
      plansData =
        typeof advertData.plans === 'string'
          ? JSON.parse(advertData.plans)
          : advertData.plans;
    } catch (e) {
      console.error('Invalid JSON format for plans:', advertData.plans);
      plansData = {}; // or handle error accordingly
    }

    const selectedItems = plansData.selected || [];
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

    this.Plans = plans.map((plan) => ({
      header: planLabels[plan],
      description: plansData[plan]?.description || '',
      duration: `${plansData[plan]?.consultation_time_minutes || ''} min`,
      items: selectedItems.map((item: any) => ({
        name: item.name,
        isSelected: item[plan] || false,
      })),
      numofconsultation: `${
        plansData[plan]?.consultation_number || ''
      } konsultacija`,
    }));
    this.Plans.forEach((_, index) => {
      this.documentItemsToShow[index] = 5;
    });

    this._store
      .select(getUser)
      .pipe(take(1))
      .subscribe({
        next: (user) => {
          this.currentUserId = user.id;
        },
      });
  }

  showMoreComments() {
    this.commentsToShow = this.comments.length;
  }

  showLessComments() {
    this.commentsToShow = 2;
  }

  showMoreDocumentItems(index: number) {
    this.documentItemsToShow[index] = this.Plans[index].items.length;
  }

  showLessDocumentItems(index: number) {
    this.documentItemsToShow[index] = 5;
  }

  isCustomVisible: boolean = false;

  prosiriCustom() {
    this.isCustomVisible = !this.isCustomVisible;
  }

  visible: boolean = false;

  showDialog(planHeader: string) {
    this.selectedPlanName = planHeader;
    this.visible = true;
  }

  inputs: string[] = [];

  addInput(inputField: HTMLInputElement): void {
    if (inputField.value.trim() !== '') {
      this.inputs.push(inputField.value.trim());
      inputField.value = '';
    }
  }

  removeInput(index: number): void {
    this.inputs.splice(index, 1);
  }

  currentIndex = 0;

  prevItem() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
    } else {
      this.currentIndex = this.mediaArray.length - 1;
    }
  }

  nextItem() {
    if (this.currentIndex < this.mediaArray.length - 1) {
      this.currentIndex++;
    } else {
      this.currentIndex = 0;
    }
  }

  datetime12h: Date[] | undefined;

  datetime24h: Date[] | undefined;

  time: Date[] | undefined;

  onCreateConsultation() {
    let plans: string = '';
    let choosedPlan: string = '';

    if (!this.datetime24h) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Datum ne sme biti prazan.',
      });
      return;
    }

    const description = document.getElementById(
      'descriptionPlan'
    ) as HTMLInputElement;

    if (!description.value) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Opis ne sme biti prazan.',
      });
      return;
    }

    if (description.value.length > 1500) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Opis ne sme biti duzi od 1500 karaktera.',
      });
      return;
    }

    if (this.isCustomVisible) {
      if (this.inputs.length < 1) {
        this._messageService.add({
          severity: 'error',
          summary: 'Greska',
          detail: 'Morate da unesete barem jednu stavku.',
        });
        return;
      }

      plans = JSON.stringify(this.inputs);
      choosedPlan = 'custom';
    } else {
      const plan = this.Plans.find(
        (item) => item.header === this.selectedPlanName
      );

      if (!plan) {
        this._messageService.add({
          severity: 'error',
          summary: 'Greska',
          detail: 'Greska prilikom uzimanja stavke plana.',
        });
        return;
      }

      plans = JSON.stringify(
        plan.items
          .filter((item: any) => item.isSelected == true)
          .map((item: any) => item.name)
      );

      switch (this.selectedPlanName) {
        case 'Osnovni': {
          choosedPlan = 'basic';
          break;
        }
        case 'Srednji': {
          choosedPlan = 'standard';
          break;
        }
        case 'Napredni': {
          choosedPlan = 'premium';
          break;
        }
      }
    }

    if (plans.length < 3) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greska',
        detail: 'Morate da unesete barem jednu stavku.',
      });
      return;
    }

    const date = moment
      .tz(this.datetime24h?.toString() ?? '', 'Europe/Belgrade')
      .toDate();

    const data: ICreateForm = {
      adId: this.advert.id,
      choosedPlan: choosedPlan,
      dateTime: date,
      plans: plans,
      description: description.value,
    };

    this._advertService.createForm(data).subscribe({
      next: (response) => {
        if (response.success) {
          description.value = '';
          this.datetime24h = undefined;
          this.inputs = [];
          this.isCustomVisible = false;

          const data: ISocketMessage = {
            text: 'Nova konsultacija',
            timestamp: moment.tz(moment(), 'Europe/Belgrade').toISOString(),
            senderId: this.currentUserId,
            receiverId: this.advert.expertId,
            profilePic: '',
          };

          this._chatService.sendNewForm(data);

          this._messageService.add({
            severity: 'success',
            summary: 'Uspesno',
            detail: response.message,
          });
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

  navigateToGrade() {
    this.router.navigate([`/grade/${this.advert.id}`]);
  }
}
