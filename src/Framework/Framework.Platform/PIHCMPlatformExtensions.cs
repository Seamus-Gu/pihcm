using Asp.Versioning;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Framework.Cache;
using Framework.Consul;
using Framework.Core;
using Framework.DI;
using Framework.Grpc;
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

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
            {
                containerBuilder.InitAutofac();
            });

            configuration.AddConsulConfiguration();

            services.AddIdGenerater();
            services.AddHealthChecks();
            services.AddConsulClient();
            services.AddServiceDiscovery();
            services.AddHttpContextAccessor();
            services.AddLocalization();
            services.AddLog();
            services.AddCache(options.EnableRedisCache);
            if (options.EnableSqlSugar)
            {
                services.AddSqlSugar();
            }
            if (App.IsDevelop)
            {
                builder.Services.AddFrameworkOpenApi();
            }
            services.AddCorsService();
            //services.AddMiniProfilerService();
            services.AddGrpcService();
            services
                .AddControllers(options =>
                {
                    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                    options.Filters.Add(new ValidationActionFilter());
                    options.Filters.Add(new GlobalExceptionFilter());
                    //Todo 操作日志过滤器
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

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";        // 版本格式: v1, v2
                options.SubstituteApiVersionInUrl = true;  // 替换 URL 中的版本参数
            });

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            return builder;
        }

        /// <summary>
        /// Configures the PIHCM platform middleware and endpoints for the application.
        /// </summary>
        /// <remarks>This method sets up essential middleware components such as HTTPS redirection,
        /// localization, routing, health checks, and controller endpoints. In development environments, it also enables
        /// OpenAPI endpoints. Call this method during application startup to ensure the PIHCM platform is properly
        /// initialized.</remarks>
        /// <param name="app">The application builder used to configure the request pipeline.</param>
        public static void UsePIHCMPlatform(this IApplicationBuilder app)
        {
            app.ConfigureApp();

            app.UseHttpsRedirection();
            app.UseLocalization();
            app.UseRouting();
            //Todo 跨域 性能监控
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
                endpoints.MapMagicOnionService();
            });
        }
    }

}
