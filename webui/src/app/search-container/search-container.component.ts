import {Component, Input, OnInit} from '@angular/core';
import {ISearchContainer} from "./search-container.model";
import {generateUuid} from "../container/container.model";

@Component({
  selector: 'app-search-container',
  templateUrl: './search-container.component.html',
  styleUrls: ['./search-container.component.css']
})
export class SearchContainerComponent implements OnInit {
  @Input() delegate!: ISearchContainer;
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

  accordionItemClassOrNot(): string {
    return this.delegate.isLeaf() ? "" : "accordion-item"
  }

  buttonClassOrNot(): string {
    return this.delegate.isLeaf() ? "" : "accordion-button"
  }
}
