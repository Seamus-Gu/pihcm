namespace PIHCM.Gen.Services
{
    /// <summary>
    /// 业务处理层
    /// </summary>
    public class GenColumnService : IGenColumnService, IScopeService
    {
        private readonly GenColumnRepository _genColumnRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public GenColumnService(GenColumnRepository genColumnRepository)
        {
            _genColumnRepository = genColumnRepository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<GenColumn>> GetGenColumnPageList(GenColumnQueryDto query)
        {
            var result = await _genColumnRepository.SelectGenColumnPageList(query);

            return result;
        }

        /// <summary>
        /// 根据Id获取详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GenColumn> GetGenColumnById(long id)
        {
            var result = await _genColumnRepository.GetByIdAsync(id);

            return result;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddGenColumn(GenColumn entity)
        {
            var result = await _genColumnRepository.InsertAsync(entity);

            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> EditGenColumn(GenColumn entity)
        {
            var result = await _genColumnRepository.UpdateAsync(entity);

            return result;
        }

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveGenColumnById(long id)
        {
            var result = await _genColumnRepository.DeleteByIdAsync(id);

            return result;
        }
    }
}
