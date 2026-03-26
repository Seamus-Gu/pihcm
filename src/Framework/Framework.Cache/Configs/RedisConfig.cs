namespace Framework.Cache
{
    public class RedisConfig
    {
        /// <summary>
        /// Redis 地址
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Redis 端口
        /// </summary>
        public string Port { get; set; } = string.Empty;

        /// <summary>
        /// Redis密碼
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}