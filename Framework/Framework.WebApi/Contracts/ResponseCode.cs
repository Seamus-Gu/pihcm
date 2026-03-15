namespace Framework.WebApi
{
    /// <summary>
    /// 定义标准的响应代码和消息常量，用于表示操作的成功或失败状态。
    /// </summary>
    /// <remarks>该类提供统一的响应码和消息，便于在应用程序中标准化接口返回值。常用于接口开发或服务端响应，帮助调用方根据响应码判断操作结果。</remarks>
    public class ResponseCode
    {
        /// <summary>
        /// 表示操作成功时的标准状态码（200）。
        /// </summary>
        public const int SUCCESS = 200;

        /// <summary>
        /// 表示通用的错误状态码 400，通常用于指示请求无效或参数错误。
        /// </summary>
        /// <remarks>该常量可用于统一处理接口请求中的错误响应，便于与其他状态码进行区分。</remarks>
        public const int ERROR = 400;

        /// <summary>
        /// 表示操作成功时的标准消息文本。用于指示操作已成功完成。
        /// </summary>

        public const string SUCCESS_MESSAGE = "Success";

        /// <summary>
        /// 表示通用的错误消息常量。
        /// </summary>
        /// <remarks>可用于统一错误提示信息，避免硬编码字符串。</remarks>
        public const string ERROR_MESSAGE = "Error";
    }
}
