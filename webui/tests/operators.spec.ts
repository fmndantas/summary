import 'jest';
import {generateUuid, ICrudContainer, IContainerOperator, SequencedTraversal} from "../src/app/container/container.model";
import {ContainerBuilder, ContainerComparator} from "./utils";

describe("Test sequenced traverser", () => {
  let root!: ICrudContainer;
  let traverser: IContainerOperator = new SequencedTraversal();


  beforeEach(() => {
    root = ContainerBuilder.buildTreeContainerType1(true);
  })

  it("after sequenced traversal, mid-way containers should remain unchanged and " +
    "target should be pointed by traversal result", () => {
    root.Title = generateUuid();

    let child: ICrudContainer = root
      .Children[2]
      .Children[0]
      .Children[0];

    let sameAsChild: ICrudContainer = root.accept(traverser, {sequence: [2, 0, 0]});

    child.Title = generateUuid();

    expect(root.Title === child.Title).toBeFalsy();
    expect(root === child || root === sameAsChild).toBeFalsy();

    expect(root.Children[2].Title === child.Title).toBeFalsy();
    expect(root.Children[2] === child || root.Children[2] === sameAsChild).toBeFalsy();

    expect(root.Children[2].Children[0].Title === child.Title).toBeFalsy();
    expect(root.Children[2].Children[0] === child || root.Children[2].Children[0] === sameAsChild).toBeFalsy();

    expect(root.Children[2].Children[0].Children[0].Title === child.Title).toBeTruthy();
    expect(root.Children[2].Children[0].Children[0] === child
      || root.Children[2].Children[0].Children[0] === sameAsChild).toBeTruthy();

    expect(sameAsChild === child).toBeTruthy();
    expect(sameAsChild.Title).toEqual(child.Title);
    expect(ContainerComparator.equal(sameAsChild, child)).toBeTruthy();
  })
})
