namespace Framework.Orm
{
    internal enum MigrationActionEnum
    {
        /// <summary>
        /// Specifies the operation to create a new table in the database.
        /// </summary>
        CreateTable = 1,

        /// <summary>
        /// Specifies an operation that drops an existing table from the database.
        /// </summary>
        DropTable = 2,

        /// <summary>
        /// Specifies an operation to add a new column.
        /// </summary>
        AddColumn = 3,

        /// <summary>
        /// Indicates that an existing column is to be altered in a database schema operation.
        /// </summary>
        /// <remarks>Use this value when specifying a schema change that modifies the definition of an
        /// existing column, such as changing its data type or constraints.</remarks>
        AlterColumn = 4,

        /// <summary>
        /// Specifies an operation that removes a column from a database table.
        /// </summary>
        DropColumn = 5,

        /// <summary>
        /// Represents the operation to insert new data into a table.
        /// </summary>
        InsertData = 6,
    }
}
