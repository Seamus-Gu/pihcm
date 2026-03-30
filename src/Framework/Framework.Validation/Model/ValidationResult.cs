namespace Framework.Validation
{
    /// <summary>
    /// 统一校验响应模型。
    /// </summary>
    internal sealed class ValidationResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; } = 200;

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 返回数据
        /// </summary>
        public List<ValidationFailure> Data { get; set; } = new();
    }
}