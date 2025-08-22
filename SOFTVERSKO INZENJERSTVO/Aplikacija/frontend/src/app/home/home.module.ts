import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { RouterModule } from '@angular/router';
import { ExpertCardComponent } from './expert-card/expert-card.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { CarouselModule } from 'primeng/carousel';
import { ExpertCarouselComponent } from './expert-carousel/expert-carousel.component';
import { InputTextModule } from 'primeng/inputtext';
import { HeroComponent } from './hero/hero.component';
import { FaqComponent } from './faq/faq.component';
import { AccordionModule } from 'primeng/accordion';
import { FooterComponent } from '../shared/components/footer/footer.component';
import { NavBarComponent } from '../shared/components/navbar/navbar.component';
import { FormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';

@NgModule({
  declarations: [
    HomeComponent,
    ExpertCardComponent,
    ExpertCarouselComponent,
    HeroComponent,
    FaqComponent,
  ],
  exports: [HomeComponent],
  imports: [
    CommonModule,
    RouterModule,
    InputTextModule,
    AccordionModule,
    CardModule,
    CarouselModule,
    ButtonModule,
    AccordionModule,
    FooterComponent,
    NavBarComponent,
    FormsModule,
    ToastModule
  ],
})
export class HomeModule { }
