using Framework.Core;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Net;

namespace Framework.WebApi
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        /// <summary>
        /// 异步异常处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                return Task.CompletedTask;
            }

            if (context.Exception is RpcException rpc)
            {
                context.Result = CreateContentResult(rpc.Status.Detail);
            }
            else if (context.Exception is HttpException httpEx)
            {
                var result = new Result(httpEx);

                context.Result = CreateContentResult(result, httpEx.HttpCode);
            }
            else if (context.Exception is CodeException codeEx)
            {
                var result = new Result(codeEx);

                context.Result = CreateContentResult(result);
            }
            else
            {
                var result = new Result(context.Exception);

                context.Result = CreateContentResult(result, HttpStatusCode.InternalServerError);
            }

            var errorMessage = string.Format("Message:{0} Source:{1} StackTrace:{2}", context.Exception.Message, context.Exception.Source, context.Exception.StackTrace);
            Log.Error(errorMessage);

            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 创建返回对象
        /// </summary>
        /// <param name="httpCode">Http 状态码</param>
        /// <param name="response">返回Body</param>
        /// <returns></returns>
        private static ContentResult CreateContentResult(string response, HttpStatusCode httpCode = HttpStatusCode.OK)
        {
            return new ContentResult
            {
                StatusCode = (int)httpCode,
                ContentType = "application/json;charset=utf-8",
                Content = response
            };
        }

        /// <summary>
        /// 创建返回对象
        /// </summary>
        /// <param name="httpCode">Http 状态码</param>
        /// <param name="response">返回Body</param>
        /// <returns></returns>
        private static ContentResult CreateContentResult(Result response, HttpStatusCode httpCode = HttpStatusCode.OK)
        {
            return new ContentResult
            {
                StatusCode = (int)httpCode,
                ContentType = "application/json;charset=utf-8",
                Content = JsonUtil.Serialize(response)
            };
        }
    }
}