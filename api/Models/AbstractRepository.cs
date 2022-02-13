using System.Collections.Generic;

namespace api.Models;

public class AbstractRepository<T> : IAbstractRepository<T> where T : class
{
    private IAbstractRepositoryImpl<T> _impl;

    public AbstractRepository(IAbstractRepositoryImpl<T> impl)
    {
        _impl = impl;
    }

    public T Add(T entity)
    {
        return _impl.Add(entity);
    }

    public T Update(T entity)
    {
        return _impl.Update(entity);
    }

    public List<T> FindAll()
    {
        return _impl.FindAll();
    }

    public T Delete(int id)
    {
        return _impl.Delete(id);
    }
}