import { Component, Input } from '@angular/core';
import { IUser } from '../services/home.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-expert-carousel',
  templateUrl: './expert-carousel.component.html',
  styleUrl: './expert-carousel.component.css',
})
export class ExpertCarouselComponent {
  @Input() users!: IUser[];


  products = [
    {
      title: 'Emma Smith',
      text: 'Award-winning journalist with a knack for investigative reporting. Former anchor at NBC News. Known for her in-depth coverage of international affairs and human interest stories.',
      image:
        'https://www.shutterstock.com/image-photo/smiling-young-middle-eastern-man-600nw-2063524544.jpg',
    },
    {
      title: 'Michael Johnson',
      text: 'Veteran sports commentator with over 30 years of experience. Hosted major events like the Olympics and FIFA World Cup. Renowned for his insightful analysis and charismatic on-screen presence.',
      image:
        'https://www.thefashionisto.com/wp-content/uploads/2019/07/Male-Model-Businessman-Suit.jpg',
    },
    {
      title: 'Sarah Parker',
      text: 'Leading expert in environmental conservation and sustainability. Founder of GreenEarth Initiative, a non-profit organization dedicated to protecting the planet. Featured in TIME magazine for her groundbreaking work.',
      image:
        'https://as1.ftcdn.net/v2/jpg/02/94/62/14/1000_F_294621430_9dwIpCeY1LqefWCcU23pP9i11BgzOS0N.jpg',
    },
    {
      title: 'David Chang',
      text: 'Celebrity chef and restaurateur, known for his innovative approach to culinary arts. Host of Netflix’s hit series “Uprooted”. Recipient of multiple James Beard Awards for his contributions to the food industry.',
      image:
        'https://i.pinimg.com/736x/01/e4/ff/01e4ff3e2af2b426e8e7677c66be307b.jpg',
    },
    {
      title: 'Chris Cuomo',
      text: 'Previously host of CNN’s Cuomo Prime Time, #1 show on the biggest media platform in the world. 20 years in media and politics. Multiple Emmy nominations.',
      image:
        'https://www.thesun.co.uk/wp-content/uploads/2023/11/crop-24791624.jpg?strip=all&quality=100&w=1080&h=1080&crop=1',
    },
    {
      title: 'Rachel Adams',
      text: 'Tech entrepreneur and advocate for women in STEM fields. Co-founder of TechGirls Foundation, empowering young girls to pursue careers in technology. Featured on Forbes 30 Under 30 list for her groundbreaking work.',
      image:
        'https://socialstarage.com/wp-content/uploads/2024/04/Justin-Waller.jpg',
    },
    {
      title: 'Mark Thompson',
      text: 'Finance guru and best-selling author of “Mastering Money Management”. Host of popular podcast “Financial Insights”, providing expert advice on personal finance and investment strategies.',
      image:
        'https://media.istockphoto.com/id/1413766112/photo/successful-mature-businessman-looking-at-camera-with-confidence.jpg?s=612x612&w=0&k=20&c=NJSugBzNuZqb7DJ8ZgLfYKb3qPr2EJMvKZ21Sj5Sfq4=',
    },
  ];

  responsiveOptions = [
    {
      breakpoint: '1024px',
      numVisible: 3,
      numScroll: 1,
    },
    {
      breakpoint: '768px',
      numVisible: 2,
      numScroll: 1,
    },
    {
      breakpoint: '560px',
      numVisible: 1,
      numScroll: 1,
    },
    {
      breakpoint: '0px',
      numVisible: 1,
      numScroll: 1,
    },
  ];

  ngOnInit() {
    this.products = this.users.map(user => ({
      title: user.first_name + ' ' + user.last_name, // assuming 'name' property exists in IUser
      text: user.profession as string, // assuming 'description' property exists in IUser
      image: environment.API_URL + '/uploads/' + user.avatar, // assuming 'imageUrl' property exists in IUser
    }));
  }
}
