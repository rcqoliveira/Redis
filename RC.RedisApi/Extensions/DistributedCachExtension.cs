using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RC.RedisApi.Extensions
{
    public static class DistributedCachExtension
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, 
            string recordId,
            T data,
            TimeSpan? absoluteExpiredTime = null,
            TimeSpan? unusedExpireTime = null)
        {

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiredTime ?? TimeSpan.FromDays(1),
                SlidingExpiration = unusedExpireTime
            };

            var json = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, json, options);
        }



        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var json = await cache.GetStringAsync(recordId);

            if (json is null)
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}