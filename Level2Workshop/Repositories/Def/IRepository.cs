using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level2Workshop.Repositories.Def
{
    public interface IRepository<T>
    {
        bool Add(T item);
        bool Remove(T item);
        T Get(int id);
        T Get(string name);
        IEnumerable<T> Get();
        bool Update(T item,int id);
        bool ContainsKey(int itemId);
    }
}
