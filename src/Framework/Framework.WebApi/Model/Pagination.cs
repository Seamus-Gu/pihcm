namespace Framework.WebApi
{
    public class Pagination
    {
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageNum { get; set; } = 1;

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public string? Order { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string? Sort { get; set; }
    }
}