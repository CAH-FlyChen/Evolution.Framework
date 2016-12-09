import { Directive, ElementRef, Input, Renderer } from '@angular/core';

@Directive({ selector: '[myhighlight]' })
export class HighlightDirective {
    @Input('myhighlight') highlightColor: string;
    el: ElementRef;
    renderer: Renderer;
    constructor(el: ElementRef, renderer: Renderer) {
        this.el = el;
        this.renderer = renderer;
    }
    private highlight(color: string) {
        this.renderer.setElementStyle(this.el.nativeElement, 'backgroundColor', color);
    }
}