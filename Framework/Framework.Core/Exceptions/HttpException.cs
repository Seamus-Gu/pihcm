using System.Net;

namespace Framework.Core
{
    /// <summary>
    /// 表示在处理 HTTP 请求时发生的异常，包含相关的 HTTP 状态码和自定义状态码信息。
    /// </summary>
    /// <remarks>通常用于在 Web 应用程序中封装 HTTP 错误响应。可通过设置 <see cref="HttpCode"/> 和 <see cref="Code"/>
    /// 属性，向调用方传递详细的错误信息。适用于需要将业务错误与 HTTP 协议错误区分的场景。</remarks>
    public class HttpException : Exception
    {
        /// Http状态码
        /// </summary>
        public HttpStatusCode HttpCode { get; set; }

        /// <summary>
        /// 通用状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public HttpException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
