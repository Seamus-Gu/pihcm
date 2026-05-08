using Framework.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Cache
{
    public static class CacheExtension
    {
        /// <summary>
        /// Adds memory and distributed caching services to the specified service collection, and optionally registers
        /// Redis-based caching.
        /// </summary>
        /// <remarks>When useRedis is set to true, a singleton implementation of ICache using Redis is
        /// registered. Otherwise, only in-memory and distributed memory caching are configured. This method is intended
        /// to be used during application startup to configure caching strategies.</remarks>
        /// <param name="services">The service collection to which the caching services will be added. Cannot be null.</param>
        /// <param name="useRedis">true to register Redis-based caching; otherwise, false. The default is true.</param>
        public static void AddCache(this IServiceCollection services, bool useRedis = true)
        {
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            if (useRedis)
            {
                services.AddSingleton<ICache, RedisCache>();
                return;
            }
        }
    }
}