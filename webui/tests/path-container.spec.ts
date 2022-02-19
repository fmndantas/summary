import 'jest';
import {IContainer, Container} from "../src/app/container/container.model";

describe("Path container", () => {
  let root!: IContainer;
  let child1!: IContainer;
  let child2!: IContainer;
  let child3!: IContainer;

  beforeEach(() => {
    root = new Container("", null);
    child1 = new Container("", null);
    root.addChild(child1);
    child2 = new Container("", null);
    child1.addChild(child2);
    child3 = new Container("", null);
    child2.addChild(child3);
  });

  it("leaf should be initialized as incomplete", () => {
    expect(child3.isCompleted).toBeFalsy();
  })

  it("leaf should be completed", () => {
    child3.setCompleted(true);
    expect(child3.isCompleted).toBeTruthy();
    expect(child2.isCompleted).toBeTruthy();
    expect(child1.isCompleted).toBeTruthy();
    expect(root.isCompleted).toBeTruthy();
  })

  it("leaf should be correctly removed", () => {
    root.removeChild(root.Children[0]);
    root.removeChild(root.Children[2]);
    expect(root.Children.find(x => x == child1)).toBeUndefined();
    expect(root.Children.find(x => x == child3)).toBeUndefined();
  })

  it("title should be correctly set", () => {
    let newTitle = "this is a brand new title";
    root.Title = newTitle;
    expect(root.Title).toEqual(newTitle);
  })

  it("leaf should have one total items", () => {
    expect(child3.totalOfItems).toEqual(1);
    child3.mark();
    expect(child3.totalOfItems).toEqual(1);
  })

  it ("root should have one total items", () => {
    expect(root.totalOfItems).toEqual(1);
  })

  it("father removal should be correct", () => {
    child3.removeItselfFromFather();
    expect(child2.Children.length).toEqual(0);
    child2.removeItselfFromFather();
    expect(child1.Children.length).toEqual(0);
    child1.removeItselfFromFather();
    expect(root.Children.length).toEqual(0);
  })
})
