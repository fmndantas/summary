import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {generateUuid, IContainer} from "./container.model";

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.css']
})
export class ContainerComponent implements OnInit {
  @Input() delegate!: IContainer;
  @Input() title!: string;
  @Output() notifyChange: EventEmitter<void> = new EventEmitter<void>();

  headerUuid!: string;
  bodyUuid!: string;
  containerUuid!: string;

  constructor() {
    this.headerUuid = generateUuid();
    this.bodyUuid = generateUuid();
    this.containerUuid = generateUuid();
  }

  ngOnInit(): void {
  }

  get isNotRoot(): boolean {
    return !this.delegate.isRoot;
  }

  get isCompleted(): boolean {
    return this.delegate.isCompleted;
  }

  get TitleStyle(): any {
    let textDecoration = !this.delegate.isCompleted
      ? "none"
      : "line-through";
    let color = this.delegate.isCompleted
      ? "gray"
      : "black";
    return {"text-decoration": textDecoration, "color": color};
  }

  get buttonClassOrNot(): string {
    return this.delegate.isLeaf ? "" : "accordion-button";
  }

  get accordionItemClassOrNot(): string {
    return this.delegate.isLeaf ? "" : "accordion-item";
  }

  addHash(what: string): string {
    return `#${what}`;
  }

  toggle(): void {
    this.delegate.mark();
    this._notifyChange();
  }

  addNested(): void {
    this.delegate.addNested(this.title);
    this._notifyChange();
  }

  addEqualAfter(): void {
    this.delegate.addEqualAfter(this.title);
    this._notifyChange();
  }

  addEqualBefore(): void {
    this.delegate.addEqualBefore(this.title);
    this._notifyChange();
  }

  edit(): void {
    this.delegate.Title = this.title;
    this._notifyChange();
  }

  removeThisFromFather(): void {
    this.delegate.removeItselfFromFather();
    this._notifyChange();
  }

  liftCopyToRootAndCut() {
    this.delegate.liftItselfToTheRoot();
    this.removeThisFromFather();
  }

  receiveCopy() {
    this.delegate.receiveCopy();
  }

  _notifyChange(): void {
    this.notifyChange.emit();
  }
}
