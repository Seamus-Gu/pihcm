namespace Framework.Consul
{
    /// <summary>
    /// Consul配置项
    /// </summary>
    public class ConsulConfig
    {
        /// <summary>
        /// Consul 地址
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Consul 端口
        /// </summary>
        public int Port { get; set; } = 8500;

        #region 配置中心

        /// <summary>
        /// 轮询周期 (s) 默认10分钟
        /// </summary>
        public TimeSpan TimerCycle { get; set; } = TimeSpan.FromMinutes(5);

        #endregion 配置中心

        #region 服务发现

        /// <summary>
        /// 注册地址
        /// </summary>
        public string? RegistHost { get; set; }

        /// <summary>
        /// 注册端口
        /// </summary>
        public int RegistPort { get; set; } = 443;

        /// <summary>
        /// 健康检查间隔 (s) 默认15s
        /// </summary>
        public int HealthCheckInterval { get; set; } = 15;

        /// <summary>
        /// 服务标签,可用于分类/版本控制/环境/自定义标记
        /// </summary>
        public string[] ServerTags { get; set; } = Array.Empty<string>();

        /// <summary>
        /// 服务异常后自动注销时间 (s) 默认5s
        /// </summary>
        public int DeregisterCriticalServiceAfter { get; set; } = 5;

        /// <summary>
        /// 健康检查超时时间 (s) 默认5s
        /// </summary>
        public int TimeOut { get; set; } = 5;

        #endregion 服务发现
    }
}