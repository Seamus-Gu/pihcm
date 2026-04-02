using Autofac;
using Autofac.Extensions.DependencyInjection;
using Framework.Cache;
using Framework.Consul;
using Framework.Core;
using Framework.DI;
using Framework.IdGenerater;
using Framework.Logger;
using Framework.OpenApi;
using Framework.Orm;
using Framework.Validation;
using Framework.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Framework.Platform
{
    public static class PIHCMPlatformExtensions
    {
        /// <summary>
        /// 为 WebApplicationBuilder 实例添加 PIHCM 平台所需的核心服务和配置，包括控制器、JSON 选项、本地化、健康检查、服务发现、ID 生成等功能。
        /// </summary>
        /// <remarks>此方法应在应用程序启动时调用，以确保平台相关的服务和配置被正确注册。可通过 configureModules 参数扩展或自定义服务注册流程。部分服务如
        /// Consul、日志、ORM 等可根据实际需求进行启用或调整。</remarks>
        /// <param name="builder">要扩展的 WebApplicationBuilder 实例，作为应用程序服务和配置的注册入口。</param>
        /// <param name="configureModules">可选的自定义模块配置委托。用于在平台服务注册过程中，进一步配置服务集合和应用配置。可以为 null。</param>
        /// <returns>配置完成的 WebApplicationBuilder 实例，可用于后续的应用程序构建和启动。</returns>
        public static WebApplicationBuilder AddPIHCMPlatform(this WebApplicationBuilder builder, Action<IServiceCollection, IConfiguration>? configureModules = null)
        {
            return builder.AddPIHCMPlatform(_ => { }, configureModules);
        }

        public static WebApplicationBuilder AddPIHCMPlatform(this WebApplicationBuilder builder, Action<PIHCMPlatformOptions>? configureOptions, Action<IServiceCollection, IConfiguration>? configureModules = null)
        {
            var options = new PIHCMPlatformOptions();
            configureOptions?.Invoke(options);

            var configuration = builder.Configuration;
            var services = builder.Services;
            var host = builder.Host;

            builder.InitializeApp();

            configuration.AddConsulConfiguration();

            services.AddConsulClient();

            services.AddLog();

            services.AddHttpContextAccessor();

            services.AddCache();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
            {
                containerBuilder.InitAutofac();
            });

            if (options.EnableSqlSugar)
            {
                services.AddSqlSugar();
            }

            services
                .AddControllers(options =>
                {
                    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                    options.Filters.Add(new ValidationActionFilter());
                    options.Filters.Add(new GlobalExceptionFilter());
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new BooleanConverter()); // 布尔处理
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());//时间处理
                    options.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());//时间处理
                    options.JsonSerializerOptions.Converters.Add(new LongConverter());//long类型处理
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;//忽略循环引用
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;//处理乱码问题
                    options.JsonSerializerOptions.IncludeFields = true;//包含成员字段序列化
                    options.JsonSerializerOptions.AllowTrailingCommas = true;//允许尾随逗号
                    options.JsonSerializerOptions.WriteIndented = true;//是否应使用整齐打印
                    options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;//允许注释
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;//不区分大小写
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;//驼峰
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;//驼峰
                }); ;

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            services.AddLocalization();

            if (App.IsDevelop)
            {
                builder.Services.AddFrameworkOpenApi();
            }

            services.AddHealthChecks();

            services.AddServiceDiscovery();

            configureModules?.Invoke(services, configuration);

            //
            ////builder.Services.AddMiniProfiler();

            services.AddIdGenerater();

            //builder.Services.AddGrpcService();

            return builder;
        }

        /// <summary>
        /// 配置并启用 PIHCM 平台的核心中间件和路由。应在应用程序启动时调用，以确保平台功能正常运行。
        /// </summary>
        /// <remarks>此方法应在应用程序启动配置期间调用，通常位于 Startup.cs 的 Configure 方法中。调用后将自动启用 HTTPS
        /// 重定向、路由和控制器终结点映射。请确保在注册自定义中间件或终结点前调用本方法以保证平台兼容性。</remarks>
        /// <param name="app">要配置的应用程序构建器实例。用于注册中间件和终结点。</param>
        public static void UsePIHCMPlatform(this IApplicationBuilder app)
        {
            app.ConfigureApp();

            app.UseHttpsRedirection();

            app.UseLocalization();

            app.UseRouting();

            //if (App.IsDevelop)
            //{
            //    //app.UseFrameworkSwagger();
            //}

            //app.UseAuthorization();

            //app.UseMiniProfiler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks(FrameworkConstant.HEALTH_ROUTE);
                endpoints.MapControllers();

                if (App.IsDevelop)
                {
                    endpoints.MapOpenApi();
                }
                //endpoints.MapMagicOnionService();
            });
        }
    }

}
