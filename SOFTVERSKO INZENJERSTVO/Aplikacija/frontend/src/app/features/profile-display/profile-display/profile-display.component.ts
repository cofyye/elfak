import {
  Component,
  ViewEncapsulation,
  ElementRef,
  OnInit,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IUserProfile } from '../interface/profile-display.interface';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-profile-display',
  templateUrl: './profile-display.component.html',
  styleUrls: ['./profile-display.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class ProfileDisplayComponent implements OnInit {
  itemsToShow: number = 2;
  environment = environment;
  userProfile: IUserProfile | null = null;
  bioShort: string = '';
  isLongBio: boolean = false;
  bioFull: string = '';
  showFullBio: boolean = false;

  comments: any[] = [
    {
      id: 1,
      profilePic:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/asiyajavayant.png',
      profileName: 'Filip Lakicevic',
      job: 'Boss',
      text: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.Id atque ducimus voluptatibus tempore? Commodi, recusandae minus,repellendu solestiasi nventore.',
      time: 'pre 7 sati',
      sellerRating: 3,
    },
    {
      id: 2,
      profilePic:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/amyelsner.png',
      profileName: 'Aleksa Ilic',
      job: 'Project manager',
      text: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.Id atque ducimus voluptatibus tempore? Commodi, recusandae minus,repell endusoles tiasinve ntore.Lorem ipsum dolor sit amet, consectetur adipisicing elit. IdLorem ipsum dolor sit amet, consectetur adipisicing elit. IdLorem ipsum dolor sit amet, consectetur adipisicing elit. Id',
      time: 'pre 21 sati',
      sellerRating: 4,
    },
    {
      id: 3,
      profilePic:
        'https://primefaces.org/cdn/primeng/images/demo/avatar/asiyajavayant.png',
      profileName: 'Andjela Mia Doncov',
      job: 'Asistent',
      text: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.Id atque ducimus voluptatibus tempore? Commodi, recusandae minus,repell endusoles tiasin ventore.Id atque ducimus voluptatibus tempore? Commodi, recusandae minus,repellendusolestiasinventore',
      time: 'pre 21 sati',
      sellerRating: 1,
    },
    {
      id: 4,
      profilePic: 'https://primefaces.org/cdn/primeng/images/card-ng.jpg',
      profileName: ' Ilija Ivanovic',
      job: 'Frontend',
      text: 'Lorem ipsum dolor sit amet.',
      time: 'pre 7 sati',
      sellerRating: 5,
    },
  ];

  constructor(private route: ActivatedRoute, private elementRef: ElementRef) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.userProfile = data['userProfile'].data;
      if (this.userProfile!.bio.length > 900) {
        this.bioFull = this.userProfile!.bio;
        this.bioShort = this.userProfile!.bio.substring(0, 900);
        this.isLongBio = true;
      } else {
        this.bioShort = this.userProfile!.bio;
        this.isLongBio = false;
      }
    });
  }

  showMore() {
    this.itemsToShow = this.comments.length;
  }

  toggleBio() {
    this.showFullBio = !this.showFullBio;
  }

  showLess() {
    this.itemsToShow = 2;
  }

  scrollToElement(elementId: string): void {
    const element = this.elementRef.nativeElement.querySelector(
      `#${elementId}`
    );
    if (element) {
      element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }
}
