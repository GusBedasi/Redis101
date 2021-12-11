using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Redis101.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _redisClient;

        public CacheService(IConnectionMultiplexer redisClient)
        {
            _redisClient = redisClient.GetDatabase() ?? throw new ArgumentNullException(nameof(redisClient));
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            return await _redisClient.StringGetAsync(key);
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            await _redisClient.StringSetAsync(key, value);
        }
    }
}