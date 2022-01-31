import 'jest';
import {IContainer, Container} from "../src/app/container/container.model";

describe("Common tree container", () => {
  let root!: IContainer;
  let children!: IContainer[];

  beforeEach(() => {
    /*
      r
     /  \
    0   1
    |  /|\
    2 3 4 5
     */
    root = new Container("", null);
    children = [];
    for (let i = 0; i < 6; ++i) {
      children.push(new Container("", null));
    }
    root.addChild(children[0]);
    root.addChild(children[1]);
    children[0].addChild(children[2]);
    children[1].addChild(children[3]);
    children[1].addChild(children[4]);
    children[1].addChild(children[5]);
  });

  it("root should have on total items", () => {
    expect(root.totalOfItems).toEqual(4);
  })

  it("root should have zero done items", () => {
    expect(root.doneItems).toEqual(0);
  })

  it("the number of root's done items should be progressively increased", () => {
    children[5].mark();
    expect(root.doneItems).toEqual(1);
    children[4].mark();
    expect(root.doneItems).toEqual(2);
    children[3].mark();
    expect(root.doneItems).toEqual(3);
    children[2].mark();
    expect(root.doneItems).toEqual(4);
  })

  it("the root should be found", () => {
    children.forEach(x => expect(x.Root).toEqual(root));
  })

  it("nested addition should be correct", () => {
    let oldTotal: number = root.totalOfItems;
    let childrenTotals: number[] = children.map((x: IContainer) => x.totalOfItems);
    root.addNested("new nested container");
    expect(root.totalOfItems).toEqual(oldTotal + 1);
    children.forEach((x, index) => expect(x.totalOfItems).toEqual(childrenTotals[index]));
  })

  it("equal before should be correct", () => {
    root.Children[0].addEqualBefore("b0");
    root.Children[0].addEqualBefore("b1");
    root.Children[0].addEqualBefore("b2");
    root.Children[1].addEqualBefore("b3");
    root.Children[2].addEqualBefore("b4");
    root.Children[3].addEqualBefore("b5");
    expect(root.Children[0].Title).toEqual("b2");
    expect(root.Children[1].Title).toEqual("b3");
    expect(root.Children[2].Title).toEqual("b4");
    expect(root.Children[3].Title).toEqual("b5");
    expect(root.Children[4].Title).toEqual("b1");
    expect(root.Children[5].Title).toEqual("b0");
    // 0 1
    // b0 0 1
    // b1 b0 0 1
    // b2 b1 b0 0 1
    // b2 b3 b1 b0 0 1
    // b2 b3 b4 b1 b0 0 1
    // b2 b3 b4 b5 b1 b0 0 1
  })

  it("equal after should be correct", () => {
    root.Children[0].addEqualAfter("b0");
    root.Children[0].addEqualAfter("b1");
    root.Children[0].addEqualAfter("b2");
    root.Children[1].addEqualAfter("b3");
    root.Children[2].addEqualAfter("b4");
    root.Children[3].addEqualAfter("b5");
    expect(root.Children[1].Title).toEqual("b2");
    expect(root.Children[2].Title).toEqual("b3");
    expect(root.Children[3].Title).toEqual("b4");
    expect(root.Children[4].Title).toEqual("b5");
    expect(root.Children[5].Title).toEqual("b1");
    expect(root.Children[6].Title).toEqual("b0");
    // 0 1
    // 0 b0 1
    // 0 b1 b0 1
    // 0 b2 b1 b0 1
    // 0 b2 b3 b1 b0 1
    // 0 b2 b3 b4 b1 b0 1
    // 0 b2 b3 b4 b5 b1 b0 1
  })
})
