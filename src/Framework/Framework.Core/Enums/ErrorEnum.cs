namespace Framework.Core
{
    /// <summary>
    /// 定义一组用于表示常见错误类型的枚举值。
    /// </summary>
    /// <remarks>该枚举用于标识系统中常见的错误状态，如参数缺失、认证失败、用户状态异常等。可用于统一错误处理和错误码管理，便于前后端或日志系统识别和处理不同类型的错误。
    /// 前2位 服务(Core/Auth/Payroll) 2位 模块(SysMenu/SysUser) 4位编号
    /// </remarks>
    public enum ErrorEnum
    {
        #region 启动项

        /// <summary>
        /// 表示未加载 Consul 的状态代码。
        /// </summary>
        /// <remarks>可用于指示系统在初始化或运行过程中未检测到 Consul 服务的情形。通常用于错误处理或状态判断。</remarks>
        NotLoadConfig = 10000000,

        /// <summary>
        /// 表示未加载 Consul 的状态代码。
        /// </summary>
        /// <remarks>可用于指示系统在初始化或运行过程中未检测到 Consul 服务的情形。通常用于错误处理或状态判断。</remarks>
        NotLoadConsul = 10000001,

        #endregion

        #region 通用异常

        /// <summary>
        /// 事务异常
        /// </summary>
        TranError = 10020001,
        #endregion

        /// <summary>
        /// 表示未登录用户的状态代码。
        /// </summary>
        /// <remarks>用于指示当前操作或请求未关联已登录用户。可用于权限校验、异常处理等场景。</remarks>
        NoLoginUser = 000003,

        /// <summary>
        /// 表示未提供令牌的状态代码。
        /// </summary>
        /// <remarks>通常用于指示请求缺少身份验证令牌时的错误状态。调用方应确保在需要时正确传递令牌以避免此状态。</remarks>
        NoToken = 010001,

        /// <summary>
        /// 表示令牌未通过验证的状态码。
        /// </summary>
        /// <remarks>当请求中的令牌无效、过期或未通过验证时，使用此状态码。调用方应检查令牌的有效性并根据需要重新获取或刷新令牌。</remarks>
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
        /// 用户禁用
        /// </summary>
        UserDisabled = 030002,
    }
}
