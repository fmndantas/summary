import 'jest';
import {Container, IContainer} from "../src/app/container/container.model";
import {OrderedContainerInsertion} from "./utils";

describe("Path container", () => {
  let root!: IContainer;
  const permutationUtil: OrderedContainerInsertion = new OrderedContainerInsertion();

  beforeEach(() => {
    root = new Container("", null);
  })

  it("child should be inserted at the beginning", () => {
    let newChild1: IContainer = new Container("", null);
    let newChild2: IContainer = new Container("", null);
    let newChild3: IContainer = new Container("", null);
    let newChild4: IContainer = new Container("", null);
    let newChild5: IContainer = new Container("", null);
    root.addChildOnPosition(newChild1, 0);
    root.addChildOnPosition(newChild2, 0);
    root.addChildOnPosition(newChild3, 0);
    root.addChildOnPosition(newChild4, 0);
    root.addChildOnPosition(newChild5, 0);
    expect(root.Children[0] === newChild5).toBeTruthy();
    expect(root.Children[1] === newChild4).toBeTruthy();
    expect(root.Children[2] === newChild3).toBeTruthy();
    expect(root.Children[3] === newChild2).toBeTruthy();
    expect(root.Children[4] === newChild1).toBeTruthy();
  })

  it("child should be inserted at the end", () => {
    let newChild1: IContainer = new Container("", null);
    let newChild2: IContainer = new Container("", null);
    let newChild3: IContainer = new Container("", null);
    let newChild4: IContainer = new Container("", null);
    let newChild5: IContainer = new Container("", null);
    root.addChildOnPosition(newChild1, 1000);
    root.addChildOnPosition(newChild2, 1000);
    root.addChildOnPosition(newChild3, 1000);
    root.addChildOnPosition(newChild4, 1000);
    root.addChildOnPosition(newChild5, 1000);
    expect(root.Children[0] === newChild1).toBeTruthy();
    expect(root.Children[1] === newChild2).toBeTruthy();
    expect(root.Children[2] === newChild3).toBeTruthy();
    expect(root.Children[3] === newChild4).toBeTruthy();
    expect(root.Children[4] === newChild5).toBeTruthy();
  })

  it("child should be inserted at the 1-5-2-4-3 ordering", () => {
    // 0 -> 0 1 -> 0 2 1 -> 0 2 1 3 -> 0 2 4 1 3
    let ordering: number[] = [0, 4, 1, 3, 2];
    let added: IContainer[] =
      permutationUtil.addChildOnSpecifiedOrdering(root, ordering);
    expect(root.Children[0] === added[0]).toBeTruthy();
    expect(root.Children[1] === added[2]).toBeTruthy();
    expect(root.Children[2] === added[4]).toBeTruthy();
    expect(root.Children[3] === added[1]).toBeTruthy();
    expect(root.Children[4] === added[3]).toBeTruthy();
  })
})
