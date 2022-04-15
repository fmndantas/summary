using api.Models.Entities;
using api.Models.Utils;
using NUnit.Framework;

namespace tests.ModelTests;

public class SummaryTest
{
    private Summary _summary;

    [SetUp]
    public void SetUp()
    {
        _summary = SummaryGenerator.Generate(20, 5, 5);
    }

    [Test]
    public void RootContainerShouldBeLazyInitialized()
    {
        Assert.True(_summary.IsRootEmpty);
        var root = _summary.Root;
        Assert.False(_summary.IsRootEmpty);
    }

    [Test]
    public void ContainerReturnedByGetterShouldBeTheSameThanSummaryContainer()
    {
        var newTitle = "12345678890!@#$%*()";
        var root = _summary.Root;
        _summary.Root.Title = newTitle;
        Assert.AreEqual(newTitle, root.Title);
    }
}