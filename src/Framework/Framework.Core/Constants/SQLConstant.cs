namespace Framework.Core
{
    public class SQLConstant
    {
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
