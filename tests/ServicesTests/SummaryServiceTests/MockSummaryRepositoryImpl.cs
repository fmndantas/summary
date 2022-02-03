using System.Collections.Generic;
using api.Models;
using api.Models.Entities;

namespace tests.ServicesTests.SummaryServiceTests;

public class MockSummaryRepositoryImpl : IAbstractRepositoryImpl<Summary>
{
    private List<Summary> entities;
    private int Id { get; set; }

    public MockSummaryRepositoryImpl()
    {
        entities = new List<Summary>();
        Id = 1;
    }

    public void Add(Summary entity)
    {
        entity.Id = Id++;
        entities.Add(entity);
    }

    public void Update(Summary entity)
    {
    }

    public List<Summary> FindAll()
    {
        return entities;
    }

    public void Commit()
    {
    }
}