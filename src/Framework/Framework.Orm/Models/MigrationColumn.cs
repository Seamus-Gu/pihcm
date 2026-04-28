using SqlSugar;

namespace Framework.Orm
{
    internal class MigrationColumn
    {
        public required string Name { get; init; }

        public required Type Type { get; init; }

        public SugarColumn? ColumnOptions { get; init; }
    }
}
