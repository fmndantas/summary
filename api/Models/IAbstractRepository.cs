using System.Collections.Generic;

namespace api.Models;

public interface IAbstractRepository<T>
{
    T Add(T entity);
    T Update(T entity);
    List<T> FindAll();
    T Delete(int id);
}