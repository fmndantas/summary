import 'jest';
import {IContainer, Container} from "../src/app/container/container.model";

describe("Empty container", () => {
  let title: string = "test title";

  let container: IContainer = new Container(title, null);

  it("should have right title", () => {
    expect(container.Title).toBe(title);
  })

  it("should be root", () => {
    expect(container.isRoot).toBeTruthy();
  })

  it("should be leaf", () => {
    expect(container.isLeaf).toBeTruthy();
  })

  it("should return itself as root", () => {
    expect(container.Root === container).toBeTruthy();
  })

  it("its father should be null", () => {
    expect(container.Father).toBeNull();
  })

  it("should return empty list as children", () => {
    expect(container.Children.length === 0).toBeTruthy();
  })
})
