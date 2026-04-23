namespace PIHCM.Core.Repositories
{
    /// <summary>
    /// 用户信息表仓储层
    /// </summary>
    public class SysUserRepository : BaseRepository<SysUser>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysUserRepository(ISqlSugarClient context) : base(context)
        {
        }

        /// <summary>
        /// Asynchronously retrieves a user entity that matches the specified user name.
        /// </summary>
        /// <param name="userName">The user name to search for. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user entity that matches the
        /// specified user name, or null if no such user exists.</returns>
        public Task<SysUser> SelectByUserName(string userName)
        {
            return GetFirstAsync(u => u.UserName == userName);
        }

        /// <summary>
        /// 获取用户信息表分页列表
        /// </summary>
        public Task<List<SysUser>> SelectSysUserPageList(Pagination query)
        {
            var q = AsQueryable();

            return PaginationListAsync(q, query);
        }
    }
}
