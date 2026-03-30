namespace Framework.Validation
{
    /// <summary>
    /// 单个字段的校验失败明细。
    /// </summary>
    internal sealed class ValidationFailure
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { get; init; } = string.Empty;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; init; } = string.Empty;

        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; init; }
    }
}