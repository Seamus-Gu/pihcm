namespace Framework.Core.Responses
{
    /// <summary>
    /// 表示一个包含 HTTP 状态码、业务状态码和消息的通用 HTTP 响应结果。
    /// </summary>
    /// <remarks>通常用于封装 Web API 的响应结果，便于客户端统一处理 HTTP 状态和业务状态。适用于需要同时传递 HTTP 协议状态和自定义业务状态的场景。</remarks>
    public class HttpResult
    {
        /// <summary>
        /// Http 状态码
        /// </summary>
        public int HttpCode { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// 表示一个通用的 HTTP 响应结果，包含状态码、消息和数据内容。
    /// </summary>
    /// <remarks>通常用于封装 Web API 的响应结果，便于统一处理 HTTP 状态码、业务状态码和返回数据。适用于需要标准化接口返回结构的场景。</remarks>
    /// <typeparam name="T">响应中包含的数据类型。</typeparam>
    public class HttpResult<T>
    {
        /// <summary>
        /// Http 状态码
        /// </summary>
        public int HttpCode { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 返回数据
        /// </summary>
        public T? Data { get; set; }
    }

}
