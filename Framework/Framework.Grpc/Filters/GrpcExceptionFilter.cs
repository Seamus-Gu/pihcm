using Framework.Core;
using Grpc.Core;
using MagicOnion.Server;

namespace Framework.Grpc
{
    //请使用中文注释
    /// <summary>
    /// 用于捕获和处理 gRPC 服务中的异常，将异常信息以结构化格式返回给客户端的过滤器。
    /// </summary>
    /// <remarks>该过滤器可统一处理服务方法执行过程中抛出的异常，包括自定义的 CodeException 和其他未处理的异常。异常信息会被序列化后作为 gRPC Status
    /// 返回，便于客户端进行统一的错误处理。适用于需要标准化错误响应的 gRPC 服务场景。</remarks>
    public class GrpcExceptionFilter : MagicOnionFilterAttribute
    {
        public override async ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
        {
            try
            {
                await next(context);
            }
            catch (CodeException ex)
            {
                // 返回结构化错误给客户端
                context.CallContext.Status = new Status(
                    StatusCode.Unknown,
                    JsonUtil.Serialize(new Result(ex)));
            }
            catch (Exception err)
            {
                // 返回结构化错误给客户端
                context.CallContext.Status = new Status(
                    StatusCode.Unknown,
                    JsonUtil.Serialize(new Result(err)));
            }
        }
    }
}