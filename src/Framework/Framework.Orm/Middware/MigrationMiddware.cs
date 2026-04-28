using Framework.Core;
using Framework.Core.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Dynamic;
using System.Reflection;

namespace Framework.Orm
{
    public static class MigrationMiddware
    {
        private sealed record MigrationEntry(Migration Instance, string Name, string Path);

        public static void UseMigrationMiddleware(this IApplicationBuilder app)
        {
            if (app.ApplicationServices.GetService<ISqlSugarClient>() is not SqlSugarScope scope)
            {
                return;
            }

            var dbConfig = App.GetConfig<DBConfig>(OrmConstant.DB_CONFIG);
            if (!dbConfig.UseDBMigration)
            {
                return;
            }

            var migrationTypes = GetMigrationTypes();
            if (migrationTypes.Count == 0)
            {
                return;
            }

            var migrations = CreateMigrationEntries(migrationTypes);
            if (migrations.Count == 0)
            {
                return;
            }

            var system = App.GetConfig<SystemConfig>(FrameworkConstant.SYSTEM);

            if (system.IsRollback)
            {
                HandleRollback(system, scope, dbConfig.ConnectionConfigs, migrations);
            }
            else
            {
                HandleUp(system, scope, dbConfig.ConnectionConfigs, migrations);
            }
        }

        private static List<MigrationEntry> CreateMigrationEntries(List<Type> migrationTypes)
        {
            var result = new List<MigrationEntry>(migrationTypes.Count);

            foreach (var migrationType in migrationTypes)
            {
                if (Activator.CreateInstance(migrationType) is not Migration migration)
                {
                    continue;
                }

                result.Add(new MigrationEntry(
                    migration,
                    migration.Name,
                    migrationType.FullName ?? migrationType.Name));
            }

            return result;
        }

        private static void HandleRollback(
            SystemConfig system,
            SqlSugarScope scope,
            List<ConnectionConfig> list,
            List<MigrationEntry> migrations)
        {
            foreach (var conn in list)
            {
                var db = scope.GetConnectionScope(conn.ConfigId);
                db.CodeFirst.InitTables<MigrationHistory>();

                var rollbackNames = db.Queryable<MigrationHistory>()
                    .Where(x => x.Version == system.Version && x.Success)
                    .Select(x => x.MigrationName)
                    .ToList()
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                foreach (var migration in migrations)
                {
                    if (!rollbackNames.Contains(migration.Name))
                    {
                        continue;
                    }

                    ExecuteMigration(
                        db,
                        system,
                        migration,
                        migration.Instance.GetDownOperations(),
                        onSuccess: () =>
                        {
                            db.Deleteable<MigrationHistory>()
                                .Where(x => x.Version == system.Version && x.MigrationName == migration.Name)
                                .ExecuteCommand();
                        });
                }
            }
        }

        private static void HandleUp(
            SystemConfig system,
            SqlSugarScope scope,
            List<ConnectionConfig> list,
            List<MigrationEntry> migrations)
        {
            foreach (var conn in list)
            {
                var db = scope.GetConnectionScope(conn.ConfigId);
                db.CodeFirst.InitTables<MigrationHistory>();

                var appliedNames = db.Queryable<MigrationHistory>()
                    .Where(x => x.Success)
                    .Select(x => x.MigrationName)
                    .ToList()
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                foreach (var migration in migrations)
                {
                    if (appliedNames.Contains(migration.Name))
                    {
                        continue;
                    }

                    ExecuteMigration(
                        db,
                        system,
                        migration,
                        migration.Instance.GetUpOperations(),
                        onSuccess: () =>
                        {
                            db.Insertable(new MigrationHistory
                            {
                                Version = system.Version,
                                MigrationName = migration.Name,
                                Path = migration.Path,
                                Success = true,
                                CreatedAt = DateTime.Now
                            }).ExecuteCommand();

                            appliedNames.Add(migration.Name);
                        });
                }
            }
        }

        private static void ExecuteMigration(
            SqlSugarScopeProvider db,
            SystemConfig system,
            MigrationEntry migration,
            IEnumerable<MigrationOperation> operations,
            Action onSuccess)
        {
            try
            {
                db.Ado.BeginTran();

                foreach (var op in operations)
                {
                    ExecuteOperation(db, op);
                }

                onSuccess();
                db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                db.Ado.RollbackTran();

                db.Insertable(new MigrationHistory
                {
                    Version = system.Version,
                    MigrationName = migration.Name,
                    Path = migration.Path,
                    Success = false,
                    ErrorMessage = ex.ToString(),
                    CreatedAt = DateTime.Now
                }).ExecuteCommand();
            }
        }

        private static List<Type> GetMigrationTypes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName != null && a.FullName.Contains(FrameworkConstant.PREFIX));

            return assemblies
                .SelectMany(GetLoadableTypes)
                .Where(t => t.IsClass && !t.IsAbstract && typeof(Migration).IsAssignableFrom(t))
                .OrderBy(t => t.Name)
                .ToList();
        }

        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            try { return assembly.GetTypes(); }
            catch (ReflectionTypeLoadException ex) { return ex.Types.Where(t => t != null)!; }
        }

        private static void ExecuteOperation(SqlSugarScopeProvider db, MigrationOperation op)
        {
            switch (op.Action)
            {
                case MigrationActionEnum.CreateTable:
                    if (db.DbMaintenance.IsAnyTable(op.TableName))
                    {
                        return;
                    }

                    var typeBuilder = db.DynamicBuilder().CreateClass(op.TableName, new SugarTable());
                    foreach (var column in op.Columns)
                    {
                        typeBuilder.CreateProperty(column.Name, column.Type, column.ColumnOptions ?? new SugarColumn());
                    }

                    var type = typeBuilder.BuilderType();
                    db.CodeFirst.InitTables(type);
                    break;

                case MigrationActionEnum.DropTable:
                    if (db.DbMaintenance.IsAnyTable(op.TableName))
                    {
                        db.DbMaintenance.DropTable(op.TableName);
                    }
                    break;

                case MigrationActionEnum.AddColumn:
                    if (!db.DbMaintenance.IsAnyColumn(op.TableName, op.ColumnName))
                    {
                        db.DbMaintenance.AddColumn(op.TableName, op.ColumnInfo);
                    }
                    break;

                case MigrationActionEnum.AlterColumn:
                    if (db.DbMaintenance.IsAnyColumn(op.TableName, op.ColumnName))
                    {
                        db.DbMaintenance.UpdateColumn(op.TableName, op.ColumnInfo);
                    }
                    break;

                case MigrationActionEnum.DropColumn:
                    if (db.DbMaintenance.IsAnyColumn(op.TableName, op.ColumnName))
                    {
                        db.DbMaintenance.DropColumn(op.TableName, op.ColumnName);
                    }
                    break;
                case MigrationActionEnum.InsertData:
                    if (string.IsNullOrWhiteSpace(op.TableName))
                    {
                        break;
                    }

                    if (op.DataColumns.Count > 0 && op.DataValues.Count > 0)
                    {
                        foreach (var values in op.DataValues)
                        {
                            IDictionary<string, object?> row = new ExpandoObject();
                            for (var i = 0; i < op.DataColumns.Count; i++)
                            {
                                row[op.DataColumns[i]] = values[i];
                            }

                            db.InsertableByObject((object)row).AS(op.TableName).ExecuteCommand();
                        }

                        break;
                    }
                    break;

                default:
                    throw new NotSupportedException($"不支持的迁移动作: {op.Action}");
            }
        }
    }
}
