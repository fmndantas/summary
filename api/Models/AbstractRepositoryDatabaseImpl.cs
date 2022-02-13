using System;
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

    public T Add(T entity)
    {
        _set.Add(entity);
        Commit();
        return entity;
    }

    public T Update(T entity)
    {
        _set.Update(entity);
        Commit();
        return entity;
    }

    public List<T> FindAll()
    {
        return _set.ToList();
    }

    public T FindById(int id)
    {
        var entity = _set.Find(id);
        if (entity != null)
        {
            return entity;
        }

        throw new Exception(ExceptionMessages.EntityNotFound);
    }

    public T Delete(int id)
    {
        var entity = FindById(id);
        if (entity != null)
        {
            _set.Remove(entity);
            Commit();
            return entity;
        }

        throw new Exception(ExceptionMessages.EntityNotFound);
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
}