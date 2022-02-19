import {Container, generateUuid, IContainer} from "../src/app/container/container.model";

export class OrderedContainerInsertion {
  public addChildOnSpecifiedOrdering(root: IContainer, ordering: number[]): IContainer[] {
    let children: IContainer[] = [];
    ordering.forEach(_ => children.push(new Container("", null)));
    ordering.forEach((x, index) => root.addChildOnPosition(children[index], x));
    return children;
  }
}

export class ContainerBuilder {
  public static DEFAULT_TITLE: string = "default";

  public static buildRandomContainer(randomTitles: boolean) {
    return new Container(randomTitles ? generateUuid() : ContainerBuilder.DEFAULT_TITLE, null);
  }

  /*
              r
         c1       c2  c3
    c11 c12 c13       c31
                      c311
   */
  public static buildTreeContainerType1(randomTitles: boolean = false): IContainer {
    let root: IContainer = ContainerBuilder.buildRandomContainer(randomTitles);
    let c1: IContainer = ContainerBuilder.buildRandomContainer(randomTitles);
    root.addChild(c1);
    c1.addChild(ContainerBuilder.buildRandomContainer(randomTitles));
    c1.addChild(ContainerBuilder.buildRandomContainer(randomTitles));
    c1.addChild(ContainerBuilder.buildRandomContainer(randomTitles));
    root.addChild(ContainerBuilder.buildRandomContainer(randomTitles));
    let c3: IContainer = ContainerBuilder.buildRandomContainer(randomTitles);
    root.addChild(c3);
    let c31: IContainer = ContainerBuilder.buildRandomContainer(randomTitles);
    c3.addChild(c31);
    let c311: IContainer = ContainerBuilder.buildRandomContainer(randomTitles);
    c31.addChild(c311);
    return root;
  }

  public static buildAlmostEqualToType1(randomTitles: boolean = false): IContainer {
    let root: IContainer = ContainerBuilder.buildTreeContainerType1(randomTitles);
    root.Children[0].removeChild(root.Children[0].Children[0]);
    return root;
  }

  /*
   r
   c1
   c11
   c111
   */
  public static buildTreeContainerType2(randomTitles: boolean): IContainer {
    let root: IContainer = ContainerBuilder.buildRandomContainer(randomTitles);
    let c1: IContainer = ContainerBuilder.buildRandomContainer(randomTitles);
    root.addChild(c1);
    let c11: IContainer = ContainerBuilder.buildRandomContainer(randomTitles);
    c1.addChild(c11);
    c11.addChild(ContainerBuilder.buildRandomContainer(randomTitles));
    return root;
  }

  public static buildEmptyContainer(randomTitles: boolean): IContainer {
    return this.buildRandomContainer(randomTitles);
  }
}

export class ContainerComparator {
  public static equal(containerA: IContainer, containerB: IContainer): boolean {
    if (containerA.Title !== containerB.Title) {
      return false;
    }
    if (containerA.Children.length !== containerB.Children.length) {
      return false;
    }
    if (containerA.isCompleted !== containerB.isCompleted) {
      return false;
    }
    for (let i = 0; i < containerA.Children.length; ++i) {
      if (!this.equal(containerA.Children[i], containerB.Children[i])) {
        return false;
      }
    }
    return true;
  }
}
