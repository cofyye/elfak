// element-width.directive.ts
import {
  Directive,
  ElementRef,
  EventEmitter,
  Output,
  AfterViewInit,
  OnDestroy,
} from '@angular/core';

@Directive({
  selector: '[appElementWidth]',
})
export class ElementWidthDirectiveExpert implements AfterViewInit, OnDestroy {
  @Output() widthChange = new EventEmitter<number>();
  private resizeObserver!: ResizeObserver;

  constructor(private el: ElementRef) {}

  ngAfterViewInit() {
    this.resizeObserver = new ResizeObserver((entries) => {
      for (let entry of entries) {
        if (entry.contentRect) {
          this.emitWidth(entry.contentRect.width);
        }
      }
    });
    this.resizeObserver.observe(this.el.nativeElement);
    setTimeout(() => this.emitWidth(this.el.nativeElement.offsetWidth));
  }

  ngOnDestroy() {
    if (this.resizeObserver) {
      this.resizeObserver.disconnect();
    }
  }

  private emitWidth(width: number) {
    this.widthChange.emit(width);
  }
}
