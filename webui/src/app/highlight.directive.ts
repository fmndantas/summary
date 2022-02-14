import {Directive, ElementRef, HostListener, Input} from '@angular/core';

@Directive({
  selector: '[appHighlight]'
})
export class HighlightDirective {

  @Input() appHighlight!: string;

  constructor(private el: ElementRef) {
  }

  @HostListener("mouseenter")
  private OnMouseEnter(): void {
    this.el.nativeElement.style.backgroundColor = this.appHighlight;
  }

  @HostListener("mouseleave")
  private OnMouseLeave(): void {
    this.el.nativeElement.style.backgroundColor = 'white';
  }
}
