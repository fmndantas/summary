using System.Collections.Generic;

namespace api.Models;

public interface IAbstractRepositoryImpl<T>
{
    void Add(T entity);
    void Update(T entity);
    List<T> FindAll();
}