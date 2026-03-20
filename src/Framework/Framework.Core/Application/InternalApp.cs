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
        /// 应用程序名称
        /// </summary>
        internal static string _applicationName = string.Empty;

        /// <summary>
        /// 是否处于开发环境
        /// </summary>
        internal static bool _isDevelop = false;

        /// <summary>
        /// 应用服务
        /// </summary>
        internal static IServiceCollection _internalServices = default!;

        /// <summary>
        /// 根服务
        /// </summary>
        internal static IServiceProvider _rootServices = default!;

        /// <summary>
        /// 配置对象
        /// </summary>
        internal static IConfiguration _configuration = default!;

        /// <summary>
        /// 获取Web主机环境
        /// </summary>
        internal static IWebHostEnvironment _webHostEnvironment = default!;

        /// <summary>
        /// 获取泛型主机环境
        /// </summary>
        internal static IHostEnvironment _hostEnvironment = default!;

        private InternalApp()
        {
        }

        /// <summary>
        /// 从 WebApplicationBuilder 初始化内部应用程序静态状态（配置、服务和环境）。
        /// </summary>
        /// <param name="builder">WebApplicationBuilder 实例。</param>
        internal static void InitializeFromBuilder(WebApplicationBuilder builder)
        {
            _applicationName = builder.Environment.ApplicationName;
            _isDevelop = builder.Environment.IsDevelopment();

            _configuration = builder.Configuration;
            _internalServices = builder.Services;
            _hostEnvironment = _webHostEnvironment = builder.Environment;
        }

        /// <summary>
        /// 将应用的根服务引用保存到静态字段。
        /// </summary>
        /// <param name="app">应用程序构建器。</param>
        internal static void ConfigureInternalApp(IApplicationBuilder app)
        {
            _rootServices = app.ApplicationServices;
        }
    }
}
