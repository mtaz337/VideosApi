    using Microsoft.Extensions.Caching.Distributed;
    using VideoApi.Data.Entities;
    using System.Text.Json;

    namespace VideoApi.Services
    {
        public class CacheService : ICacheService
        {
            private readonly IDistributedCache _distributedCache;

            public CacheService(IDistributedCache distributedCache)
            {
                _distributedCache = distributedCache;
            }

            public async Task CacheVideoFilesAsync(int productId, IEnumerable<VideoFile> videoFiles)
            {
                var cacheKey = GetCacheKey(productId);
                var cacheValue = JsonSerializer.Serialize(videoFiles);

                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)); // Cache expiration time

                await _distributedCache.SetStringAsync(cacheKey, cacheValue, cacheEntryOptions);
            }

            public async Task<IEnumerable<VideoFile>> GetCachedVideoFilesAsync(int productId)
                {
                    var cacheKey = GetCacheKey(productId);
                    var cacheValue = await _distributedCache.GetStringAsync(cacheKey);

                    if (cacheValue == null)
                        return Enumerable.Empty<VideoFile>();

                    return JsonSerializer.Deserialize<IEnumerable<VideoFile>>(cacheValue);
                }


            private string GetCacheKey(int productId)
            {
                return $"VideoFiles_{productId}";
            }
        }
    }