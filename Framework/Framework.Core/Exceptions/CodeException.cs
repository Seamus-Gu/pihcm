namespace Framework.Core
{
    /// <summary>
    /// 表示包含错误代码的异常类型，用于在抛出异常时携带自定义的错误编号。
    /// </summary>
    /// <remarks>可用于需要根据错误代码进行异常处理或错误追踪的场景。通过 Code 属性可获取或设置与异常关联的错误编号。</remarks>
    public class CodeException : Exception
    {
        /// <summary>
        /// Code编号
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public CodeException(int code, string? message) : base(message)
        {
            Code = code;
        }
    }
}
