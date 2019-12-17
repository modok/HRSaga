using System;
using System.Threading.Tasks;
using CQRSlite.Domain;
#if NET452
using System.Runtime.Caching;
#else
using Microsoft.Extensions.Caching.Memory;
#endif

namespace CQRSlite.Caching
{
    /// <summary>
    /// A cache implementation that has cache in memory and 15 minutes sliding expiration.
    /// </summary>
    public class MemoryCache : ICache
    {
#if NET452
        private readonly System.Runtime.Caching.MemoryCache _cache;
        private Func<CacheItemPolicy> _policyFactory;
#else
        private readonly MemoryCacheEntryOptions _cacheOptions;
        private readonly IMemoryCache _cache;
#endif

        public MemoryCache()
        {

#if NET452
            _cache = System.Runtime.Caching.MemoryCache.Default;
            _policyFactory = () => new CacheItemPolicy {
                SlidingExpiration = TimeSpan.FromMinutes(15)
            };
#else
            _cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(15)
            };
            _cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
#endif

        }

        public Task<bool> IsTracked(Identity identity)
        {
#if NET452
            return Task.FromResult(_cache.Contains(identity.ToString()));
#else
            return Task.FromResult(_cache.TryGetValue(identity, out var o) && o != null);
#endif
        }

        public Task Set(Identity identity, AggregateRoot aggregate)
        {
#if NET452
            _cache.Add(identity.ToString(), aggregate, _policyFactory.Invoke());
#else
            _cache.Set(identity, aggregate, _cacheOptions);
#endif
            return Task.FromResult(0);
        }

        public Task<AggregateRoot> Get(Identity identity)
        {
#if NET452
            return Task.FromResult((AggregateRoot)_cache.Get(identity.ToString()));
#else
            return Task.FromResult((AggregateRoot) _cache.Get(identity));
#endif
        }

        public Task Remove(Identity identity)
        {
#if NET452
            _cache.Remove(identity.ToString());
#else
            _cache.Remove(identity);
#endif
            return Task.FromResult(0);
        }

        public void RegisterEvictionCallback(Action<Identity> action)
        {
#if NET452
            _policyFactory = () => new CacheItemPolicy
            {
                SlidingExpiration = TimeSpan.FromMinutes(15),
                RemovedCallback = x =>
                {
                    action.Invoke(Identity.Parse(x.CacheItem.Key));
                }
            };
#else
            _cacheOptions.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                action.Invoke((Identity) key);
            });
#endif
        }
    }
}
