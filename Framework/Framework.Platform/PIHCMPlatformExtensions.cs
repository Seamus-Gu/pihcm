using Framework.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Platform
{
    public static class PIHCMPlatformExtensions
    {
        /// <summary>
        /// 初始化项目
        /// </summary>
        /// <param name="builder">ApplicationBuild</param>
        /// <param name="action">启动配置(委托)</param>
        /// <returns></returns>
        public static WebApplicationBuilder AddPIHCMPlatform(this WebApplicationBuilder builder, Action<IServiceCollection, IConfiguration>? configureModules = null)
        {
            var services = builder.Services;
            var config = builder.Configuration;

            builder.InitApp();

            //// 配置中心
            //builder.Configuration.AddConsulConfiguration();

            //// Consul 客户端
            //builder.Services.AddConsulClient();

            //// 日志
            //builder.Services.AddLog();

            //// 添加HttpContext
            //builder.Services.AddHttpContextAccessor();

            //// 缓存注册
            //builder.Services.AddCache(opeions.UseRedis);

            //// DI
            //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            //{
            //    builder.InitAutofac();
            //});

            //// Orm
            //builder.Services.AddSqlSugar(opeions.UseDatebase);

            //builder.Services
            //    .AddControllers(options =>
            //    {
            //        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            //        options.Filters.Add(new GlobalActionFilter());
            //        options.Filters.Add(new GlobalExceptionFilter());
            //    })
            //    .AddJsonOptions(options =>
            //    {
            //        options.JsonSerializerOptions.Converters.Add(new BooleanConverter()); // 布尔处理
            //        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());//时间处理
            //        options.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());//时间处理
            //        options.JsonSerializerOptions.Converters.Add(new LongConverter());//long类型处理
            //        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;//忽略循环引用
            //        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;//处理乱码问题
            //        options.JsonSerializerOptions.IncludeFields = true;//包含成员字段序列化
            //        options.JsonSerializerOptions.AllowTrailingCommas = true;//允许尾随逗号
            //        options.JsonSerializerOptions.WriteIndented = true;//是否应使用整齐打印
            //        options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;//允许注释
            //        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;//不区分大小写
            //        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;//驼峰
            //        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;//驼峰
            //    }); ;

            //// 关闭自动验证
            //builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            //// 国际化处理
            //builder.Services.AddLocalization();

            //if (App.IsDevelop)
            //{
            //    builder.Services.AddSwagger();
            //}

            //// 健康监测
            //builder.Services.AddHealthChecks();

            //// 服务发现
            //builder.Services.AddServiceDiscovery();

            ////
            //////builder.Services.AddMiniProfiler();

            //// 雪花Id
            //builder.Services.AddIdGenerater();

            //builder.Services.AddGrpcService();

            return builder;
        }

        //public static void InitConfigure(this IApplicationBuilder app)
        //{
        //    app.ConfigureApp();

        //    app.UseHttpsRedirection();

        //    app.UseLocalization();
        //    app.UseRouting();

        //    if (App.IsDevelop)
        //    {
        //        app.UseFrameworkSwagger();
        //    }

        //    app.UseAuthorization();

        //    //app.UseMiniProfiler();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapHealthChecks(FrameworkConstant.HEALTH_ROUTE);
        //        endpoints.MapControllers();
        //        endpoints.MapMagicOnionService();
        //    });
        //}
    }
}
