
namespace PIHCM.Gen.Repositories
{
    public class GenTableRepository : BaseRepository<GenTable>
    {

        public GenTableRepository(ISqlSugarClient context) : base(context)
        {
        }

        //    public List<string> GetTables()
        //    {
        //        using (var connection = new SqlConnection(_connectionString))
        //        {
        //            connection.Open();
        //            var tables = connection.GetSchema("Tables");
        //            return tables.AsEnumerable().Select(row => row["TABLE_NAME"].ToString()).ToList();
        //        }
        //    }

        //    public List<ColumnInfo> GetColumns(string tableName)
        //    {
        //        using (var connection = new SqlConnection(_connectionString))
        //        {
        //            connection.Open();
        //            var columns = connection.GetSchema("Columns", new[] { null, null, tableName });
        //            return columns.AsEnumerable().Select(row => new ColumnInfo
        //            {
        //                ColumnName = row["COLUMN_NAME"].ToString(),
        //                DataType = row["DATA_TYPE"].ToString()
        //            }).ToList();
        //        }
        //    }
        //}

        //public class ColumnInfo
        //{
        //    public string ColumnName { get; set; }
        //    public string DataType { get; set; }
        //}
    }
}