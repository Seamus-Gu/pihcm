using Consul;
using Framework.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Consul
{
    /// <summary>
    /// Provides extension methods for integrating Consul as a configuration source and registering a Consul client for
    /// dependency injection.
    /// </summary>
    /// <remarks>These extension methods enable applications to retrieve configuration values from a Consul
    /// key-value store and to register a Consul client for use with dependency injection. Ensure that the Consul
    /// service is accessible and properly configured before using these methods.</remarks>
    public static class ConsulExtension
    {
        /// <summary>
        /// Adds Consul as a configuration source to the specified configuration builder.
        /// </summary>
        /// <remarks>This method enables the application to retrieve configuration values from a Consul
        /// key-value store. It uses the application's name and environment to determine the configuration path. Ensure
        /// that the Consul service is accessible and properly configured before calling this method.</remarks>
        /// <param name="configuration">The configuration builder to which the Consul configuration source will be added. Cannot be null.</param>
        public static void AddConsulConfiguration(this IConfigurationBuilder configuration)
        {
            var appName = App.AppName;
            var envName = App.WebHostEnvironment.EnvironmentName;

            var consulConfig = App.GetConfig<ConsulConfig>(ConsulConstant.CONSUL);

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
        /// Registers a singleton Consul client with the specified service collection for dependency injection.
        /// </summary>
        /// <remarks>This method configures the Consul client using application configuration values for
        /// the Consul host and port. The registered client can be injected into dependent services via the
        /// IConsulClient interface.</remarks>
        /// <param name="service">The service collection to which the Consul client will be added. Cannot be null.</param>
        public static void AddConsulClient(this IServiceCollection service)
        {
            var consulConfig = App.GetConfig<ConsulConfig>(ConsulConstant.CONSUL);

            service.AddSingleton<IConsulClient>(sp =>
            {
                var url = $"{FrameworkConstant.HTTP}{consulConfig.Host}{DelimitersConstant.COLON}{consulConfig.Port}";

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