using System;
using System.Collections.Concurrent;

namespace ToDoList.Modules
{
    public delegate IRateLimiter RateLimiterFactory(int _burst, TimeSpan _recover);

    public class RateLimiterBucket : IRateLimiterBucket
    {
        private readonly ConcurrentDictionary<string, IRateLimiter> limiters;
        private readonly int burst;
        private readonly TimeSpan recover;
        private readonly RateLimiterFactory rateLimiterFactory;

        public RateLimiterBucket(
            int _burst,
            TimeSpan _recover,
            RateLimiterFactory _rateLimiterFactory)
        {
            limiters = new ConcurrentDictionary<string, IRateLimiter>();
            burst = _burst;
            recover = _recover;
            rateLimiterFactory = _rateLimiterFactory;
        }

        public IRateLimiter Get(string ident)
        {
            if (!limiters.TryGetValue(ident, out var limiter))
            {
                limiter = rateLimiterFactory(burst, recover);
                limiters.TryAdd(ident, limiter);
            }

            return limiter;
        }
    }
}
