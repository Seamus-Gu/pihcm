using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Framework.Core
{
    internal class InternalApp
    {
        /// <summary>
        /// 应用服务
        /// </summary>
        internal static IServiceCollection? _internalServices;

        /// <summary>
        /// 根服务
        /// </summary>
        internal static IServiceProvider? _rootServices;

        /// <summary>
        /// 配置对象
        /// </summary>
        internal static IConfiguration? _configuration;

        /// <summary>
        /// 获取Web主机环境
        /// </summary>
        internal static IWebHostEnvironment? _webHostEnvironment;

        /// <summary>
        /// 获取泛型主机环境
        /// </summary>
        internal static IHostEnvironment? _hostEnvironment;

        private InternalApp()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        internal static void InitInternalApp(WebApplicationBuilder builder)
        {
            _configuration = builder.Configuration;
            _internalServices = builder.Services;
            _hostEnvironment = _webHostEnvironment = builder.Environment;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        internal static void ConfigureInternalApp(IApplicationBuilder app)
        {
            _rootServices = app.ApplicationServices;
        }
    }
}
