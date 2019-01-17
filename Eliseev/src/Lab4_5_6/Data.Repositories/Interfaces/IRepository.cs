using System.Collections.Generic;

namespace Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Create(T item);
        IEnumerable<T> GetAll();
    }
}