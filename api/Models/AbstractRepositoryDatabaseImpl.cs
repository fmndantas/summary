using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class AbstractRepositoryDatabaseImpl<T> : IAbstractRepositoryImpl<T> where T : class
{
    private DbSet<T> _set;
    private DbContext _context;
    
    public AbstractRepositoryDatabaseImpl(SummaryContext summaryContext)
    {
        _set = summaryContext.Set<T>();
        _context = summaryContext;
    }

    public void Add(T entity)
    {
        _set.Add(entity);
        Commit();
    }

    public void Update(T entity)
    {
        _set.Update(entity);
        Commit();
    }

    public List<T> FindAll()
    {
        return _set.ToList();
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
}