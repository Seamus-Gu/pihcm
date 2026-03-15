using MagicOnion.Server;
using Microsoft.Extensions.DependencyInjection;

namespace Seed.Framework.Grpc
{
    public static class GrpcExtension
    {
        public static void AddGrpcService(this IServiceCollection services)
        {
            services.AddSingleton<GrpcChannelFactory>();
            services.AddSingleton(typeof(GrpcService<>));

            services.AddGrpc();
            services.AddMagicOnion(options =>
            {
                options.GlobalFilters.Add<GrpcExceptionFilter>();
                options.IsReturnExceptionStackTraceInErrorDetail = true; // ← 关键配置
            });
        }
    }
}