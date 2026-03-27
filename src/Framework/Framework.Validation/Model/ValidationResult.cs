namespace Framework.Validation
{
    /// <summary>
    /// 校验结果
    /// </summary>
    internal class ValidationResult
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
        public List<ValidationError> Data { get; set; } = new();
    }
}