import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-support-confirmation',
  templateUrl: './support-confirmation.component.html',
  styleUrl: './support-confirmation.component.css',
})
export class SupportConfirmationComponent {
  constructor(public readonly _router: Router) {}
}
