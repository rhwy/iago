using System;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Iago.Runner.Runtime
{
    public class MiniThrottler
    {
        private readonly MemoryCache _cache;
        private readonly int _defaultDuration;
        public MiniThrottler(int? defaultDuration = null)
        {
            _defaultDuration = defaultDuration ?? 50;
            _cache = new MemoryCache(new OptionsWrapper<MemoryCacheOptions>(new MemoryCacheOptions()));
        }

        public void Throttle<T>(string key, T value, Action<T> next, int duration = 50)
        {
            if (_cache.TryGetValue(key, out T entry)) return;
            
            var cacheEntryOptions = SetCacheEntryOptions(value, next, Math.Max(duration,_defaultDuration));
            _cache.Set<T>(key, value, cacheEntryOptions);
        }

        private static MemoryCacheEntryOptions SetCacheEntryOptions<T>(T value, Action<T> next, int duration)
        {
            var tokenSource = new CancellationTokenSource(duration);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMilliseconds(duration))
                .AddExpirationToken(new CancellationChangeToken(tokenSource.Token))
                .RegisterPostEvictionCallback((k, v, r, s) => next(value));
            return cacheEntryOptions;
        }
    }
}