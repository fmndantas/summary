import {generateUuid, IContainer, IContainerOperator, SequencedTraversal} from "../src/app/container/container.model";
import {ContainerBuilder, ContainerComparator} from "./utils";

describe("Testing state acquirance", () => {
  let root: IContainer;
  let traverser: IContainerOperator = new SequencedTraversal();

  beforeEach(() => {
    root = ContainerBuilder.buildTreeContainerType1(true);
  })

  it("the state should be a snapshot of its reference", () => {
    let snapshot: IContainer = root.getState();
    expect(ContainerComparator.equal(snapshot, root)).toBeTruthy();
    expect(root.Title === snapshot.Title).toBeTruthy();
    expect(root.isCompleted === snapshot.isCompleted).toBeTruthy();
  })

  it("when random titles are considered, the state should be a snapshot of its reference", () => {
    let snapshot: IContainer = root.getState();
    expect(ContainerComparator.equal(snapshot, root)).toBeTruthy();
  })

  it("when root is copied and after receives a new child, the snapshot should be different than root",
    () => {
      let snapshot: IContainer = root.getState();
      root.addChild(ContainerBuilder.buildRandomContainer(false));
      expect(ContainerComparator.equal(root, snapshot)).toBeFalsy();
    })

  it("when root is copied and after the snapshot receives a new child, the snapshot should be different than root",
    () => {
      let snapshot: IContainer = root.getState();
      snapshot.addChild(ContainerBuilder.buildRandomContainer(false));
      expect(ContainerComparator.equal(root, snapshot)).toBeFalsy();
    })

  it("when container state is set, it should have the same composition than its reference, " +
    "but it should not be the same object", () => {
    let different: IContainer = ContainerBuilder.buildTreeContainerType2(false);
    expect(ContainerComparator.equal(root, different)).toBeFalsy();
    expect(root === different).toBeFalsy();
    expect(root == different).toBeFalsy();
    different.setState(root);
    expect(ContainerComparator.equal(root, different)).toBeTruthy();
    expect(root === different).toBeFalsy();
    expect(root == different).toBeFalsy();
  })

  it("no completeness snapshot should cause snapshot to be different than its reference", () => {
    root
      .Children[0]
      .Children[0]
      .mark();
    let different: IContainer = ContainerBuilder.buildTreeContainerType2(false);
    different.setState(root);
    expect(ContainerComparator.equal(root, different)).toBeTruthy();
  })

  it("no title snapshot should cause snapshot to be different than its reference", () => {
    root
      .Children[0]
      .Children[0]
      .Title = generateUuid();
    let different: IContainer = ContainerBuilder.buildTreeContainerType2(false);
    different.setState(root);
    expect(ContainerComparator.equal(root, different)).toBeTruthy();
  })

  it("root should be initialized with empty container to be copied", () => {
    expect(root.getContainerBeingCopied()).toBeNull();
  })

  it("root should receive correct snapshot of container to be cut-and-pasted", () => {
    let whatLift: IContainer = root
      .Children[2]
      .Children[0]
      .Children[0];
    whatLift.liftItselfToTheRoot();
    let beingCopied: IContainer | null = root.getContainerBeingCopied();
    if (beingCopied == null) {
      throw new Error("Container being copied should not be empty");
    }
    expect(ContainerComparator.equal(beingCopied, whatLift)).toBeTruthy();
    expect(beingCopied !== whatLift).toBeTruthy();
  })

  it("when there's no container to be copied, target should remain the same", () => {
    let oldRootState = root.getState();
    root.receiveCopy();
    expect(ContainerComparator.equal(root, oldRootState)).toBeTruthy();
  })

  it("when there's a container to be copied, target should receive this container", () => {
    let beingCopied: IContainer = traverser.operate(root, {sequence: [2, 0, 0]});
    let target: IContainer = traverser.operate(root, {sequence: [0]});
    let targetSnapshot: IContainer = target.getState();
    beingCopied.liftItselfToTheRoot();

    // only root should receive container to be copied
    expect(root.Children[2].getContainerBeingCopied()).toBeNull();
    expect(root.Children[2].Children[0].getContainerBeingCopied()).toBeNull();
    expect(root.Children[2].Children[0].Children[0].getContainerBeingCopied()).toBeNull();

    let rootBeingCopied: IContainer | null = root.getContainerBeingCopied();
    if (rootBeingCopied != null) {
      expect(ContainerComparator.equal(rootBeingCopied, beingCopied));
    }

    target.receiveCopy();

    // a new child should be inserted on target
    expect(targetSnapshot.Children.length).toEqual(target.Children.length - 1);

    // the recently inserted container should be a copy
    expect(target.Children[target.Children.length - 1] == beingCopied).toBeFalsy();
    expect(ContainerComparator.equal(
      target.Children[target.Children.length - 1],
      beingCopied
    )).toBeTruthy();

    // after have been inserted, root should not have container to be copied
    expect(root.getContainerBeingCopied()).toBeNull();
    target.receiveCopy();
    expect(targetSnapshot.Children.length).toEqual(target.Children.length - 1);
  })
});
