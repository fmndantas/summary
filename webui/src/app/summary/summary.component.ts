import {Component, Input, OnChanges, SimpleChanges} from '@angular/core';
import {Observable, of} from 'rxjs';
import {IContainer} from "../container/container.model";
import {IModal} from "../generic-modal/generic-modal.model";
import {AppSummary, ISummary} from "./summary.model";

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent implements OnChanges, IModal {
  @Input() summary!: AppSummary;
  inputValue: string;
  states!: IContainer[];
  currentState!: number;

  constructor() {
    this.inputValue = "";
    this.states = [];
    this.currentState = -1;
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.resetTimeline();
  }

  extendTimeline() {
    while (this.states.length - 1 > this.currentState) {
      this.states.pop();
    }
    this.states.push(this.summary.root.getState());
    this.currentState = this.states.length - 1;
  }

  resetTimeline() {
    this.currentState = -1;
    this.states = [];
    this.extendTimeline();
  }

  back() {
    this.currentState = Math.max(0, this.currentState - 1);
    this.summary.root.setState(this.states[this.currentState]);
  }

  forward() {
    this.currentState = Math.min(this.states.length - 1, this.currentState + 1)
    this.summary.root.setState(this.states[this.currentState]);
  }

  invalidValues(): [string, string][] {
    return [["", ""]];
  }

  valid(): boolean {
    return true;
  }

  changed(): Observable<any> {
    return of(true);
  }

  serialize(): ISummary {
    return this.summary.serialize;
  }
}
