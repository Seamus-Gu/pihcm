using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Framework.Logger
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class LoggerExtension
    {
        /// <summary>
        /// 配置Serilog日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static void AddLog(this IServiceCollection services)
        {
            Log.Logger = LoggerBuilder.CreateLogger();
        }
    }
}