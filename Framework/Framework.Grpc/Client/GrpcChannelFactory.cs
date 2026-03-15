using Framework.Core;
using Grpc.Net.Client;
using System.Collections.Concurrent;

namespace Framework.Grpc
{
    public class GrpcChannelFactory
    {
        private readonly IServiceDiscovery _serviceDicovery;
        private static readonly object _lock = new object();
        private readonly ConcurrentDictionary<string, GrpcChannel> _grpcServers = new ConcurrentDictionary<string, GrpcChannel>();

        public GrpcChannelFactory(IServiceDiscovery serviceDicovery)
        {
            _serviceDicovery = serviceDicovery;
        }

        public async Task<GrpcChannel> Get(string serverName)
        {
            if (!_grpcServers.ContainsKey(serverName))
            {
                var handler = new HttpClientHandler();

                handler.ServerCertificateCustomValidationCallback =
                       HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                var services = await _serviceDicovery.GetServices();

                if (!services.ContainsKey(serverName))
                {
                    //throw new Exception(string.Format(FrameworkI18N.NotConnect, serverName));
                }

                lock (_lock)
                {
                    var channel = GrpcChannel.ForAddress(services[serverName], new GrpcChannelOptions { HttpHandler = handler });

                    _grpcServers.GetOrAdd(serverName, channel);
                }
            }

            return _grpcServers[serverName];
        }
    }
}