using System.Collections.Generic;

namespace api.Models;

public interface IAbstractRepositoryImpl<T>
{
    T Add(T entity);
    T Update(T entity);
    List<T> FindAll();
    T FindById(int id);
    T Delete(int id);
    void Commit();
}