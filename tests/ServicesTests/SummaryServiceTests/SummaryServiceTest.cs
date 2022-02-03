using api.Models;
using api.Models.Entities;
using api.Services;
using NUnit.Framework;

namespace tests.ServicesTests.SummaryServiceTests;

public class SummaryServiceTest
{
    private IAbstractRepository<Summary> _mockRepository;
    private ISummaryService _subject;

    public SummaryServiceTest()
    {
        _mockRepository = new AbstractRepository<Summary>(new MockSummaryRepositoryImpl());
        _subject = new SummaryService(_mockRepository);
    }

    [Test]
    public void TestSave()
    {
        var toSave = new Summary
        {
            Title = "Test title",
            Author = "Test author",
            Content = "Test content"
        };

        Assert.AreEqual(0, toSave.Id);
        _subject.Save(toSave);
        Assert.AreEqual(1, toSave.Id);
        Assert.AreEqual(1, _subject.FindAll().Count);
    }
}