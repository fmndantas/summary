import {Container, PlainContainer} from "../container/container.model";

export interface ISummary {
  id: number;
  title: string,
  author: string;
  content: string
}

export class AppSummary {
  public id: number;
  public title: string;
  public author: string;
  public content: Container;

  constructor(json: ISummary) {
    this.id = json.id;
    this.title = json.title;
    this.author = json.author;
    let plainContent: PlainContainer = JSON.parse(json.content) as PlainContainer;
    this.content = new Container(this.title, null);
    this.content.setStateFromJson(plainContent);
  }

  get serialize(): ISummary {
    return {
      id: this.id,
      title: this.title,
      author: this.author,
      content: JSON.stringify(this.content.serialize())
    }
  }

  get progress(): number {
    return this.content.doneItems / this.content.totalOfItems;
  }
}

const EmptyPlainContainer: PlainContainer = {
  title: "",
  completed: false,
  children: []
}

export class EmptyAppSummary extends AppSummary {
  constructor() {
    super({id: -1, title: "", author: "", content: JSON.stringify(EmptyPlainContainer)});
  }
}
