namespace PIHCM.Gen.Controller
{
    /// <summary>
    /// 代码生成列接口层
    /// </summary>
    [ApiRoute("[controller]")]
    public class GenColumnController : BaseController
    {
        private readonly IGenColumnService _genColumnService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public GenColumnController(IGenColumnService genColumnService)
        {
            _genColumnService = genColumnService;
        }

        /// <summary>
        /// 获取代码生成列分页列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetGenColumnPageList([FromQuery] GenColumnQueryDto query)
        {
            var result = await _genColumnService.GetGenColumnPageList(query);

            return Success(new
            {
                items = result,
                total = query.Total
            });
        }

        /// <summary>
        /// 获取代码生成列详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenColumnById(long id)
        {
            var result = await _genColumnService.GetGenColumnById(id);

            return Success(result);
        }

        /// <summary>
        /// 新增代码生成列
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddGenColumn([FromBody] GenColumn entity)
        {
            var result = await _genColumnService.AddGenColumn(entity);

            return BoolResult(result);
        }

        /// <summary>
        /// 修改代码生成列
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("edit")]
        public async Task<IActionResult> EditGenColumn([FromBody] GenColumn entity)
        {
            var result = await _genColumnService.EditGenColumn(entity);

            return BoolResult(result);
        }

        /// <summary>
        /// 删除代码生成列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveGenColumnById(long id)
        {
            var result = await _genColumnService.RemoveGenColumnById(id);

            return BoolResult(result);
        }
    }
}
