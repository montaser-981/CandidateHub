using System.Collections.Concurrent;
using System.Text.Json;

namespace CandidateHub.Repositories.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly ConcurrentDictionary<string, string> cache = new();

        public CacheService()
        {
        }


        public void Add<T>(string key, T item)
        {
            this.Remove(key);

            if (item != null)
            {
                string cachedData = JsonSerializer.Serialize(item);

                cache.TryAdd(key, cachedData);
            }
        }
        public T? Get<T>(string key)
        {

            cache.TryGetValue(key, out string? cachedData);

            if (cachedData != null)
            {

                var returnData = JsonSerializer.Deserialize<T>(cachedData);


                return returnData;
            }
            return default;
        }

        public void Remove(string key)
        {
            cache.TryRemove(key, out _);
        }
    }
}
