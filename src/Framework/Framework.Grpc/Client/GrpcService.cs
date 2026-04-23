using Framework.Core;
using MagicOnion.Client;

namespace Framework.Grpc
{
    public class GrpcService<TService> where TService : IServiceClient<TService>
    {
        private readonly TService _proxy;
        private readonly GrpcChannelFactory _grpcChannelFactory;

        public TService Service => _proxy;

        public GrpcService(GrpcChannelFactory grpcChannelFactory)
        {
            _grpcChannelFactory = grpcChannelFactory;

            string lastSegment = typeof(TService).Assembly.GetName().Name!
                     .Split(DelimitersConstant.DOT)
                     .LastOrDefault() ?? string.Empty;

            var serviceChannel = _grpcChannelFactory.Get(FrameworkConstant.FRAMEWORK_PREFIX + DelimitersConstant.DOT + lastSegment).Result;
            _proxy = MagicOnionClient.Create<TService>(serviceChannel);
        }
    }
}