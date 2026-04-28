using SqlSugar;

namespace Framework.Orm
{
    internal sealed class MigrationOperation
    {
        /// <summary>
        /// Gets the migration action to be performed.
        /// </summary>
        public MigrationActionEnum Action { get; init; }

        /// <summary>
        /// Gets the name of the database table associated with this instance.
        /// </summary>
        public string? TableName { get; init; }

        /// <summary>
        /// Gets the collection of columns included in the migration operation.
        /// </summary>
        /// <remarks>The returned list contains the definitions of all columns affected by this migration.
        /// The collection is read-only; to modify the columns, use the appropriate migration builder methods.</remarks>
        public List<MigrationColumn> Columns { get; } = [];

        /// <summary>
        /// Gets the name of the column associated with this instance.
        /// </summary>
        public string? ColumnName { get; init; }

        /// <summary>
        /// Gets the database column metadata associated with this instance.
        /// </summary>
        public DbColumnInfo? ColumnInfo { get; init; }

        /// <summary>
        /// Gets the list of data column names associated with the current instance.
        /// </summary>
        public List<string> DataColumns { get; } = [];

        /// <summary>
        /// Gets the collection of data values for each row in the result set.
        /// </summary>
        /// <remarks>Each element in the collection represents a row, with the array containing the values
        /// for each column in that row. The order of values in each array corresponds to the order of columns in the
        /// result set.</remarks>
        public List<object?[]> DataValues { get; } = [];

        /// <summary>
        /// Creates a migration operation that represents the creation of a new table with the specified name.
        /// </summary>
        /// <param name="tableName">The name of the table to be created. Cannot be null or empty.</param>
        /// <returns>A MigrationOperation configured to create the specified table.</returns>
        public static MigrationOperation CreateTable(string tableName)
            => new() { Action = MigrationActionEnum.CreateTable, TableName = tableName };

        /// <summary>
        /// Creates a migration operation that drops the specified table from the database schema.
        /// </summary>
        /// <param name="tableName">The name of the table to be dropped. Cannot be null or empty.</param>
        /// <returns>A MigrationOperation representing the drop table action for the specified table.</returns>
        public static MigrationOperation DropTable(string tableName)
                   => new() { Action = MigrationActionEnum.DropTable, TableName = tableName };

        /// <summary>
        /// Creates a migration operation that adds a new column to the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table to which the column will be added. Cannot be null or empty.</param>
        /// <param name="columnInfo">An object that describes the column to add, including its name, type, and other metadata. Cannot be null.</param>
        /// <returns>A MigrationOperation representing the action to add the specified column to the table.</returns>
        public static MigrationOperation AddColumn(string tableName, DbColumnInfo columnInfo)
            => new()
            {
                Action = MigrationActionEnum.AddColumn,
                TableName = tableName,
                ColumnName = columnInfo.DbColumnName,
                ColumnInfo = columnInfo,
            };

        /// <summary>
        /// Creates a migration operation that drops a column from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table from which the column will be dropped. Cannot be null or empty.</param>
        /// <param name="columnName">The name of the column to drop. Cannot be null or empty.</param>
        /// <returns>A MigrationOperation representing the drop column action for the specified table and column.</returns>
        public static MigrationOperation DropColumn(string tableName, string columnName)
            => new()
            {
                Action = MigrationActionEnum.DropColumn,
                TableName = tableName,
                ColumnName = columnName
            };

        /// <summary>
        /// Creates a migration operation that alters the definition of an existing column in a database table.
        /// </summary>
        /// <param name="tableName">The name of the table containing the column to alter. Cannot be null or empty.</param>
        /// <param name="columnName">The name of the column to alter. Cannot be null or empty.</param>
        /// <param name="dbColumnInfo">An object describing the new definition of the column, including type, constraints, and other metadata.
        /// Cannot be null.</param>
        /// <returns>A MigrationOperation representing the alter column action for use in a database migration.</returns>
        public static MigrationOperation AlterColumn(string tableName, string columnName, DbColumnInfo dbColumnInfo)
            => new()
            {
                Action = MigrationActionEnum.AlterColumn,
                TableName = tableName,
                ColumnName = columnName,
                ColumnInfo = dbColumnInfo
            };

        /// <summary>
        /// Creates a migration operation that represents the insertion of new data rows into the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table to which data will be inserted. Cannot be null or empty.</param>
        /// <param name="rows">The data rows to be inserted. Each row is an object array representing the values for the columns.</param>
        /// <returns>A MigrationOperation configured to insert the specified data rows into the table.</returns>
        public static MigrationOperation InsertData(string tableName)
            => new()
            {
                Action = MigrationActionEnum.InsertData,
                TableName = tableName
            };
    }
}
