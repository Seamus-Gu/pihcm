using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Framework.Core
{
    public static class App
    {
        /// <summary>
        /// 应用名
        /// </summary>
        public static string AppName = InternalApp._applicationName;

        /// <summary>
        /// 是否开发环境
        /// </summary>
        public static bool IsDevelop = InternalApp._isDevelop;

        /// <summary>
        /// 全局配置选项
        /// </summary>
        public static IConfiguration Configuration => InternalApp._configuration!;

        /// <summary>
        /// 获取Web主机环境，如，是否是开发环境，生产环境等
        /// </summary>
        public static IWebHostEnvironment WebHostEnvironment => InternalApp._webHostEnvironment!;

        /// <summary>
        /// 获取泛型主机环境，如，是否是开发环境，生产环境等
        /// </summary>
        public static IHostEnvironment HostEnvironment => InternalApp._hostEnvironment!;

        /// <summary>
        /// 存储根服务，可能为空
        /// </summary>
        public static IServiceProvider RootServices => InternalApp._rootServices!;

        /// <summary>
        /// 获取请求上下文
        /// </summary>
        public static HttpContext HttpContext => RootServices.GetService<IHttpContextAccessor>()?.HttpContext!;

        /// <summary>
        /// 缓存
        /// </summary>
        public static ICache Cache => GetService<ICache>();

        /// <summary>
        /// 当前用户
        /// </summary>
        public static ICurrentUser CurrentUser => GetService<ICurrentUser>();

        /// <summary>
        /// 解析服务提供器
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider(Type serviceType)
        {
            var isSingleton = InternalApp._internalServices!
                .Where(u => u.ServiceType == (serviceType.IsGenericType ? serviceType.GetGenericTypeDefinition() : serviceType))
                .Any(u => u.Lifetime == ServiceLifetime.Singleton);

            // 第一选择，判断是否是单例注册且单例服务不为空，如果是直接返回根服务提供器
            if (RootServices != null && isSingleton)
            {
                return RootServices;
            }

            // 第二选择是获取 HttpContext 对象的 RequestServices
            var httpContext = HttpContext;
            if (httpContext?.RequestServices != null)
            {
                return httpContext.RequestServices;
            }

            if (RootServices != null) // 第三选择，创建新的作用域并返回服务提供器
            {
                var scoped = RootServices.CreateScope();
                return scoped.ServiceProvider;
            }
            // 第四选择，构建新的服务对象（性能最差）
            else
            {
                var serviceProvider = InternalApp._internalServices.BuildServiceProvider();
                return serviceProvider;
            }
        }

        /// <summary>
        /// 获取请求生存周期的服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TService GetService<TService>(IServiceProvider serviceProvider = default!)
            where TService : class
        {
            var service = GetService(typeof(TService), serviceProvider) as TService;

            return service!;
        }

        /// <summary>
        /// 获取请求生存周期的服务
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static object? GetService(Type type, IServiceProvider serviceProvider)
        {
            return (serviceProvider ?? GetServiceProvider(type)).GetService(type);
        }

        /// <summary>
        /// 获取请求生存周期的服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TService? GetRequiredService<TService>(IServiceProvider serviceProvider = default!)
            where TService : class
        {
            return GetRequiredService(typeof(TService), serviceProvider) as TService;
        }

        /// <summary>
        /// 获取请求生存周期的服务
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static object GetRequiredService(Type type, IServiceProvider serviceProvider = default!)
        {
            return (serviceProvider ?? GetServiceProvider(type)).GetRequiredService(type);
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <typeparam name="TConfig">强类型选项类</typeparam>
        /// <param name="path">配置中对应的Key</param>
        /// <returns>TOptions</returns>
        public static TConfig GetConfig<TConfig>(string path)
        {
            var options = Configuration.GetSection(path).Get<TConfig>();

            if (options == null)
            {
                var message = string.Format(FrameworkResource.NotLoadConfig, path);
                throw new CodeException(ErrorEnum.NotLoadConsul.ToInt(), message);
            }

            return options;
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns>TOptions</returns>
        public static TOptions GetOptions<TOptions>(IServiceProvider serviceProvider = default!)
            where TOptions : class, new()
        {
            return GetService<IOptions<TOptions>>(serviceProvider ?? RootServices)?.Value!;
        }
    }
}
