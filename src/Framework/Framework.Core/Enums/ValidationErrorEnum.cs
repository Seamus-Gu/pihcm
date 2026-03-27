namespace Framework.Core
{
    /// <summary>
    /// 指示验证错误的类型。
    /// 00 核心 00 校验
    /// </summary>
    public enum ValidationErrorEnum
    {
        /// <summary>
        /// 指示该成员为必需项。
        /// </summary>
        /// <remarks>在使用此枚举值时，调用方应确保相关字段或属性已被正确赋值，否则可能导致验证失败或运行时异常。</remarks>
        Required = 00000001,
    }
}
