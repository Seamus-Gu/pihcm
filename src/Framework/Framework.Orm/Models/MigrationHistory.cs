using Framework.Core;
using SqlSugar;

namespace Framework.Orm
{
    [SugarTable("__migration_history")]
    internal class MigrationHistory
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the version identifier for the entity.
        /// </summary>
        [SugarColumn(Length = 20)]
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the migration to be applied or referenced.
        /// </summary>
        public string MigrationName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file system path associated with this instance.
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the operation completed successfully.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error message associated with the current operation or result.
        /// </summary>
        [SugarColumn(ColumnDataType = SQLConstant.TEXT, IsNullable = true)]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
