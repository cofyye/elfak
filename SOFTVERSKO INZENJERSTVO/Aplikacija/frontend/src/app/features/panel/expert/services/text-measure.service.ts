import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class TextMeasureService {
    private canvas: HTMLCanvasElement;
    private context: CanvasRenderingContext2D;

    constructor() {
        this.canvas = document.createElement('canvas');
        this.context = this.canvas.getContext('2d') as CanvasRenderingContext2D;
    }

    measureText(text: string, font: string): number {
        this.context.font = font;
        const metrics = this.context.measureText(text);
        return metrics.width;
    }
}
