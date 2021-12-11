using System.Threading.Tasks;

namespace Redis101.Services
{
    public interface ICacheService
    {
        Task<string> GetCacheValueAsync(string key);
        Task SetCacheValueAsync(string key, string value);
    }
}