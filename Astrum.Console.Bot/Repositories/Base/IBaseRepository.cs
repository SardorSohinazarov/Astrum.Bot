using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astrum.Console.Bot.Repositories.Base
{
    public interface IBaseRepository<TKey, TValue>
    {
        Task<TValue> Insert(TValue item);
        Task<TValue> GetById(TKey key);
        Task<List<TValue>> GetAll();
        Task<List<TValue>> Filter(Func<TValue, bool> predicate);
        Task<TValue> Update(TKey key, TValue item);
        Task<TValue> Delete(TKey key);
    }
}
