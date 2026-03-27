namespace Framework.Validation
{
    /// <summary>
    /// 校验错误
    /// </summary>
    internal class ValidationError
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Field { get; init; } = string.Empty;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; init; } = string.Empty;
    }
}