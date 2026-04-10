namespace PIHCM.Gen.Controllers
{
    /// <summary>
    /// 提供用于处理代码生成表相关操作的 API 控制器。
    /// </summary>
    /// <remarks>该控制器作为 API 端点，通常用于管理和操作代码生成相关的数据表。所有路由均以 "api/GenTable" 开头。继承自 BaseController，可能包含通用的 API
    /// 响应处理逻辑。</remarks>

    [Route("v1/[controller]")]
    public class GenTableController : BaseController
    {
        private readonly IGenTableService _genTableService;

        public GenTableController(IGenTableService genTableService)
        {
            _genTableService = genTableService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPageList([FromQuery] GenTableQueryDto query)
        {
            var result = await _genTableService.GetPageList(query);

            return Success(new
            {
                items = result,
                total = query.Total
            });
        }

        [HttpGet("generate-code")]
        public async Task<IActionResult> GenerateCode([FromQuery] long tableId)
        {
            var result = await _genTableService.GenerateCode(tableId);

            return Success(result);
        }

        [HttpPost("export-code")]
        public async Task<IActionResult> ExportCode([FromBody] DownloadDto dto)
        {
            var result = await _genTableService.ExportCode(dto.TableId);

            return File(result, "application/octet-stream", "test.zip");
        }
    }
}
