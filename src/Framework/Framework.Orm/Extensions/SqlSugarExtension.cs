using Framework.Core;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Reflection;

namespace Framework.Orm
{
    public static class SqlSugarExtension
    {
        public static void AddSqlSugar(this IServiceCollection services)
        {
            var dbConfig = App.GetConfig<DBConfig>(OrmConstant.DB_CONFIG);

            dbConfig.ConnectionConfigs.ForEach(SetDbConfig);

            SqlSugarScope sqlSugar = new(dbConfig.ConnectionConfigs, db =>
            {
                dbConfig.ConnectionConfigs.ForEach(config =>
                {
                    var dbProvider = db.GetConnectionScope(config.ConfigId);
                    SetDbAop(dbProvider);
                    SetDbDiffLog(dbProvider, config);
                });
            });

            // 单例注册
            services.AddSingleton<ISqlSugarClient>(sqlSugar);
        }

        /// <summary>
        /// 配置数据库连接
        /// </summary>
        /// <param name="config"></param>
        private static void SetDbConfig(ConnectionConfig config)
        {
            var configureExternalServices = new ConfigureExternalServices
            {
                // 类(表) 处理
                EntityNameService = (type, entity) =>
                {
                    entity.DbTableName = UtilMethods.ToUnderLine(entity.DbTableName); // 驼峰转下划线
                },
                // 属性 列处理
                EntityService = (property, column) =>
                {
                    if (new NullabilityInfoContext().Create(property).WriteState is NullabilityState.Nullable)
                        column.IsNullable = true;

                    if (column.DbColumnName == "Id")
                    {
                        column.DbColumnName = $"{column.DbTableName}_id";
                        return;
                    }

                    column.DbColumnName = UtilMethods.ToUnderLine(column.DbColumnName); // 驼峰转下划线
                },
                DataInfoCacheService = new SqlSugarCache(),
            };

            config.ConfigureExternalServices = configureExternalServices;
            config.InitKeyType = InitKeyType.Attribute;
            config.IsAutoCloseConnection = true;
            config.MoreSettings = new ConnMoreSettings
            {
                IsAutoRemoveDataCache = true,
                IsAutoDeleteQueryFilter = true, // 启用删除查询过滤器
                IsAutoUpdateQueryFilter = true, // 启用更新查询过滤器
                SqlServerCodeFirstNvarchar = true // 采用Nvarchar
            };
        }

        /// <summary>
        /// 配置Aop
        /// </summary>
        /// <param name="db"></param>
        public static void SetDbAop(SqlSugarScopeProvider db)
        {
            var config = db.CurrentConnectionConfig;

            // 设置超时时间
            db.Ado.CommandTimeOut = 30;

            // Todo 打印SQL语句
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //Log.Information($"SQL:{sql} , Params: {pars}");
                // Todo 打印到MiniProfiler
            };
            db.Aop.OnError = ex =>
            {
                //Log.Error($"SQL:{ex.Sql} , Params: {ex.Parametres}");

                // Todo 打印到MiniProfiler
            };

            // Todo 数据事务
            db.Aop.DataExecuting = (oldValue, entityInfo) =>
            {
                if (entityInfo.OperationType == DataFilterType.InsertByObject)
                {
                    //if (entityInfo.PropertyName == "CreateBy")
                    //{
                    //    var createBy = ((dynamic)entityInfo.EntityValue).CreateBy;
                    //    if (createBy == "" || createBy == null)
                    //        entityInfo.SetValue(LoginHelper.GetUserName());
                    //}

                    //可配置 租户id处理
                }
                if (entityInfo.OperationType == DataFilterType.UpdateByObject)
                {
                    if (entityInfo.PropertyName == "UpdateTime")
                    {
                        entityInfo.SetValue(DateTime.Now);
                    }

                    //if (entityInfo.PropertyName == "UpdateBy")
                    //{
                    //    entityInfo.SetValue(LoginHelper.GetUserName());
                    //}
                }
            };

            // Todo 配置实体假删除过滤器
            // Todo 配置租户过滤器
            // Todo 配置用户机构（数据范围）过滤器
            // Todo 配置自定义过滤器
        }

        /// <summary>
        /// Todo 开启库表差异化日志 ORM审计
        /// </summary>
        /// <param name="db"></param>
        /// <param name="config"></param>
        private static void SetDbDiffLog(SqlSugarScopeProvider db, ConnectionConfig config)
        {
            db.Aop.OnDiffLogEvent = u =>
            {
                // 操作前记录（字段描述、列名、值、表名、表描述）
                var b = u.BeforeData;
                // 操作后记录（字段描述、列名、值、表名、表描述）
                var a = u.AfterData;
                // 传进来的对象
                var c = u.BeforeData;
                // insert、update、delete
                var d = u.DiffType.ToString();
                // sql 语句
                var sql = UtilMethods.GetSqlString(config.DbType, u.Sql, u.Parameters);
                // 用时
                var elapsed = u.Time == null ? 0 : (long)u.Time.Value.TotalMilliseconds;

                // todo await 插入mongDB/数据库
            };
        }
    }
}