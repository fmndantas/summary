using System;
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

    public Summary Add(Summary entity)
    {
        entity.Id = Id++;
        entities.Add(entity);
        return entity;
    }

    public List<Summary> FindAll()
    {
        return entities;
    }

    public Summary Update(Summary entity)
    {
        throw new NotImplementedException();
    }

    public Summary FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Summary Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Commit()
    {
        throw new NotImplementedException();
    }
}