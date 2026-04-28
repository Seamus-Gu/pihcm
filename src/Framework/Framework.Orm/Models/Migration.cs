using SqlSugar;

namespace Framework.Orm
{
    /// <summary>
    /// 迁移基类
    /// </summary>
    public abstract class Migration
    {
        private readonly List<MigrationOperation> _operations = [];

        /// <summary>
        /// Gets the name of the current type instance.
        /// </summary>
        public virtual string Name => GetType().Name;

        /// <summary>
        /// Generates and returns the list of operations required to apply the migration.
        /// </summary>
        /// <remarks>This method is intended for internal use when building the set of operations for the
        /// 'up' direction of a migration. The returned list reflects the current state after executing the migration's
        /// 'Up' method.</remarks>
        /// <returns>A read-only list of <see cref="MigrationOperation"/> objects representing the operations to be performed
        /// when applying the migration. The list will be empty if no operations are defined.</returns>
        internal IReadOnlyList<MigrationOperation> GetUpOperations()
        {
            _operations.Clear();
            Up();
            return _operations.ToArray();
        }

        /// <summary>
        /// Retrieves the list of migration operations required to revert the effects of the current migration.
        /// </summary>
        /// <remarks>This method is intended for internal use when generating the down script for a
        /// migration. The returned operations correspond to the actions defined in the migration's Down
        /// method.</remarks>
        /// <returns>A read-only list of <see cref="MigrationOperation"/> objects representing the operations to be applied when
        /// rolling back the migration. The list will be empty if no down operations are defined.</returns>
        internal IReadOnlyList<MigrationOperation> GetDownOperations()
        {
            _operations.Clear();
            Down();
            return _operations.ToArray();
        }

        /// <summary>
        /// Defines the operations to be performed when applying a migration.
        /// </summary>
        /// <remarks>Override this method to specify the logic required to update the database schema to
        /// the latest version. This method is called when the migration is applied.</remarks>
        protected abstract void Up();

        /// <summary>
        /// Reverts the operations performed by the associated migration, restoring the database to its previous state.
        /// </summary>
        /// <remarks>Override this method in a derived class to define the logic required to undo changes
        /// applied in the corresponding Up method. This is typically used in database migration scenarios to support
        /// rolling back schema or data changes.</remarks>
        protected abstract void Down();

        protected MigrationCreateTableBuilder CreateTable(string tableName)
        {
            var op = MigrationOperation.CreateTable(tableName);
            _operations.Add(op);
            return new MigrationCreateTableBuilder(op);
        }

        protected void DropTable(string tableName)
            => _operations.Add(MigrationOperation.DropTable(tableName));

        protected void AddColumn(string tableName, DbColumnInfo columnInfo)
            => _operations.Add(MigrationOperation.AddColumn(tableName, columnInfo));

        protected void DropColumn(string tableName, string columnName)
            => _operations.Add(MigrationOperation.DropColumn(tableName, columnName));

        protected void AlterColumn(string tableName, string columnName, DbColumnInfo columnInfo)
            => _operations.Add(MigrationOperation.AlterColumn(tableName, columnName, columnInfo));

        protected MigrationInsertDataBuilder InsertData(string tableName)
        {
            var op = MigrationOperation.InsertData(tableName);
            _operations.Add(op);
            return new MigrationInsertDataBuilder(op);
        }
    }
}
