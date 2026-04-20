namespace PIHCM.Gen.Repositories
{
    public class GenColumnRepository : BaseRepository<GenColumn>
    {

        public GenColumnRepository(ISqlSugarClient context) : base(context)
        {
        }

        public Task<List<GenColumn>> SelectListByTableId(long tableId)
        {
            var q = AsQueryable()
                .Where(g => g.TableId == tableId);

            return q.ToListAsync();
        }

        /// <summary>
        /// 获取代码生成列分页列表
        /// </summary>
        public Task<List<GenColumn>> SelectGenColumnPageList(GenColumnQueryDto query)
        {
            var q = AsQueryable()
                .WhereIF(query.TableId != null, x => x.TableId == query.TableId);

            return PaginationListAsync(q, query);
        }
    }
}