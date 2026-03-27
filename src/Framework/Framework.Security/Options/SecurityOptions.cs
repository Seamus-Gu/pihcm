namespace Framework.Security
{
    public class SecurityOptions
    {
        /// <summary>
        /// 白名单
        /// </summary>
        public List<string> Whites { get; set; } = new();

        /// <summary>
        /// JWT密匙
        /// </summary>
        public string JwtSecurityKey { get; set; } = string.Empty;

        /// <summary>
        /// Token临时有效期 [指定时间内无操作就视为token过期] (单位: 分钟), 默认30分钟
        /// </summary>
        public int ActivityTimeOut { get; set; } = 30;
    }
}