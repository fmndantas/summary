import {Component, OnInit, ViewChild} from '@angular/core';
import {SummaryService} from "../summary/summary.service";
import {AppSummary, NullAppSummary, ISummary} from "../summary/summary.model";
import {IModal} from "../generic-modal/generic-modal.model";

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  @ViewChild("form") form!: IModal;

  summaries!: AppSummary[];
  summary: AppSummary = new NullAppSummary();
  _showContentForm: boolean = false;
  _showSummaryForm: boolean = false;

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

  setCurrentSummary(i: number) {
    this.summary = this.summaries[i];
  }

  setCurrentSummaryAsNullSummary() {
    this.summary = new NullAppSummary();
  }

  openEditContent(i: number) {
    this.setCurrentSummary(i);
    this._showContentForm = true;
  }

  openAddSummary() {
    this._showSummaryForm = true;
  }

  openEditSummary(i: number) {
    this.setCurrentSummary(i);
    this._showSummaryForm = true;
  }

  hideContentAndUpdateSummaries() {
    this._showContentForm = false;
    this._showSummaryForm = false;
    this.fetchSummaries();
    this.setCurrentSummaryAsNullSummary();
  }

  handleConfirm() {
    let newSummary: ISummary = this.form.serialize();
    if (this.summary.empty) {
      this.summaryService
        .save$(newSummary)
        .subscribe(_ => this.hideContentAndUpdateSummaries());
    } else {
      this.summaryService
        .update$(newSummary)
        .subscribe(_ => this.hideContentAndUpdateSummaries());
    }
  }

  handleDelete(i: number) {
    this.summaryService
      .delete$(this.summaries[i].serialize)
      .subscribe(_ => this.hideContentAndUpdateSummaries());
  }
}
