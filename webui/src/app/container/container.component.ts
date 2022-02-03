import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {generateUuid, IContainer} from "./container.model";

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.css']
})
export class ContainerComponent implements OnInit {
  @Input() content!: IContainer;
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
    return this.content.Father != null;
  }

  get isCompleted(): boolean {
    return this.content.isCompleted;
  }

  get TitleStyle(): any {
    let textDecoration = !this.content.isCompleted
      ? "none"
      : "line-through";
    let color = this.content.isCompleted
      ? "gray"
      : "black";
    return {"text-decoration": textDecoration, "color": color};
  }

  get buttonClassOrNot(): string {
    return this.content.isLeaf ? "" : "accordion-button";
  }

  get accordionItemClassOrNot(): string {
    return this.content.isLeaf ? "" : "accordion-item";
  }

  addHash(what: string): string {
    return `#${what}`;
  }

  toggle(): void {
    this.content.mark();
    this._notifyChange();
  }

  addNested(): void {
    this.content.addNested(this.title);
    this._notifyChange();
  }

  addEqualAfter(): void {
    this.content.addEqualAfter(this.title);
    this._notifyChange();
  }

  addEqualBefore(): void {
    this.content.addEqualBefore(this.title);
    this._notifyChange();
  }

  edit(): void {
    this.content.Title = this.title;
    this._notifyChange();
  }

  removeFromFather(): void {
    this.content.Father?.removeChild(this.content);
    this._notifyChange();
  }

  liftCopyToRootAndCut() {
    this.content.liftCopyToRoot();
    this.removeFromFather();
  }

  receiveCopy() {
    this.content.receiveCopy();
  }

  _notifyChange(): void {
    this.notifyChange.emit();
  }
}
