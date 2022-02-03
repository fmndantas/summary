import {Component, OnInit} from '@angular/core';
import {SummaryService} from "../summary/summary.service";
import {AppSummary, EmptyAppSummary, ISummary} from "../summary/summary.model";

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  summaries!: AppSummary[];
  summary: AppSummary = new EmptyAppSummary();
  _showContent: boolean = false;

  constructor(private summaryService: SummaryService) {
  }

  ngOnInit(): void {
    this.fetchSummaries();
  }

  private fetchSummaries() {
    this.summaryService
      .findAll$()
      .subscribe(response => {
        this.summaries = response.map(x => new AppSummary(x));
      });
  }

  showContent(i: number) {
    this.summary = this.summaries[i];
    this._showContent = true;
  }

  hideContent() {
    this.fetchSummaries();
    this._showContent = false;
    this.summary = new EmptyAppSummary();
  }

  handleSave() {
    this.summaryService
      .save$(this.summary.serialize)
      .subscribe();
  }

  handleUpdate() {
    this.summaryService
      .update$(this.summary.serialize)
      .subscribe();
    this._showContent = false;
  }
}
