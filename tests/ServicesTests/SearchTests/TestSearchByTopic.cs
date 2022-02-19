using api.Models.Entities;
using api.Models.Utils;
using NUnit.Framework;

namespace tests.ServicesTests.SearchTests;

public class TestSearchByTopic
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
        var root = _summary.Root;
        Assert.False(root.isEmpty());
    }

    [Test]
    public void AssertContainerReturnedByGetterIsTheSameThanSummary()
    {
        var newTitle = SummaryGenerator.GenerateWord(20);
        var root = _summary.Root;
        _summary.Root.title = newTitle;
        Assert.AreEqual(newTitle, root.title);
    }
}