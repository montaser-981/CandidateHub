namespace CandidateHub.Repositories.CacheService
{
    public interface ICacheService
    {
        void Add<T>(string key, T item);
        T? Get<T>(string key);
        void Remove(string key);
    }
}
