using Framework.Core;
using SqlSugar;

namespace Framework.WebApi
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IRepository
           where TEntity : class, new()
    {
        protected ITenant iTenant; // 多租户事务

        public BaseRepository(ISqlSugarClient context) : base(context)
        {
            iTenant = App.GetRequiredService<ISqlSugarClient>()!.AsTenant();

            // 若实体贴有多库特性，则返回指定的连接
            if (typeof(TEntity).IsDefined(typeof(TenantAttribute), false))
            {
                base.Context = iTenant.GetConnectionScopeWithAttr<TEntity>();
                return;
            }

            //// 若实体贴有系统表特性，则返回默认的连接
            //if (typeof(T).IsDefined(typeof(SystemTableAttribute), false)) return;

            //// 若当前未登录或是默认租户Id，则返回默认的连接
            //var tenantId = App.GetRequiredService<UserManager>().TenantId;
            //if (tenantId < 1 || tenantId.ToString() == SqlSugarConst.ConfigId) return;

            //// 根据租户Id切库
            //if (!iTenant.IsAnyConnection(tenantId.ToString()))
            //{
            //    var tenant = App.GetRequiredService<SysCacheService>().Get<List<SysTenant>>(CacheConst.KeyTenant)
            //        .FirstOrDefault(u => u.Id == tenantId);
            //    iTenant.AddConnection(new ConnectionConfig()
            //    {
            //        ConfigId = tenant.Id,
            //        DbType = tenant.DbType,
            //        ConnectionString = tenant.Connection,
            //        IsAutoCloseConnection = true
            //    });
            //    SqlSugarSetup.SetDbAop(iTenant.GetConnectionScope(tenantId.ToString()));
            //}
            //base.Context = iTenant.GetConnectionScope(tenantId.ToString());

            iTenant = context.AsTenant();
        }

        protected List<TEntity> PaginationList(ISugarQueryable<TEntity> queryable, Pagination pagination)
        {
            int pageTotal = 0;

            //if (!pagination.Sort.IsNullOrEmpty())
            //{
            //    var orderType = OrderByType.Asc;

            //    if (pagination.Order == "ascending")
            //    {
            //        orderType = OrderByType.Asc;
            //    }
            //    else if (pagination.Order == "descending")
            //    {
            //        orderType = OrderByType.Desc;
            //    }

            //    List<OrderByModel> orderList = OrderByModel.Create(
            //        new OrderByModel() { FieldName = UtilMethods.ToUnderLine(pagination.Sort), OrderByType = orderType }
            //    );

            //    queryable.OrderBy(orderList);
            //}

            var result = queryable.ToPageList(pagination.PageNum, pagination.PageSize, ref pageTotal);

            pagination.Total = pageTotal;

            return result.ToList();
        }

        //protected List<TEntity> PaginationList<T>(ISugarQueryable<TEntity> queryable, T queryDto) where T : Pagination
        //{
        //    int pageTotal = 0;

        //    if (!queryDto.Sort.IsNullOrEmpty())
        //    {
        //        var orderType = OrderByType.Asc;

        //        if (queryDto.Order == "asc")
        //        {
        //            orderType = OrderByType.Asc;
        //        }
        //        else if (queryDto.Order == "desc")
        //        {
        //            orderType = OrderByType.Desc;
        //        }

        //        List<OrderByModel> orderList = OrderByModel.Create(
        //            new OrderByModel() { FieldName = UtilMethods.ToUnderLine(queryDto.Sort), OrderByType = orderType }
        //        );

        //        queryable.OrderBy(orderList);
        //    }

        //    var result = queryable.ToPageList(queryDto.PageNum, queryDto.PageSize, ref pageTotal);

        //    queryDto.Total = pageTotal;

        //    return result.ToList();
        //}

        //protected async Task<List<TEntity>> PaginationListAsync<T>(ISugarQueryable<TEntity> queryable, T queryDto) where T : Pagination
        //{
        //    RefAsync<int> pageTotal = 0;

        //    if (!queryDto.Sort.IsNullOrEmpty())
        //    {
        //        var orderType = OrderByType.Asc;

        //        if (queryDto.Order == "ascending")
        //        {
        //            orderType = OrderByType.Asc;
        //        }
        //        else if (queryDto.Order == "descending")
        //        {
        //            orderType = OrderByType.Desc;
        //        }

        //        List<OrderByModel> orderList = OrderByModel.Create(
        //            new OrderByModel() { FieldName = UtilMethods.ToUnderLine(queryDto.Sort), OrderByType = orderType }
        //        );

        //        queryable.OrderBy(orderList);
        //    }

        //    var result = await queryable.ToPageListAsync(queryDto.PageNum, queryDto.PageSize, pageTotal);

        //    queryDto.Total = pageTotal.Value;

        //    return result.ToList();
        //}
    }
}