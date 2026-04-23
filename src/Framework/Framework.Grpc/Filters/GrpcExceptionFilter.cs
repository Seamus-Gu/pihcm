using Framework.Core;
using Grpc.Core;
using MagicOnion.Server;

namespace Framework.Grpc
{
    /// <summary>
    /// Provides a MagicOnion filter attribute that handles exceptions thrown during gRPC service invocation and returns
    /// structured error information to the client.
    /// </summary>
    /// <remarks>This filter captures both CodeException and general exceptions, serializing them into a
    /// structured response using JSON. It ensures that clients receive consistent error information in the gRPC status.
    /// Apply this filter to gRPC services to standardize error handling and reporting.</remarks>
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