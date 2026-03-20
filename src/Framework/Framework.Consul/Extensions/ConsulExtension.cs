using Consul;
using Framework.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Consul
{
    /// <summary>
    /// Consul 扩展
    /// </summary>
    public static class ConsulExtension
    {
        /// <summary>
        /// 使用 Consul 动态加载应用程序配置。
        /// </summary>
        /// <remarks>此方法允许应用程序通过 Consul 实现集中式配置管理。添加后，配置更改可在 Consul
        /// 中动态生效，无需重启应用。适用于需要分布式配置中心的场景。</remarks>
        /// <param name="configuration">要扩展的配置生成器。用于注册 Consul 配置源，不能为 null。</param>
        public static void AddConsulConfiguration(this IConfigurationBuilder configuration)
        {
            var appName = App.AppName;
            var envName = App.WebHostEnvironment.EnvironmentName;

            var consulConfig = App.GetConfig<ConsulConfig>(FrameworkConstant.CONSUL);

            var consulClient = new ConsulClient(client =>
            {
                var url = $"{FrameworkConstant.HTTP}{consulConfig.Host}{DelimitersConstant.COLON}{consulConfig.Port}";
                client.Address = new Uri(url);

                // TODO: Consul身份验证
                // client.Token = consulConfig.Token;
            });

            var consulConfigSource = new ConsulConfigSource(consulClient, consulConfig.TimerCycle, appName, envName);

            configuration.Add(consulConfigSource);
        }

        /// <summary>
        /// 向依赖注入容器中注册 Consul 客户端服务，以便应用程序可以通过 IConsulClient 接口访问 Consul。
        /// </summary>
        /// <remarks>此方法将 Consul 客户端以单例方式注册，便于在应用程序中统一管理 Consul 连接。调用前需确保已正确配置 Consul 的主机和端口信息。</remarks>
        /// <param name="service">要向其添加 Consul 客户端服务的 IServiceCollection 实例。不能为空。</param>
        public static void AddConsulClient(this IServiceCollection service)
        {
            var consulConfig = App.GetConfig<ConsulConfig>(FrameworkConstant.CONSUL);

            service.AddSingleton<IConsulClient>(sp =>
            {
                var url = FrameworkConstant.HTTP + consulConfig.Host + DelimitersConstant.COLON + consulConfig.Port;

                return new ConsulClient(client =>
                {
                    client.Address = new Uri(url);

                    // TODO: Consul身份验证
                    // client.Token = consulConfig.Token;
                });
            });
        }
    }
}