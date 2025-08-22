import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProfileGet } from '../../interfaces/profile.interface';
import { IDataAcceptResponse } from '../../../../../shared/interfaces/response.interface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  closeCallback($event: any) { }

  name: string = '';

  constructor(private _activatedRoute: ActivatedRoute) { }
  ngOnInit(): void {
    const responseData = (
      this._activatedRoute.snapshot.data[
      'profile'
      ] as IDataAcceptResponse<IProfileGet>
    ).data;
    this.name = responseData.firstName;
    const ball = document.querySelector('.ball') as HTMLElement;
    const welcomeMessage = document.querySelector(
      '.welcome-message'
    ) as HTMLElement;

    ball.addEventListener('animationend', () => {
      ball.classList.add('hidden');
      welcomeMessage.classList.add('visible');
    });
  }

}
