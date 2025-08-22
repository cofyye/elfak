import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-expert-card',
  templateUrl: './expert-card.component.html',
  styleUrls: ['./expert-card.component.css'],
})
export class ExpertCardComponent {
  @Input() cardTitle: string = '';
  @Input() cardText: string = '';
  @Input() cardImage: string = '';
}
