using api.Models;
using api.Models.Entities;
using api.Services;

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
}