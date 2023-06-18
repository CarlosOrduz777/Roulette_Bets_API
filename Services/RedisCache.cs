using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace RouletteBetsApi.Services
{
    public static class RedisCache
    {
        public static async Task SetValueAsync<T>(this IDistributedCache cache,string recordId,T data,TimeSpan? absoluteExpireTime = null,
            TimeSpan? slidingExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = slidingExpireTime;
            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task<T?> GetValueAsync<T>(this IDistributedCache cache,string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);
            if (jsonData is null)
                return default(T);
            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
