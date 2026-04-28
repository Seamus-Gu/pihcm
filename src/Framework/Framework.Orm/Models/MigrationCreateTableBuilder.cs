using Framework.Core;
using SqlSugar;

namespace Framework.Orm
{
    public sealed class MigrationCreateTableBuilder
    {
        private readonly MigrationOperation _operation;

        internal MigrationCreateTableBuilder(MigrationOperation operation)
        {
            _operation = operation;
        }

        public MigrationCreateTableBuilder CreateId()
        {
            _operation.Columns.Add(new MigrationColumn
            {
                Name = $"_{_operation.TableName}{DelimitersConstant.UNDERSCORE}{SQLConstant.ID}",
                Type = typeof(long),
                ColumnOptions = new SugarColumn() { IsPrimaryKey = true, IsIdentity = true }
            });

            return this;
        }

        public MigrationCreateTableBuilder CreateProperty(string columnName, Type type, SugarColumn? column = null)
        {
            _operation.Columns.Add(new MigrationColumn
            {
                Name = columnName,
                Type = type,
                ColumnOptions = column
            });

            return this;
        }
    }
}
