using System.Collections.Generic;
using summary.Models;

namespace api.Models;

public class AbstractRepository<T> : IAbstractRepository<T> where T : class
{
    private IAbstractRepositoryImpl<T> _impl;

    public AbstractRepository(IAbstractRepositoryImpl<T> impl)
    {
        _impl = impl;
    }

    public void Add(T entity)
    {
        _impl.Add(entity);
    }

    public void Update(T entity)
    {
        _impl.Update(entity);
    }

    public List<T> FindAll()
    {
        return _impl.FindAll();
    }
}