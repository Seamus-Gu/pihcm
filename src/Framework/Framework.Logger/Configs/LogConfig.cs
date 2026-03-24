using Framework.Core;
using Serilog;

namespace Framework.Logger
{
    public class LogConfig
    {
        public LogFileConfig? File { get; set; }

        public LogLokiConfig? Loki { get; set; }
    }

    public class LogFileConfig
    {
        /// <summary>
        /// 是否开启
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 文件目录
        /// </summary>
        public string FilePath { get; set; } = FrameworkConstant.LOGGING_FILE_PATH;

        /// <summary>
        /// 文件类别(分支,小时,天)
        /// </summary>
        public RollingInterval RollingInterval { get; set; } = RollingInterval.Day;

        /// <summary>
        /// 输出模板
        /// </summary>
        public string OutPutTemplate { get; set; } = FrameworkConstant.LOG_OUT_PUT;

        /// <summary>
        /// 控制最大创建的日志文件数量
        /// </summary>
        public int MaxRollingFiles { get; set; } = 60;

        /// <summary>
        /// 日志保存时间
        /// </summary>
        public TimeSpan RetainedTimeLimit { get; set; } = TimeSpan.FromDays(7);

        /// <summary>
        /// 控制每一个日志文件最大存储大小
        /// 为0时不限制单个文件最大大小
        /// </summary>
        public long FileSizeLimitBytes { get; set; } = 5242880;
    }

    public class LogDatabaseConfig
    {
        /// <summary>
        /// 是否开启
        /// </summary>
        public bool Enabled { get; set; } = true;
    }

    public class LogLokiConfig
    {
        /// <summary>
        /// 是否开启
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }

    internal class LogMonitorConfig
    {
    }
}