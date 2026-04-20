namespace PIHCM.Gen.Services
{
    public class SQLService : ISQLService, IScopeService
    {
        private readonly GenTableRepository _genTableRepository;
        private readonly GenColumnRepository _genColumnRepository;
        private readonly ISqlSugarClient _db;

        private static readonly Regex _tableNameRegex = new(
            SQLConstant.TABLE_NAME_REGEX,
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex _tableCommentRegex = new(
            SQLConstant.TABLE_COMMENT_REGEX,
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex _columnDefinitionRegex = new(
            SQLConstant.COLUMN_DEFINITION_REGEX,
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex _columnCommentRegex = new(
            SQLConstant.COLUMN_COMMENT_REGEX,
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex _inlinePrimaryKeyRegex = new(
            SQLConstant.INLINE_PRIMARY_KEY_REGEX,
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex _notNullRegex = new(
            SQLConstant.NOT_NULL_REGEX,
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex _constraintDefinitionRegex = new(
            SQLConstant.CONSTRAINT_DEFINITION_REGEX,
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public SQLService(GenTableRepository genTableRepository, GenColumnRepository genColumnRepository, ISqlSugarClient db)
        {
            _genTableRepository = genTableRepository;
            _genColumnRepository = genColumnRepository;
            _db = db;
        }

        public async Task<bool> ParseCreateTableSql(SQLGenerateDto sqlDto)
        {
            var sql = sqlDto.SqlStr;
            var tableName = ParseTableName(sql);

            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("无法从 SQL 中解析表名。", nameof(sqlDto));
            }

            var name = ParseTableComment(sql) ?? string.Empty;
            var tableBody = ParseTableBody(sql);
            var definitions = SplitDefinitions(tableBody);

            var columns = ParseColumns(definitions, name);

            var table = new GenTable
            {
                Namespace = App.AppName,
                Name = name,
                TableName = tableName,
                EntityName = NamingUtil.SnakeCaseToPascal(tableName),
            };

            columns.ForEach(t => t.TableId = table.Id);

            var tran = await _db.Ado.UseTranAsync(async () =>
            {
                await _genTableRepository.InsertAsync(table);
                await _genColumnRepository.InsertRangeAsync(columns);
            });

            if (!tran.IsSuccess)
            {
                //Todo 打印日志 代码出错问题和位置stack
                return false;
            }

            return true;
        }

        private static string ParseTableName(string sql)
        {
            var match = _tableNameRegex.Match(sql);
            if (!match.Success)
            {
                return string.Empty;
            }

            var rawName = match.Groups[SQLConstant.NAME].Value;
            var pureName = rawName.Split(DelimitersConstant.DOT).Last();
            return TrimQuotes(pureName);
        }

        private static string? ParseTableComment(string sql)
        {
            var match = _tableCommentRegex.Match(sql);
            return match.Success ? match.Groups[SQLConstant.COMMENT].Value : null;
        }

        private static string ParseTableBody(string sql)
        {
            var start = sql.IndexOf(DelimitersConstant.LEFT_PARENTHESIS[0]);
            if (start < 0)
            {
                throw new ArgumentException("建表 SQL 缺少字段定义部分。", nameof(sql));
            }

            var depth = 0;
            for (var i = start; i < sql.Length; i++)
            {
                if (sql[i] == DelimitersConstant.LEFT_PARENTHESIS[0])
                {
                    depth++;
                }
                else if (sql[i] == DelimitersConstant.RIGHT_PARENTHESIS[0])
                {
                    depth--;
                    if (depth == 0)
                    {
                        return sql[(start + 1)..i];
                    }
                }
            }

            throw new ArgumentException("建表 SQL 字段定义括号不完整。", nameof(sql));
        }

        private static List<string> SplitDefinitions(string body)
        {
            var result = new List<string>();
            var sb = new StringBuilder();
            var depth = 0;

            foreach (var ch in body)
            {
                if (ch == DelimitersConstant.LEFT_PARENTHESIS[0])
                {
                    depth++;
                }
                else if (ch == DelimitersConstant.RIGHT_PARENTHESIS[0])
                {
                    depth--;
                }

                if (ch == DelimitersConstant.COMMA[0] && depth == 0)
                {
                    var part = sb.ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(part))
                    {
                        result.Add(part);
                    }
                    sb.Clear();
                    continue;
                }

                sb.Append(ch);
            }

            var last = sb.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(last))
            {
                result.Add(last);
            }

            return result;
        }

        private static List<GenColumn> ParseColumns(List<string> definitions, string tableName)
        {
            var columns = new List<GenColumn>(definitions.Count);

            foreach (var item in definitions)
            {
                if (IsConstraintDefinition(item))
                {
                    continue;
                }

                var match = _columnDefinitionRegex.Match(item);
                if (!match.Success)
                {
                    continue;
                }

                var name = TrimQuotes(match.Groups[SQLConstant.NAME].Value);
                var descriptionMatch = _columnCommentRegex.Match(item);
                var isPrimaryKeyInline = _inlinePrimaryKeyRegex.IsMatch(item);

                if (name == $"{tableName}{DelimitersConstant.UNDERSCORE}{SQLConstant.ID}")
                {
                    continue;
                }

                if (name == SQLConstant.CREATED_AT || name == SQLConstant.CREATED_BY || name == SQLConstant.UPDATED_AT || name == SQLConstant.UPDATED_BY)
                {
                    continue;
                }

                columns.Add(new GenColumn
                {
                    ColumnName = name,
                    ColumnType = HandleSqlType(match.Groups[SQLConstant.TYPE].Value),
                    IsNullable = !_notNullRegex.IsMatch(item),
                    ColumnDesc = descriptionMatch.Success ? descriptionMatch.Groups[SQLConstant.COMMENT].Value : null
                });
            }

            return columns;
        }

        private static string HandleSqlType(string type)
        {
            var normalized = type.Trim().ToLowerInvariant();

            var leftParenIndex = normalized.IndexOf(DelimitersConstant.LEFT_PARENTHESIS);
            if (leftParenIndex > 0)
            {
                normalized = normalized[..leftParenIndex];
            }

            var spaceIndex = normalized.IndexOf(DelimitersConstant.SPACE);
            if (spaceIndex > 0)
            {
                normalized = normalized[..spaceIndex];
            }

            return normalized;
        }

        private static bool IsConstraintDefinition(string definition)
        {
            return _constraintDefinitionRegex.IsMatch(definition);
        }

        private static string TrimQuotes(string value)
        {
            return value.Trim().Trim(
                DelimitersConstant.BACKTICK[0],
                DelimitersConstant.DOUBLE_QUOTE[0],
                DelimitersConstant.LEFT_BRACKET[0],
                DelimitersConstant.RIGHT_BRACKET[0]);
        }
    }
}
