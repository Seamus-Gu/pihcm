namespace Framework.Core.Enums
{
    /// <summary>
    /// Specifies error codes that represent various error conditions within the application.
    /// </summary>
    /// <remarks>Use this enumeration to identify and handle specific error scenarios in a type-safe manner.
    /// The exact set of error codes is defined by the members of this enumeration.</remarks>
    public enum ErrorEnum
    {
        /// <summary>
        /// 必填
        /// </summary>
        Required = 000001,

        /// <summary>
        /// 无法获取Consul配置项
        /// </summary>
        NotLoadConsul = 000002,

        /// <summary>
        /// 无法获取登录用户
        /// </summary>
        NoLoginUser = 000003,

        /// <summary>
        /// 没有Token
        /// </summary>
        NoToken = 010001,

        /// <summary>
        /// Token 未通过校验
        /// </summary>
        TokenNotValidate = 010002,

        /// <summary>
        /// 会话超时
        /// </summary>
        IdleTimeOut = 010003,

        /// <summary>
        /// 强制下线
        /// </summary>
        KickedOut = 010004,

        /// <summary>
        /// 登录挤下线
        /// </summary>
        Substituted = 010005,

        /// <summary>
        /// 密码错误重试限制
        /// </summary>
        PasswordRetryLimitExceed = 020001,

        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordError = 020002,

        /// <summary>
        /// 用户不存在
        /// </summary>
        UserNotExist = 030001,

        /// <summary>
        /// 用户禁用
        /// </summary>
        UserDisabled = 030002,
    }
}
