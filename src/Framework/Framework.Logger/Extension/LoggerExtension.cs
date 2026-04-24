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
        /// Configures the application's logging by initializing the global logger instance for use with the specified
        /// service collection.
        /// </summary>
        /// <remarks>Call this method during application startup to enable logging throughout the
        /// application. This method sets up a global logger instance that can be used by other components.</remarks>
        /// <param name="services">The service collection to which the logging configuration will be applied. Cannot be null.</param>
        public static void AddLog(this IServiceCollection services)
        {
            Log.Logger = LoggerBuilder.CreateLogger();
        }
    }
}