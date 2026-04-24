using Microsoft.Extensions.DependencyInjection;

namespace Framework.MiniProfiler.Extensions
{
    public static class MiniProfilerExtension
    {
        public static void AddMiniProfilerService(this IServiceCollection services)
        {
            // Todo MiniProfiler
            //services.AddMiniProfiler(options =>
            //{
            //    options.RouteBasePath = "/profiler";
            //    //(options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);
            //    options.PopupRenderPosition = RenderPosition.Left;
            //    options.PopupShowTimeWithChildren = true;

            //    // 可以增加权限
            //    //options.ResultsAuthorize = request => request.HttpContext.User.IsInRole("Admin");
            //    //options.UserIdProvider = request => request.HttpContext.User.Identity.Name;
            //}
            //);
        }
    }
}
