using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class AbstractRepositoryDatabaseImpl<T> : IAbstractRepositoryImpl<T> where T : class
{
    private DbSet<T> _set;
    
    public AbstractRepositoryDatabaseImpl(SummaryContext summaryContext)
    {
        _set = summaryContext.Set<T>();
    }

    public void Add(T entity)
    {
        _set.Add(entity);
    }

    public void Update(T entity)
    {
        _set.Add(entity);
    }

    public List<T> FindAll()
    {
        return _set.ToList();
    }
}