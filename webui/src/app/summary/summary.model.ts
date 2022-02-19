import {Container, PlainContainer} from "../container/container.model";

export interface ISummary {
  id: number;
  title: string,
  author: string;
  serializedRoot: string;
  year: number | null;
}

export class AppSummary {
  public id: number;
  public title: string;
  public author: string;
  public root: Container;
  public year: number | null;

  constructor(json: ISummary) {
    this.id = json.id;
    this.title = json.title;
    this.author = json.author;
    let plainContent: PlainContainer = JSON.parse(json.serializedRoot);
    this.root = new Container(this.title, null);
    this.root.setStateFromJson(plainContent);
    this.year = json.year;
  }

  get serialize(): ISummary {
    return {
      id: this.id,
      title: this.title,
      author: this.author,
      serializedRoot: JSON.stringify(this.root.serialize()),
      year: this.year
    }
  }

  get progress(): number {
    return this.root.doneItems / this.root.totalOfItems;
  }

  get empty(): boolean {
    return false;
  }
}

const NullContainer: PlainContainer = {
  title: "",
  completed: false,
  children: []
}

export class NullAppSummary extends AppSummary {
  constructor() {
    super({
      id: 0,
      title: "",
      author: "",
      serializedRoot: JSON.stringify(NullContainer),
      year: 0
    });
  }

  get empty(): boolean {
    return true;
  }
}
