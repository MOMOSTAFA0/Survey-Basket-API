
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Survey_Basket.Services
{
	public class CacheService(IDistributedCache distributedCache) : ICacheService
	{
		private readonly IDistributedCache _distributedCache = distributedCache;

		public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
		{
			var CachedVAlue = await _distributedCache.GetStringAsync(key, cancellationToken);
			return CachedVAlue is null ? null : JsonSerializer.Deserialize<T>(CachedVAlue);

		}
		public async Task SetAsync<T>(string key, T Value, CancellationToken cancellationToken = default) where T : class
		{
			await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(Value), cancellationToken);
		}

		public async Task RemoveAsync(string keym, CancellationToken cancellationToken = default)
		{
			await _distributedCache.RemoveAsync(keym, cancellationToken);
		}

	}
}
