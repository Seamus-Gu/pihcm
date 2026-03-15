using Microsoft.AspNetCore.Builder;

namespace Framework.Core
{
    public static class ApplicationExtension
    {
        /// <summary>
        /// 初始化内部App对象
        /// </summary>
        /// <param name="builder"></param>
        public static void InitApp(this WebApplicationBuilder builder)
        {
            InternalApp.InitInternalApp(builder);
        }

        /// <summary>
        /// 配置内部App
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureApp(this IApplicationBuilder builder)
        {
            InternalApp.ConfigureInternalApp(builder);
        }
    }
}
