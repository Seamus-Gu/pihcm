using Framework.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Cache
{
    public static class CacheExtension
    {
        /// <summary>
        /// 注册内存,分布式内存,Redis
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static void AddCache(this IServiceCollection services, bool useRedis = true)
        {
            // 内存缓存
            services.AddMemoryCache();
            // 分布式缓存
            services.AddDistributedMemoryCache();

            if (useRedis)
            {
                services.AddSingleton<ICache, RedisCache>();
                return;
            }
        }
    }
}