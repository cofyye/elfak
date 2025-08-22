import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-client-support-confirmation',
  templateUrl: './client-support-confirmation.component.html',
  styleUrl: './client-support-confirmation.component.css',
})
export class ClientSupportConfirmationComponent {
  constructor(public readonly _router: Router) {}
}
