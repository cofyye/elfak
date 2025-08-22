import { Component, ViewEncapsulation } from '@angular/core';
import { CardModule } from 'primeng/card';
import { DividerModule } from 'primeng/divider';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ButtonModule } from 'primeng/button';
import { ActivatedRoute, Router } from '@angular/router';
import { GradeService } from '../../services/grade.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-grade-setting',
  templateUrl: './grade-setting.component.html',
  styleUrls: ['./grade-setting.component.css'],
  standalone: true,
  imports: [
    CardModule,
    DividerModule,
    RatingModule,
    FormsModule,
    InputTextareaModule,
    ButtonModule,
  ],
  encapsulation: ViewEncapsulation.None,
})
export class GradeSettingComponent {
  value!: number;
  comment!: string;
  adId!: string;

  constructor(
    private route: ActivatedRoute,
    private gradeService: GradeService,
    private messageService: MessageService,
    private router: Router
  ) {}

  ngOnInit() {
    this.adId = this.route.snapshot.paramMap.get('adId')!;
  }

  submitReview() {
    if (this.value && this.comment) {
      this.gradeService
        .createGrade(this.adId, this.value, this.comment)
        .subscribe({
          next: (response) => {
            if (response.success) {
              this.messageService.add({
                severity: 'success',
                summary: 'Uspešno',
                detail: 'Vaša recenzija je uspešno poslata.',
              });
              this.router.navigate([`/adverts/${this.adId}`]);
            } else {
              this.messageService.add({
                severity: 'error',
                summary: 'Greška',
                detail: response.message,
              });
            }
          },
          error: (error) => {
            console.error('Error creating grade:', error);
            this.messageService.add({
              severity: 'error',
              summary: 'Greška',
              detail: 'Došlo je do greške prilikom slanja recenzije.',
            });
          },
        });
    } else {
      this.messageService.add({
        severity: 'warn',
        summary: 'Upozorenje',
        detail: 'Molimo vas da unesete ocenu i komentar.',
      });
    }
  }

  navigateToHome() {
    this.router.navigate(['/']);
  }
}
