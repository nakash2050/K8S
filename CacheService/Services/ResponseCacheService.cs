using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace CacheService.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response is null)
            {
                return;
            }

            var serializeResponse = System.Text.Json.JsonSerializer.Serialize(response);
            await _distributedCache.SetStringAsync(cacheKey, serializeResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
            }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            var cachedResponse = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
        }
    }
}
