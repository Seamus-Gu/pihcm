using Framework.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Claims;

namespace Framework.Security
{
    public class AuthenticationMiddleware : IMiddleware
    {
        private readonly SecurityOptions _options;

        public AuthenticationMiddleware(IOptions<SecurityOptions> options)
        {
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //if (UrlUtil.Match(context.Request.Path.Value, _options.Whites))
            //{
            //    await next(context);
            //    return;
            //}

            if (!TryExtractToken(context, out var token))
            {
                await context.Response.UpdateResponse(ErrorEnum.NoToken);
                return;
            }

            if (!TryValidateToken(token, out var principal))
            {
                await context.Response.UpdateResponse(ErrorEnum.TokenNotValidate);
                return;
            }

            context.User = principal;

            if (!IsWebClient(context))
            {
                await next(context);
                return;
            }

            var tokenId = context.User.FindFirst(ClaimTypes.Authentication)!.Value;
            var (status, expireTime) = await GetActivityStatusAsync(tokenId);

            if (status != null)
            {
                await context.Response.UpdateResponse(status.Value);
                return;
            }
            else
            {
                await SetLastActivityCache(tokenId, _options.ActivityTimeOut, expireTime);
            }

            ClaimToHeader(context);

            await next(context);
        }

        /// <summary>
        /// 获取Token字符串
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool TryExtractToken(HttpContext ctx, [NotNullWhen(true)] out string? token)
        {
            var auth = ctx.Request.Headers.Authorization.ToString();
            if (!string.IsNullOrEmpty(auth) && auth.StartsWith(AuthConstant.TOKEN_PREFIX))
            {
                token = auth[AuthConstant.TOKEN_PREFIX.Length..].Trim();
                return true;
            }

            token = null;
            return false;
        }

        /// <summary>
        /// 验证JWT\
        /// </summary>
        /// <param name="token"></param>
        /// <param name="principal"></param>
        /// <returns></returns>
        private bool TryValidateToken(string token, [NotNullWhen(true)] out ClaimsPrincipal? principal)
        {
            try
            {
                principal = TokenUtil.ValidateToken(token, _options.JwtSecurityKey);
                return true;
            }
            catch
            {
                principal = null;
                return false;
            }
        }

        /// <summary>
        /// 是否为Web应用
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private static bool IsWebClient(HttpContext ctx) =>
            string.Equals(ctx.User.FindFirstValue(ClaimTypes.System), DeviceEnum.Web.ToString(), StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 获取会话活动
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        private async Task<(ErrorEnum? status, TimeSpan? remaining)> GetActivityStatusAsync(string tokenId)
        {
            var key = $"{AuthConstant.LAST_ACTIVITY}{tokenId}";
            //var cache = await RedisCache.GetWithExpireAsync(key);

            //var value = cache.Value.ToString();
            //var remaining = cache.Expiry;

            //return value switch
            //{
            //    AuthConstant.KICKED_OFF => (ErrorEnum.KickedOut, remaining),
            //    AuthConstant.SUBSTITUTED => (ErrorEnum.Substituted, remaining),
            //    _ when DateTime.TryParse(value, out var last) =>
            //        DateTime.Now <= last ? (null, remaining) : (ErrorEnum.IdleTimeOut, remaining),
            //    _ => (ErrorEnum.IdleTimeOut, remaining)
            //};

            throw new Exception("");
        }

        /// <summary>
        /// 下游
        /// </summary>
        /// <param name="context"></param>
        private void ClaimToHeader(HttpContext context)
        {
            TryAddHeader(context, AuthConstant.HEADER_TOKEN, ClaimTypes.Authentication);
            TryAddHeader(context, AuthConstant.HEADER_USER_ID, ClaimTypes.NameIdentifier);
            TryAddHeader(context, AuthConstant.HEADER_USER_NAME, ClaimTypes.Name);
            TryAddHeader(context, AuthConstant.HEADER_SYSTEM, ClaimTypes.System);
        }

        /// <summary>
        /// 添加Header
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="headerName"></param>
        /// <param name="claimType"></param>
        private static void TryAddHeader(HttpContext ctx, string headerName, string claimType)
        {
            var value = ctx.User.FindFirst(claimType)?.Value;
            if (!string.IsNullOrWhiteSpace(value))
                ctx.Request.Headers.TryAdd(headerName, value);
        }

        /// <summary>
        /// 刷新last Activity
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="activityTimeOut"></param>
        private async Task SetLastActivityCache(
            string tokenId,
            int activityTimeOut,
            TimeSpan? expireTime)
        {
            var key = AuthConstant.LAST_ACTIVITY + tokenId;
            var val = DateTime.Now.AddMinutes(activityTimeOut).ToDateLongString();
            //await RedisCache.SetStringWithExpireAsync(key, val, expireTime);
        }
    }

    public static class HttpResponseExtension
    {
        /// <summary>
        /// 创建返回对象
        /// </summary>
        /// <param name="httpCode">Http 状态码</param>
        /// <param name="response">返回Body</param>
        /// <returns></returns>
        public static Task UpdateResponse(this HttpResponse response, ErrorEnum error, HttpStatusCode httpCode = HttpStatusCode.Unauthorized)
        {
            response.StatusCode = (int)httpCode;
            response.ContentType = "application/json";

            var errorJson = JsonUtil.Serialize(new Result
            {
                Code = error.ToInt(),
                //Message = AuthLocalization.GetString(error.GetName())
            });

            return response.WriteAsync(errorJson);
        }
    }
}