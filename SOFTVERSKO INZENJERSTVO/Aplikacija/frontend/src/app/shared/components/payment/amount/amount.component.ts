import { Component, ViewEncapsulation } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { PaymentService } from '../../../services/payment.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-amount',
  templateUrl: './amount.component.html',
  styleUrls: ['./amount.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class AmountComponent {
  options = [
    { id: 'option1', value: 10, prize: (1 / 10) * 10 },
    { id: 'option2', value: 50, prize: (1 / 10) * 50 },
    { id: 'option3', value: 100, prize: (1 / 10) * 100 },
    { id: 'option4', value: 200, prize: (1 / 10) * 200 },
    { id: 'option5', value: 500, prize: (1 / 10) * 500 },
    { id: 'option6', value: 800, prize: (1 / 10) * 800 },
  ];

  selectedOption: string | null = null;
  selectedPaymentMethod: string | null = null;
  message: SafeHtml = '';
  confirmDialogVisible = false;

  selectedOptionValue: number | null = null;
  selectedOptionPrice: number | null = null;

  constructor(
    private router: Router,
    private sanitizer: DomSanitizer,
    private readonly _paymentService: PaymentService,
    private readonly _messageService: MessageService
  ) {}

  selectOption(option: any): void {
    this.selectedOption = option.id;
    this.selectedOptionValue = option.value;
    this.selectedOptionPrice = option.prize;
    const iconHtml = '<i class="fas fa-coins"></i>';
    const euro = '&euro;';
    this.message = this.sanitizer.bypassSecurityTrustHtml(
      `${option.value}${iconHtml} = ${option.prize} ${euro}`
    );
  }

  changeCursor(cursorStyle: string): void {
    document.body.style.cursor = cursorStyle;
  }

  selectPaymentMethod(method: string): void {
    this.selectedPaymentMethod = method;
  }

  openConfirmDialog(): void {
    if (this.selectedOption && this.selectedPaymentMethod) {
      this.confirmDialogVisible = true;
    } else {
      this._messageService.add({
        severity: 'error',
        summary: 'Greška',
        detail: 'Molimo Vas da izaberete sumu novca i način plaćanja.',
      });
    }
  }

  closeConfirmDialog(): void {
    this.confirmDialogVisible = false;
  }

  confirmPurchase(): void {
    this.confirmDialogVisible = false;
    this.makePayment();
  }

  makePayment(): void {
    const optionIndex = this.options.findIndex(
      (opt) => opt.id === this.selectedOption
    );

    if (optionIndex == -1) {
      this._messageService.add({
        severity: 'error',
        summary: 'Greška',
        detail: 'Molimo Vas da izaberete sumu novca koju želite da uplatite.',
      });
      return;
    }

    switch (this.selectedPaymentMethod) {
      case 'PayPal': {
        this._paymentService
          .createPayPalPayment(
            this.options[optionIndex].value.toString(),
            this.router.url + '/paypal'
          )
          .subscribe({
            next: (res) => {
              if (res.success) {
                window.location.href = res.data;
              } else {
                this._messageService.add({
                  severity: 'error',
                  summary: 'Greška',
                  detail: res.message,
                });
              }
            },
            error: () => {
              this._messageService.add({
                severity: 'error',
                summary: 'Greška',
                detail: 'Dogodila se greška prilikom kreiranja plaćanja.',
              });
            },
          });

        break;
      }
      case 'Stripe': {
        this._paymentService
          .createStripePayment(
            this.options[optionIndex].value.toString(),
            this.router.url + '/stripe'
          )
          .subscribe({
            next: (res) => {
              if (res.success) {
                window.location.href = res.data;
              } else {
                this._messageService.add({
                  severity: 'error',
                  summary: 'Greška',
                  detail: res.message,
                });
              }
            },
            error: () => {
              this._messageService.add({
                severity: 'error',
                summary: 'Greška',
                detail: 'Dogodila se greška prilikom kreiranja plaćanja.',
              });
            },
          });

        break;
      }
      default: {
        this._messageService.add({
          severity: 'error',
          summary: 'Greška',
          detail: 'Molimo Vas da izaberete način plaćanja.',
        });
      }
    }
  }
}
