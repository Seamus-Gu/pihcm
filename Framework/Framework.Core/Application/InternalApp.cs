using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Framework.Core
{
    /// <summary>
    /// 提供用于初始化和配置应用程序内部依赖项的静态方法和字段。该类型用于管理应用程序的服务集合、配置和主机环境等核心对象。
    /// </summary>
    /// <remarks>此类型仅供框架内部使用，不建议在应用程序代码中直接访问。通过集中管理服务和环境信息，简化了应用程序的初始化流程。线程安全性需由调用方保证。</remarks>
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
