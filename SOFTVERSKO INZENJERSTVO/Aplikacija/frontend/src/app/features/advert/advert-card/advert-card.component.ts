import { Component, Input, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-advert-card',
  templateUrl: './advert-card.component.html',
  styleUrls: ['./advert-card.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class AdvertCardComponent {
  @Input() image_1: string = '';
  @Input() image_2: string = '';
  @Input() video: string = '';
  @Input() sellerImage: string = '';
  @Input() sellerName: string = '';
  @Input() rating: number = 0;
  @Input() header: string = '';
  @Input() description: string = '';
  @Input() advertId: string = '';
  muteIconVisible = false;

  maxDescriptionLength = 80;
  maxTitleLength = 60;
  limitText(text: string, maxLength: number): string {
    if (text.length > maxLength) {
      return text.slice(0, maxLength) + '...';
    }
    return text;
  }

  parseRating(rating: string): number {
    return parseFloat(rating);
  }

  isValidImage(url: string | undefined): boolean {
    return url !== undefined && url !== '' && !url.endsWith('null');
  }

  isValidVideo(url: string | undefined): boolean {
    return url !== undefined && url !== '' && !url.endsWith('null');
  }

  playVideo(event: Event): void {
    const videoElement = (event.target as HTMLElement).querySelector(
      '.add-video'
    ) as HTMLVideoElement;
    if (videoElement.paused) {
      videoElement.play();
      const playIcon = (event.target as HTMLElement).querySelector(
        '.fa-play'
      ) as HTMLVideoElement;
      playIcon.classList.replace('fa-play', 'fa-pause');
    }
    this.muteIconVisible = true;
  }

  pauseVideo(event: Event): void {
    const videoElement = (event.target as HTMLElement).querySelector(
      '.add-video'
    ) as HTMLVideoElement;
    if (!videoElement.paused) {
      videoElement.pause();
      const playIcon = (event.target as HTMLElement).querySelector(
        '.fa-pause'
      ) as HTMLVideoElement;
      if (playIcon) {
        playIcon.classList.replace('fa-pause', 'fa-play');
      }
    }
    this.muteIconVisible = false;
  }

  togglePlay(event: Event): void {
    event.stopPropagation();
    const videoElement = (event.target as HTMLElement)
      .closest('.video-container')
      ?.querySelector('.add-video') as HTMLVideoElement;
    if (videoElement.paused) {
      videoElement.play();
      (event.target as HTMLElement).classList.replace('fa-play', 'fa-pause');
    } else {
      videoElement.pause();
      (event.target as HTMLElement).classList.replace('fa-pause', 'fa-play');
    }
  }

  toggleMute(event: Event): void {
    event.stopPropagation();
    const targetElement = event.target as HTMLElement;
    const videoElement = targetElement
      .closest('.video-container')
      ?.querySelector('.add-video') as HTMLVideoElement;

    videoElement.muted = !videoElement.muted;

    if (targetElement.classList.contains('fa-volume-mute')) {
      targetElement.classList.replace('fa-volume-mute', 'fa-volume');
    } else {
      targetElement.classList.replace('fa-volume', 'fa-volume-mute');
    }
  }
}
