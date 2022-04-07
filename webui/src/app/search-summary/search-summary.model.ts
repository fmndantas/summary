import {IPlainSearchContainer, SearchContainer} from "../search-container/search-container.model";

export interface ISearchSummary {
  title: string;
  root: IPlainSearchContainer;
}

export class AppSearchSummary {
  public title!: string;
  public root!: SearchContainer;

  constructor(json: ISearchSummary) {
    this.title = json.title;
    this.root = new SearchContainer(json.root);
  }

  get serialize(): ISearchSummary {
    return {
      title: this.title,
      root: this.root.serialize()
    }
  }
}

export class NullAppSearchSummary extends AppSearchSummary {
  constructor() {
    super({title: "", root: {title: [{content: "", selected: false}], children: []}});
  }
}
