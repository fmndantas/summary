import {Container, IContainer} from "../src/app/container/container.model";

export class OrderedContainerInsertion {
  public addChildOnSpecifiedOrdering(root: IContainer, ordering: number[]): IContainer[] {
    let children: IContainer[] = [];
    ordering.forEach(_ => children.push(new Container("", null)));
    ordering.forEach((x, index) => root.addChildOnPosition(children[index], x));
    return children;
  }
}
