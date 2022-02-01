using System.Collections.Generic;

namespace summary.Models;

public interface IAbstractRepository<T>
{
    void Add(T entity);
    void Update(T entity);
    List<T> FindAll();
}