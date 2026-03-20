using Ocelot.Configuration.File;

var builder = WebApplication.CreateBuilder(args);

builder.InitializeApp();

var configuration = builder.Configuration;
var services = builder.Services;

configuration.AddConsulConfiguration();

services.AddCors(option =>
{
    option.AddPolicy("cors", policy =>
    {
        //Todo 网关配置跨域
        policy.AllowAnyHeader() // 允许所有请求头
              .AllowAnyMethod()  // 允许所有请求方法
              .AllowCredentials()  // 允许Cookie信息
              .AllowAnyOrigin(); // 允许所有站点跨域请求
                                 //.WithOrigins(origins); // 允许部分站点跨域请求
    });
});

//services.AddCache();
services.AddControllers();

services.AddEndpointsApiExplorer();

services.AddLocalization();

services.PostConfigure<FileConfiguration>(options =>
{
    var consulConfig = App.GetConfig<ConsulConfig>(FrameworkConstant.CONSUL);

    options.GlobalConfiguration.ServiceDiscoveryProvider.Host = consulConfig.Host;
    options.GlobalConfiguration.ServiceDiscoveryProvider.Port = consulConfig.Port;
});

services
    .AddOcelot()
    .AddPolly()
    .AddConsul<CustomConsulServiceBuilder>();
//.AddSwaggerForOcelot(Configuration)
//.AddConfigStoredInConsul();

//services.Configure<SecurityOptions>(builder.Configuration.GetSection(FrameworkConstant.SECURITY));
//services.AddScoped<AuthenticationMiddleware>()


//builder.Services.AddOpenApi();

var app = builder.Build();


//if (app.Environment.IsDevelopment())
//{
//    var ocelotConfig = App.GetOptions<FileConfiguration>();
//    var swaggers = ocelotConfig.Routes
//        .Select(t => new SwaggerRouteDto
//        {
//            ServiceName = t.ServiceName,
//            UpstreamPathTemplate = t.UpstreamPathTemplate
//        }).ToList();

//    app.UseGatewaySwaggerUI(swaggers);
//}

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}


//app.UseLocalization();
//app.UseRouting();

//app.UseMiddleware<AuthenticationMiddleware>();
//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting()
   .UseOcelot().Wait();

//app.UseLocalization();
//app.UseRouting();
//app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();