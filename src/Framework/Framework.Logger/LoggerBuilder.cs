using Framework.Core;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

namespace Framework.Logger
{
    internal static class LoggerBuilder
    {
        public static ILogger CreateLogger()
        {
            var logConfig = App.GetConfig<LogConfig>(FrameworkConstant.LOG_CONFIG);
            var loggerConfiguration = InitLoggerConfiguration();

            if (logConfig.File != null && logConfig.File.Enabled)
            {
                loggerConfiguration.AddFileLog(logConfig.File);
            }

            if (logConfig.Loki != null && logConfig.Loki.Enabled)
            {
                loggerConfiguration.AddLoki(logConfig.Loki);
            }

            return loggerConfiguration.CreateLogger();
        }

        private static LoggerConfiguration InitLoggerConfiguration()
        {
            var config = new LoggerConfiguration()
              .MinimumLevel.Debug() // 捕获的最小日志级别
              .WriteTo.Console();

            return config;
        }

        private static void AddFileLog(this LoggerConfiguration loggerConfiguration, LogFileConfig fileConfig)
        {
            loggerConfiguration
                .WriteTo.File(fileConfig.FilePath,
                    rollingInterval: fileConfig.RollingInterval,
                    outputTemplate: fileConfig.OutPutTemplate,
                    retainedFileCountLimit: fileConfig.MaxRollingFiles,
                    retainedFileTimeLimit: fileConfig.RetainedTimeLimit,
                    rollOnFileSizeLimit: fileConfig.FileSizeLimitBytes != 0,
                    fileSizeLimitBytes: fileConfig.FileSizeLimitBytes
                    );
        }

        private static void AddLoki(this LoggerConfiguration loggerConfiguration, LogLokiConfig lokiConfig)
        {
            var labels = new List<LokiLabel>
            {
                new LokiLabel
                {
                    Key = "service",
                    Value = App.AppName
                },  // 服务名（如 order-service）
                new LokiLabel
                {
                    Key = "instance",
                    Value = Environment.MachineName
                },
                new LokiLabel
                {
                    Key = "environment",
                    Value = Environment.MachineName
                }
            };


            loggerConfiguration
                .Enrich.FromLogContext()
                .WriteTo.GrafanaLoki(lokiConfig.Url, labels)
                .MinimumLevel.Information();

            // 针对特定命名空间调整日志级别（可选）
            //.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
        }

        //private static void AddElastic(this LoggerConfiguration loggerConfiguration, LogElasticSearchConfig elasticSearchConfig)
        //{
        //    loggerConfiguration
        //      //.Enrich.FromLogContext()
        //      //.Enrich.WithExceptionDetails()
        //      //.Enrich.WithMachineName()
        //      //.Enrich.WithSpan()
        //      //.Enrich.WithClientIp()
        //      //.Enrich.WithCorrelationId()
        //      .Enrich.FromLogContext()
        //      .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSearchConfig.Url))
        //      {
        //          AutoRegisterTemplate = true,
        //          //IndexFormat = $"{serviceInfo.AssemblyName}-{serviceInfo.EnvironmentName}-{{0:yyyy.MM.dd}}",
        //          ModifyConnectionSettings = t => t.BasicAuthentication(elasticSearchConfig.UserName, elasticSearchConfig.Password)
        //      })
        //      .Filter.ByExcluding(Matching.WithProperty<string>("RequestPath", x => x == "/api/health"));
        //}
    }
}