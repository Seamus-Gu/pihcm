using Microsoft.Extensions.DependencyInjection;

namespace Framework.Core
{
    public static class CorsExtension
    {
        /// <summary>
        /// Adds CORS (Cross-Origin Resource Sharing) services to the specified service collection.
        /// </summary>
        /// <remarks>Call this method during application startup to enable CORS support. After adding CORS
        /// services, configure CORS policies as needed before building the application.</remarks>
        /// <param name="services">The service collection to which the CORS services will be added. Cannot be null.</param>
        public static void AddCorsService(this IServiceCollection services)
        {
            // Todo 跨域配置
            services.AddCors(c =>
            {
                //if (options.EnableAll)
                //{
                //    //允许任意跨域请求
                //    c.AddPolicy(options.Name,
                //        policy =>
                //        {
                //            policy
                //                .SetIsOriginAllowed(host => true)
                //                .AllowAnyMethod()
                //                .AllowAnyHeader()
                //                .AllowCredentials();
                //        });
                //}
                //else
                //{
                //    c.AddPolicy(options.Name,
                //        policy =>
                //        {
                //            policy
                //                .WithOrigins(options.Policy.Select(x => x.Domain).ToArray())
                //                .AllowAnyHeader()
                //                .AllowAnyMethod();
                //        });
                //}
            });
        }
    }
}
