namespace Framework.Security
{
    /// <summary>
    /// 表示用于配置安全相关参数的设置集合，包括密码错误处理、令牌有效期、设备登录限制等。用于统一管理系统的安全策略。
    /// </summary>
    /// <remarks>该配置类适用于需要集中管理安全策略的应用场景，如身份认证、访问控制等。可根据实际业务需求调整各项参数以满足安全合规要求。</remarks>
    public class SecurityConfig
    {
        #region 密码错误

        /// <summary>
        /// 最大输入密码错误
        /// </summary>
        public int MaxRetryCount { get; set; } = 5;

        /// <summary>
        /// 密码错误锁定时间 (单位:s , 默认10分钟)
        /// </summary>
        public int LockTime { get; set; } = 10 * 60;

        #endregion 密码错误

        /// <summary>
        /// JWT 密匙
        /// </summary>
        public string JwtSecurityKey { get; set; } = "ma8v4HqAEMOFTWY6sF013isL6wYiczX1Na5mTQLdB3bKXpyRhRSn3iStepOUSvmI";

        /// <summary>
        /// Token 对应的 Redis 存储时间(单位:分钟 ,默认1天)
        /// </summary>
        public int TimeOut { get; set; } = 60 * 24;

        /// <summary>
        /// Token临时有效期 [指定时间内无操作就视为token过期] (单位: 分钟), 默认30分钟
        /// </summary>
        public int ActivityTimeOut { get; set; } = 30;

        /// <summary>
        /// 一个设备只允许一个账号登录
        /// </summary>
        public bool UseDeviceKey { get; set; } = false;

        /// <summary>
        /// 同一账号,Web端最大登录数量，-1代表不限
        /// </summary>
        public int MaxLoginCount { get; set; } = 5;

        public List<string>? WhiteList { get; set; }

        /// <summary>
        /// 前端私匙
        /// </summary>
        public string FrontPrivateKey { get; set; } = string.Empty;
    }
}