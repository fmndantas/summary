import {UUID} from "angular2-uuid";

export interface PlainContainer {
  title: string;
  completed: boolean;
  children: PlainContainer[];
}

export interface IContainer {
  addChild(child: IContainer): void;

  addChildOnPosition(child: IContainer, position: number): void

  removeChild(child: IContainer): void;

  get Title(): string;

  set Title(title: string);

  get Children(): IContainer[];

  get Father(): IContainer | null;

  get isLeaf(): boolean;

  get isRoot(): boolean;

  get totalOfItems(): number;

  get doneItems(): number;

  get isCompleted(): boolean;

  get Root(): IContainer;

  setCompleted(completed: boolean): void;

  setFather(father: IContainer): void;

  setContainerBeingCopied(beingCopied: IContainer | null): void;

  getContainerBeingCopied(): IContainer | null;

  liftCopyToRoot(): void;

  receiveCopy(): void;

  mark(): void;

  addNested(title: string): void;

  addEqualAfter(title: string): void;

  addEqualBefore(title: string): void;

  getState(): IContainer;

  setState(ref: IContainer): void;

  serialize(): PlainContainer;

  setStateFromJson(json: PlainContainer, reference: IContainer | null): void;

  removeThisFromFather(): void;

  // todo moveDown
  // todo moveUp
  // todo collapse
  // todo think about changes in nesting level
}

export class Container implements IContainer {
  public father: IContainer | null;
  public children!: IContainer[];
  public title: string = "";
  public completed: boolean = false;
  public beingCopied: IContainer | null = null;

  constructor(title: string, father: IContainer | null = null) {
    this.father = father;
    this.children = [];
    this.title = title;
  }

  addChild(child: IContainer) {
    child.setFather(this);
    this.children.push(child);
  }

  addChildOnPosition(child: IContainer, position: number) {
    this.children.splice(position, 0, child);
  }

  removeChild(child: IContainer) {
    this.children = this.children.filter(x => x != child);
  }

  get Title() {
    return this.title;
  }

  set Title(title: string) {
    this.title = title;
  }

  get Children() {
    return this.children;
  }

  get Father(): IContainer | null {
    return this.father;
  }

  get numberOfChildren() {
    return this.children.length;
  }

  get isLeaf() {
    return this.numberOfChildren == 0;
  }

  get isRoot() {
    return this.father == null;
  }

  get isCompleted() {
    if (this.isLeaf) {
      return this.completed;
    }
    return this.Children.filter(x => !x.isCompleted).length === 0;
  }

  setCompleted(completed: boolean) {
    if (this.isLeaf) {
      this.completed = completed;
    }
  }

  setFather(father: IContainer) {
    this.father = father;
  }

  get totalOfItems() {
    if (this.isLeaf) {
      return 1;
    }
    let ans: number = 0;
    this.children.forEach(x => ans += x.totalOfItems);
    return ans;
  }

  get doneItems() {
    if (this.isLeaf) {
      return this.completed ? 1 : 0;
    }
    let ans: number = 0;
    this.children.forEach(x => ans += x.doneItems);
    return ans;
  }

  get Root(): IContainer {
    let container: IContainer = this;
    while (container.Father != null) {
      container = container.Father;
    }
    return container;
  }

  mark() {
    if (this.isLeaf) {
      this.setCompleted(!this.isCompleted);
    }
  }

  addNested(title: string) {
    this.addChild(new Container(title, this))
  }

  addEqualAfter(title: string) {
    if (this.Father != null) {
      let index = this.Father.Children.findIndex(x => x === this);
      this.Father.addChildOnPosition(new Container(title, this.Father), index + 1);
    }
  }

  addEqualBefore(title: string) {
    if (this.Father != null) {
      let index = this.Father.Children.findIndex(x => x === this);
      this.Father.addChildOnPosition(new Container(title, this.Father), index);
    }
  }

  getState(): IContainer {
    let copy: IContainer = new Container(this.title, this.father);
    this.Children.forEach(x => copy.addChild(x.getState()));
    copy.setCompleted(this.isCompleted);
    return copy;
  }

  setState(reference: IContainer): void {
    this.children = [];
    reference.Children.forEach(x => {
      let child = new Container(x.Title, this);
      child.setState(x);
      this.addChild(child);
    })
    this.title = reference.Title;
    this.setCompleted(reference.isCompleted);
  }

  serialize(): PlainContainer {
    return {
      title: this.title,
      completed: this.isCompleted,
      children: this.Children.map(x => x.serialize())
    }
  }

  setStateFromJson(json: PlainContainer): void {
    this.children = [];
    json.children.forEach(x => {
      let child = new Container("", this);
      child.setStateFromJson(x);
      this.addChild(child);
    })
    this.title = json.title;
    this.completed = json.completed;
  }

  liftCopyToRoot(): void {
    this.Root.setContainerBeingCopied(this.getState());
  }

  getContainerBeingCopied(): IContainer | null {
    return this.beingCopied;
  }

  receiveCopy(): void {
    let container: IContainer = this.Root;
    let toCopy = container.getContainerBeingCopied();
    if (toCopy != null) {
      this.addChild(toCopy);
      container.setContainerBeingCopied(null);
    }
  }

  setContainerBeingCopied(beginCopied: IContainer | null): void {
    this.beingCopied = beginCopied;
  }

  removeThisFromFather() {
    this.Father?.removeChild(this);
  }
}

export const generateUuid = () => {
  return "uuid" + UUID.UUID().replace(/-/g, "");
}
