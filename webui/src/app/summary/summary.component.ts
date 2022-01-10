import {Component, Input, OnInit} from '@angular/core';
import {Container, IContainer, PlainContainer} from "../container/container.model";

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent implements OnInit {
  @Input() title!: string;
  root!: IContainer;
  item!: string;
  states!: IContainer[];
  currentState!: number;

  constructor() {
    this.states = [];
    this.currentState = -1;
  }

  ngOnInit(): void {
    this.root = new Container("", null);
    this.root.setStateFromJson({
      title: "chapter 1",
      completed: false,
      children: [
        {
          title: "subtopic 1.1",
          completed: true,
          children: [
            {
              title: "subsubtopic 1.1.1",
              completed: false,
              children: []
            },
            {
              title: "subsubtopic 1.1.2",
              completed: false,
              children: []
            },
            {
              title: "subsubtopic 1.1.3",
              completed: true,
              children: []
            },
          ]
        },
        {
          title: "subtopic 1.1",
          completed: true,
          children: []
        },
        {
          title: "subtopic 1.1",
          completed: true,
          children: [
            {
              title: "subsubtopic 1.1.1",
              completed: false,
              children: []
            },
            {
              title: "subsubtopic 1.1.2",
              completed: false,
              children: []
            },
            {
              title: "subsubtopic 1.1.3",
              completed: true,
              children: []
            },
          ]
        }
      ]
    }, null);
    this.extendTimeline();
  }

  extendTimeline() {
    while (this.states.length - 1 > this.currentState) {
      this.states.pop();
    }
    this.states.push(this.root.getState());
    this.currentState = this.states.length - 1;
  }

  back() {
    this.currentState = Math.max(0, this.currentState - 1);
    this.root.setState(this.states[this.currentState]);
  }

  forward() {
    this.currentState = Math.min(this.states.length - 1, this.currentState + 1)
    this.root.setState(this.states[this.currentState]);
  }

  save() {
    console.debug(JSON.stringify(this.root.serialize()));
  }
}
