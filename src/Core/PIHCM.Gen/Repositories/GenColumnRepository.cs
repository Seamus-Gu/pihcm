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
    }
}