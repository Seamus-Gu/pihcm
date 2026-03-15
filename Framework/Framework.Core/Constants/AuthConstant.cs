namespace Framework.Core
{
    /// <summary>
    /// 提供与身份认证和授权相关的常量字段，用于标准化令牌、请求头、用户状态等在认证流程中的标识符。
    /// </summary>
    /// <remarks>这些常量可用于实现基于令牌的身份认证机制，包括请求头的设置、令牌前缀、用户状态标识及相关缓存键等。通过统一常量，有助于减少硬编码错误并提升代码可维护性。</remarks>
    public class AuthConstant
    {
        /// <summary>
        /// 权鉴 Hearder
        /// </summary>
        public const string HEADER = "Authorization";

        /// <summary>
        /// Token 前缀
        /// </summary>
        public const string TOKEN_PREFIX = "Bearer";

        /// <summary>
        /// 登录用户密码错误
        /// </summary>
        public const string PASSWORD_ERROR = "auth:password-error:";

        /// <summary>
        ///
        /// </summary>
        public const string ONLINE_TOKEN = "auth:online-token:";

        /// <summary>
        /// 最后一次登录时间(时限为token 长久有效期),
        /// null为token失效
        /// -1 为被踢出
        /// -2 为被顶下线
        /// </summary>
        public const string LAST_ACTIVITY = "auth:last-activity:";

        /// <summary>
        ///  用戶信息
        /// </summary>
        public const string USER_INFO = "auth:user-info:";

        /// <summary>
        /// Token List
        /// </summary>
        public const string TOKENS = "auth:tokens:";

        /// <summary>
        /// 邮箱验证码
        /// </summary>
        public const string EMAIL_CODE = "email-code:";

        /// <summary>
        /// 被踢下线
        /// </summary>
        public const string KICKED_OFF = "-1";

        /// <summary>
        /// 被顶下线
        /// </summary>
        public const string SUBSTITUTED = "-2";

        /// <summary>
        /// 首次密码错误
        /// </summary>
        public const string FIRST_PASSWORD_ERROR = "1";
    }
}