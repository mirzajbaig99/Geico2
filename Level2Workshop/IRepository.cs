using System.Collections.Generic;

namespace Level2Workshop
{
    public interface IRepository<T>
    {
        bool Add(T item);

        bool Add(IEnumerable<T> items);

        IEnumerable<T> Get();

        T Get(int id);

        T Get(string name);

        //int GetNextId();

        bool Update(T item, int currentId);

        bool Remove(T item);
    }
}
