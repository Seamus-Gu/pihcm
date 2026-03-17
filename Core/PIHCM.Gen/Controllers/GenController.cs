
namespace PIHCM.Gen.Controllers
{
    /// <summary>
    /// 提供用于处理代码生成表相关操作的 API 控制器。
    /// </summary>
    /// <remarks>该控制器作为 API 端点，通常用于管理和操作代码生成相关的数据表。所有路由均以 "api/GenTable" 开头。继承自 BaseController，可能包含通用的 API
    /// 响应处理逻辑。</remarks>

    [Route("api/[controller]")]
    public class GenController : BaseController
    {
        [HttpGet("list")]
        public IActionResult Test()
        {
            return Success();
        }
    }
}
