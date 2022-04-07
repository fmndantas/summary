import {Component, OnInit} from '@angular/core';
import {SummaryService} from "../summary/summary.service";
import {AppSearchSummary, ISearchSummary, NullAppSearchSummary} from "../search-summary/search-summary.model";

@Component({
  selector: 'app-page-search',
  templateUrl: './page-search.component.html',
  styleUrls: ['./page-search.component.css']
})
export class PageSearchComponent implements OnInit {
  searchSummaries: AppSearchSummary[] = [];
  searchSummary: AppSearchSummary = new NullAppSearchSummary();
  _showSearchItem: boolean = false;

  constructor(private summaryService: SummaryService) {
  }

  ngOnInit(): void {
  }

  search(searchText: string) {
    this.summaryService
      .findByTitle$(searchText)
      .subscribe((response: ISearchSummary[]) => {
          this.searchSummaries = response.map(x => new AppSearchSummary(x));
        }
      );
  }

  showSearchItem(i: number) {
    this.searchSummary = this.searchSummaries[i];
    this._showSearchItem = true;
  }

  hideSearchItem() {
    this._showSearchItem = false;
  }

  thereAreResults(): boolean {
    return this.searchSummaries.length > 0;
  }
}
