using Framework.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Framework.Consul
{
    public static class DiscoveryExtension
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static void AddServiceDiscovery(this IServiceCollection services)
        {
            services.AddScoped<IServiceDiscovery, ServiceDiscover>();
            services.AddSingleton<IHostedService, DiscoveryHostedService>();
        }
    }
}