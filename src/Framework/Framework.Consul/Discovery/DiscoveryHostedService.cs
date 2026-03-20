using Consul;
using Framework.Core;
using Microsoft.Extensions.Hosting;

namespace Framework.Consul
{
    internal class DiscoveryHostedService : IHostedService
    {
        private readonly ConsulConfig _consulConfig;
        private readonly IConsulClient _consulClient;

        public DiscoveryHostedService()
        {
            _consulConfig = App.GetConfig<ConsulConfig>(FrameworkConstant.CONSUL);

            _consulClient = new ConsulClient(config =>
            {
                var url = FrameworkConstant.HTTP + _consulConfig.Host + DelimitersConstant.COLON + _consulConfig.Port;
                config.Address = new Uri(url);
                //config.Datacenter = "dc1";
            });
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var id = FrameworkConstant.HTTPS + _consulConfig.RegistHost + DelimitersConstant.COLON + _consulConfig.RegistPort;
            var heaalthPath = id + FrameworkConstant.HEALTH_ROUTE;

            var registration = new AgentServiceRegistration
            {
                ID = id,
                Name = App.AppName,
                Address = _consulConfig.RegistHost,
                Port = _consulConfig.RegistPort,
                //Tags = new string[] { "api site" },
                Check = new AgentServiceCheck()
                {
                    TLSSkipVerify = true,
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(_consulConfig.DeregisterCriticalServiceAfter),
                    Interval = TimeSpan.FromSeconds(_consulConfig.HealthCheckInterval),
                    HTTP = heaalthPath,
                    Timeout = TimeSpan.FromSeconds(_consulConfig.TimeOut),
                }
            };

            await _consulClient.Agent.ServiceDeregister(App.AppName, cancellationToken);
            await _consulClient.Agent.ServiceRegister(registration, cancellationToken);

            //todo 应用终止,注销服务
            //app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            //lifetime.ApplicationStopping.Register(() =>
            //{
            //    client.Agent.ServiceDeregister(serviceId).Wait();
            //});
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _consulClient.Agent.ServiceDeregister(App.AppName, cancellationToken);
        }
    }
}