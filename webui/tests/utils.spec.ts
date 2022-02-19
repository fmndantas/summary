import {generateUuid, IContainer} from "../src/app/container/container.model";
import {ContainerBuilder, ContainerComparator} from "./utils";

describe("Testing equal", () => {
  let containerA: IContainer;
  let containerB: IContainer;
  let containerC: IContainer;
  let containerD: IContainer;
  let containerE: IContainer;

  beforeEach(() => {
    containerA = ContainerBuilder.buildTreeContainerType1(false);
    containerB = ContainerBuilder.buildTreeContainerType1(false);
    containerC = containerB;
    containerD = ContainerBuilder.buildTreeContainerType2(false);
    containerE = ContainerBuilder.buildAlmostEqualToType1(false);
  });

  it("(A) should have same memory address when compared to itself", () => {
    expect(containerA === containerA).toBeTruthy();
  })

  it("(B) should have same memory address when compared to itself", () => {
    expect(containerB === containerB).toBeTruthy();
  })

  it("(B) should have same memory address when compared to (C)", () => {
    expect(containerB === containerB).toBeTruthy();
  })

  it("(C) should have same memory address when compared to itself", () => {
    expect(containerC === containerC).toBeTruthy();
  })

  it("(B) should not have the same memory address than (A)", () => {
    expect(containerA === containerB).toBeFalsy();
  })

  it("(C) should not have the same memory address than (A)", () => {
    expect(containerA === containerC).toBeFalsy();
  })

  it("(A) should have the same composition than (A)", () => {
    expect(ContainerComparator.equal(containerA, containerA)).toBeTruthy();
  })

  it("(B) should have the same composition than (B)", () => {
    expect(ContainerComparator.equal(containerB, containerB)).toBeTruthy();
  })

  it("(C) should have the same composition than (C)", () => {
    expect(ContainerComparator.equal(containerC, containerC)).toBeTruthy();
  })

  it("(A) should have the same composition than (B)", () => {
    expect(ContainerComparator.equal(containerA, containerB)).toBeTruthy();
  })

  it("(A) should have the same composition than (C)", () => {
    expect(ContainerComparator.equal(containerA, containerC)).toBeTruthy();
  })

  it("(B) should have the same composition than (C)", () => {
    expect(ContainerComparator.equal(containerB, containerC)).toBeTruthy();
  })

  it("when considering titles, (A) should not be equal to (B)", () => {
    let containerA2 = ContainerBuilder.buildTreeContainerType1(true);
    let containerB2 = ContainerBuilder.buildTreeContainerType1(true);
    expect(ContainerComparator.equal(containerA2, containerB2)).toBeFalsy();
  })

  it("(A) should not be equal to (D)", () => {
    expect(ContainerComparator.equal(containerA, containerD)).toBeFalsy();
  })

  it("(B) should not be equal to (D)", () => {
    expect(ContainerComparator.equal(containerB, containerD)).toBeFalsy();
  })

  it("(C) should not be equal to (D)", () => {
    expect(ContainerComparator.equal(containerC, containerD)).toBeFalsy();
  })

  it("when considering titles, (D) should be equal to (D)", () => {
    expect(ContainerComparator.equal(containerD, containerD)).toBeTruthy();
  })

  it("empty containers should be equal", () => {
    let emptyA = ContainerBuilder.buildEmptyContainer(false);
    let emptyB = ContainerBuilder.buildEmptyContainer(false);
    expect(ContainerComparator.equal(emptyA, emptyB)).toBeTruthy();
  })

  it("when considering titles, empty containers should not be equal", () => {
    let emptyA = ContainerBuilder.buildEmptyContainer(true);
    let emptyB = ContainerBuilder.buildEmptyContainer(true);
    expect(ContainerComparator.equal(emptyA, emptyB)).toBeFalsy();
  })

  it("slightly different containers should not be equal", () => {
    expect(ContainerComparator.equal(containerA, containerE)).toBeFalsy();
  })

  it("differences in completeness should make containers unequal", () => {
    let different: IContainer = ContainerBuilder.buildTreeContainerType2(false);
    different.setState(containerA);
    // unique leaf of third child completeness is changed
    different
      .Children[2]
      .Children[0]
      .Children[0]
      .setCompleted(true);
    expect(ContainerComparator.equal(containerA, different)).toBeFalsy();
  })

  it("differences in title should make containers unequal", () => {
    let different: IContainer = ContainerBuilder.buildTreeContainerType2(false);
    different.setState(containerA);
    // unique leaf of third child title is changed
    different
      .Children[2]
      .Children[0]
      .Children[0]
      .Title = generateUuid();
    expect(ContainerComparator.equal(containerA, different)).toBeFalsy();
  })
})
