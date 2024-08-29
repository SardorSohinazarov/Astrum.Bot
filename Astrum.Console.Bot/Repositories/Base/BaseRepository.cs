using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Astrum.Console.Bot.Repositories.Base
{
    public class BaseRepository<TKey, TValue> : IBaseRepository<TKey, TValue>
    {
        string folderPath = "C:\\Users\\sardo\\source\\repos\\Astrum\\Astrum.Console.Bot\\Repositories\\Database\\";
        public async Task<TValue> Delete(TKey key)
        {
            var values = await GetAll();

            var item = await GetById(key);
            values.Remove(item);

            File.WriteAllText(await GetFullPath(), JsonSerializer.Serialize(values, options: new JsonSerializerOptions() { WriteIndented = true }));

            return item;
        }

        public async Task<List<TValue>> Filter(Func<TValue, bool> predicate)
        {
            var values = await GetAll();
            var filtered = values.Where(predicate);

            return filtered.ToList();
        }

        public async Task<List<TValue>> GetAll()
        {
            var path = await GetFullPath();
            var jsonContent = await File.ReadAllTextAsync(path);

            return JsonSerializer.Deserialize<List<TValue>>(jsonContent);
        }

        public async Task<TValue> GetById(TKey key)
        {
            var values = await GetAll();
            var propertyId = typeof(TValue).GetProperty("Id");
            var item = values.Find(x => propertyId.GetValue(x) == (object)key);
            return item;
        }

        public async Task<TValue> Insert(TValue item)
        {
            var path = GetFullPath();
            var values = await GetAll();
            values.Add(item);
            File.WriteAllText(await path, JsonSerializer.Serialize(values));

            return item;
        }

        public async Task<TValue> Update(TKey key, TValue item)
        {
            var existed = await GetById(key);
            var values =await GetAll();
            existed = item;

            return existed;
        }

        Task<string> GetFullPath()
        {
            return Task.FromResult(Path.Combine(folderPath, typeof(TValue).Name + ".json"));
        }
    }
}
