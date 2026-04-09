namespace Framework.Core
{
    public class SQLConstant
    {
        #region 类型
        public const string INT = "int";
        public const string INTEGER = "integer";
        public const string BIGINT = "bigint";
        public const string FLOAT = "float";
        public const string DOUBLE = "double";
        public const string DECIMAL = "decimal";
        public const string TINYINT = "tinyint";

        public const string DATE = "date";
        public const string TIME = "time";
        public const string YEAR = "year";
        public const string DATETIME = "datetime";
        public const string TIMESTAMP = "timestamp";

        public const string CHAR = "char";
        public const string VARCHAR = "varchar";
        public const string BLOB = "blob";
        public const string TEXT = "text";
        #endregion

        public const string CREATED_AT = "created_at";
        public const string CREATED_BY = "created_by";
        public const string UPDATED_AT = "updated_at";
        public const string UPDATED_BY = "updated_by";

        public const string NAME = "name";
        public const string COMMENT = "comment";
        public const string TYPE = "type";
        public const string KEYS = "keys";

        public const string TABLE_NAME_REGEX = """CREATE\s+TABLE\s+(?:IF\s+NOT\s+EXISTS\s+)?(?<name>[`\"\[]?[A-Za-z0-9_]+[`\"\]]?(?:\.[`\"\[]?[A-Za-z0-9_]+[`\"\]]?)?)""";
        public const string TABLE_COMMENT_REGEX = @"\bCOMMENT\s*=\s*'(?<comment>[^']*)'";
        public const string PRIMARY_KEY_DEFINITION_REGEX = @"^PRIMARY\s+KEY";
        public const string PRIMARY_KEY_COLUMNS_REGEX = @"\((?<keys>[^\)]*)\)";
        public const string COLUMN_DEFINITION_REGEX = """^(?<name>[`\"\[]?[A-Za-z0-9_]+[`\"\]]?)\s+(?<type>[A-Za-z]+(?:\s+[A-Za-z]+)?(?:\s*\([^\)]*\))?(?:\s+unsigned)?)""";
        public const string COLUMN_COMMENT_REGEX = @"\bCOMMENT\s+'(?<comment>[^']*)'";
        public const string INLINE_PRIMARY_KEY_REGEX = @"\bPRIMARY\s+KEY\b";
        public const string NOT_NULL_REGEX = @"\bNOT\s+NULL\b";
        public const string CONSTRAINT_DEFINITION_REGEX = @"^(PRIMARY\s+KEY|UNIQUE\s+KEY|UNIQUE\s+INDEX|KEY|INDEX|CONSTRAINT|FOREIGN\s+KEY)";
    }
}
