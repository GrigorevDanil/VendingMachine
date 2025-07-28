﻿public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class;
    
    Task<T> GetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? ttl = null, CancellationToken cancellationToken = default)
        where T : class;
    
    Task SetAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken cancellationToken = default)
        where T : class;
    
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    
    Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default);
}