import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {AppSearchSummary} from "./search-summary.model";
import {IModal} from "../generic-modal/generic-modal.model";
import {Observable, of} from "rxjs";

@Component({
  selector: 'app-search-summary',
  templateUrl: './search-summary.component.html',
  styleUrls: ['./search-summary.component.css']
})
export class SearchSummaryComponent implements OnInit, IModal {
  @Input() delegate!: AppSearchSummary;

  constructor() {
  }

  ngOnInit(): void {
  }

  changed(): Observable<any> {
    return of(true);
  }

  invalidValues(): Array<[string, string]> {
    return [];
  }

  serialize(): any {
    return this.delegate.serialize;
  }

  valid(): boolean {
    return true;
  }
}
