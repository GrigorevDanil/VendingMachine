using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
    
namespace VendingMachine.Infrastructure.Services;

public class CacheService : ICacheService
{
    private static ConcurrentDictionary<string, bool> CacheKeys = new();
    
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = await _distributedCache.GetStringAsync(key,cancellationToken);

        if (cachedValue is null) 
            return null;
            
        return JsonConvert.DeserializeObject<T>(cachedValue);
    }

    public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory,TimeSpan? ttl = null, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = await GetAsync<T>(key, cancellationToken);
        
        if (cachedValue is not null) 
            return cachedValue;
        
        cachedValue = await factory();
        
        await SetAsync(key, cachedValue, ttl, cancellationToken);
        
        return cachedValue;
    }

    public async Task SetAsync<T>(string key, T value,TimeSpan? ttl = null, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = JsonConvert.SerializeObject(value);

        var option = new DistributedCacheEntryOptions();
        
        if (ttl is not null) 
            option.SetAbsoluteExpiration(ttl.Value);
        
        await _distributedCache.SetStringAsync(key, cachedValue, option, cancellationToken);
        
        CacheKeys.TryAdd(key, true);    
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(key, cancellationToken);
        
        CacheKeys.TryRemove(key, out _);
    }
    
    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        var tasks = CacheKeys.Keys
            .Where(x => x.StartsWith(prefixKey))
            .Select(x => RemoveAsync(x, cancellationToken));
        
        await Task.WhenAll(tasks);
    }
}