using System.Net;

namespace Framework.Core
{
    /// <summary>
    /// 表示一个通用的操作结果，包含状态码和返回消息。
    /// </summary>
    /// <remarks>通常用于接口或方法的返回值，以统一表示操作的成功或失败状态。通过设置 Code 和 Message 属性，调用方可以根据状态码判断操作结果，并获取相关提示信息。默认状态码为
    /// 200，表示操作成功。</remarks>
    public class Result
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; } = 200;

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        public Result()
        {
        }

        public Result(Exception err)
        {
            Code = (int)HttpStatusCode.BadRequest;
            Message = err.Message;
        }

        public Result(CodeException ex)
        {
            this.Code = ex.Code;
            this.Message = ex.Message;
        }

        public Result(HttpException ex)
        {
            this.Code = ex.Code;
            this.Message = ex.Message;
        }
    }

    public class Result<T>
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
        public T? Data { get; set; }

        public static Result<List<string>> Error(int code, string message, List<string> data) =>
            new() { Code = code, Message = message, Data = data };
    }

}
