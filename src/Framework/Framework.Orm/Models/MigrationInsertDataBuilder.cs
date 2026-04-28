namespace Framework.Orm
{
    /// <summary>
    /// Provides a builder for configuring insert data operations in a database migration. Enables specifying columns
    /// and values for data insertion in a fluent manner.
    /// </summary>
    /// <remarks>This class is typically used within migration code to define the data to be inserted into a
    /// table as part of a migration operation. It supports method chaining for concise and readable configuration.
    /// Instances of this class are not intended to be created directly by application code.</remarks>
    public sealed class MigrationInsertDataBuilder
    {
        private readonly MigrationOperation _operation;

        /// <summary>
        /// Initializes a new instance of the MigrationInsertDataBuilder class using the specified migration operation.
        /// </summary>
        /// <param name="operation">The MigrationOperation that defines the data insertion operation to be configured. Cannot be null.</param>
        internal MigrationInsertDataBuilder(MigrationOperation operation)
        {
            _operation = operation;
        }

        /// <summary>
        /// Specifies the columns to be used for the insert operation in the migration builder.
        /// </summary>
        /// <remarks>Calling this method clears any previously specified columns before adding the
        /// provided column names.</remarks>
        /// <param name="columns">An array of column names to include in the insert operation. Must contain at least one column name.</param>
        /// <returns>The current instance of <see cref="MigrationInsertDataBuilder"/> to allow method chaining.</returns>
        public MigrationInsertDataBuilder Column(params string[] columns)
        {
            _operation.DataColumns.Clear();
            if (columns is { Length: > 0 })
            {
                _operation.DataColumns.AddRange(columns);
            }

            return this;
        }

        /// <summary>
        /// Adds a set of values to be inserted for the columns specified by previous calls to Column().
        /// </summary>
        /// <param name="values">The values to insert, in the same order as the columns specified by Column(). The number of values must
        /// match the number of columns. Values can be null.</param>
        /// <returns>The current MigrationInsertDataBuilder instance for method chaining.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no columns have been specified using Column(), or if the number of values does not match the
        /// number of columns.</exception>
        public MigrationInsertDataBuilder Values(params object?[] values)
        {
            if (_operation.DataColumns.Count == 0)
            {
                throw new InvalidOperationException("请先调用 Column(...) 指定列。");
            }

            if (values.Length != _operation.DataColumns.Count)
            {
                throw new InvalidOperationException("Values(...) 的值数量必须与 Column(...) 列数量一致。");
            }

            _operation.DataValues.Add(values);
            return this;
        }
    }
}