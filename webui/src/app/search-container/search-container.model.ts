interface IToken {
  content: string;
  selected: boolean;
}

export interface IPlainSearchContainer {
  title: IToken[],
  children: IPlainSearchContainer[],
}

export interface ISearchContainer {
  getChildren(): ISearchContainer[],

  isLeaf(): boolean;

  serialize(): IPlainSearchContainer;

  getTitle(): IToken[];
}

export class SearchContainer implements ISearchContainer {
  title!: IToken[];
  children!: SearchContainer[];

  constructor(plainSearchContainer: IPlainSearchContainer) {
    this.title = plainSearchContainer.title;
    this.children = [];
    plainSearchContainer.children.forEach(x => this.children.push(new SearchContainer(x)));
  }

  isLeaf(): boolean {
    return this.children.length === 0;
  }

  getChildren(): ISearchContainer[] {
    return this.children;
  }

  getTitle(): IToken[] {
    return this.title;
  }

  serialize(): IPlainSearchContainer {
    return {
      title: this.title,
      children: this.children.map(x => x.serialize())
    };
  }

}
