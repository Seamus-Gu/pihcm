namespace PIHCM.Gen.Interfaces
{
    /// <summary>
    /// 代码生成列业务层接口
    /// </summary>
    public interface IGenColumnService
    {
        /// <summary>
        /// 获取代码生成列分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<List<GenColumn>> GetGenColumnPageList(GenColumnQueryDto query);

        /// <summary>
        /// 根据Id获取代码生成列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<GenColumn> GetGenColumnById(long id);

        /// <summary>
        /// 新增代码生成列
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> AddGenColumn(GenColumn entity);

        /// <summary>
        /// 修改代码生成列
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> EditGenColumn(GenColumn entity);

        /// <summary>
        /// 删除代码生成列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> RemoveGenColumnById(long id);
    }
}
