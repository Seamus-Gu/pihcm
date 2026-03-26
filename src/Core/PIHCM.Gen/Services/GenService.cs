using PIHCM.Gen.Dto;
using System.Text;
using System.Text.RegularExpressions;

namespace PIHCM.Gen.Services
{
    public class GenService : IGenService, IScopeService
    {
        public void ParseCreateTableSql(SQLDto createTableSql)
        {
            //if (createTableSql.IsNullOrEmpty())
            //{
            //    return;
            //}

            var tableName = ParseTableName(createTableSql.SqlStr);

            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("无法从 SQL 中解析表名。", nameof(createTableSql));
            }

            //var tableDescription = ParseTableComment(createTableSql);
            //var tableBody = ParseTableBody(createTableSql);
            //var definitions = SplitDefinitions(tableBody);

            //var primaryKeys = ParsePrimaryKeys(definitions);
            //var columns = ParseColumns(definitions, primaryKeys);

            //var table = new GenTable
            //{
            //    Name = tableName,
            //    EntityName = NamingUtil.SnakeCaseToCamelCase(tableName),
            //    Description = tableDescription,
            //    Columns = columns
            //};
        }

        private static string ParseTableName(string sql)
        {
            var match = Regex.Match(
                sql,
                """CREATE\s+TABLE\s+(?:IF\s+NOT\s+EXISTS\s+)?(?<name>[`"\[]?[A-Za-z0-9_]+[`"\]]?(?:\.[`"\[]?[A-Za-z0-9_]+[`"\]]?)?)""",
                RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                return string.Empty;
            }

            var rawName = match.Groups["name"].Value;
            var pureName = rawName.Split('.').Last();
            return TrimQuotes(pureName);
        }

        private static string? ParseTableComment(string sql)
        {
            var match = Regex.Match(sql, @"\bCOMMENT\s*=\s*'(?<comment>[^']*)'", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups["comment"].Value : null;
        }

        private static string ParseTableBody(string sql)
        {
            var start = sql.IndexOf('(');
            if (start < 0)
            {
                throw new ArgumentException("建表 SQL 缺少字段定义部分。", nameof(sql));
            }

            var depth = 0;
            for (var i = start; i < sql.Length; i++)
            {
                if (sql[i] == '(')
                {
                    depth++;
                }
                else if (sql[i] == ')')
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
                if (ch == '(')
                {
                    depth++;
                }
                else if (ch == ')')
                {
                    depth--;
                }

                if (ch == ',' && depth == 0)
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

        private static HashSet<string> ParsePrimaryKeys(List<string> definitions)
        {
            var primaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var item in definitions)
            {
                if (!Regex.IsMatch(item, @"^PRIMARY\s+KEY", RegexOptions.IgnoreCase))
                {
                    continue;
                }

                var match = Regex.Match(item, @"\((?<keys>[^\)]*)\)", RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    continue;
                }

                var keys = match.Groups["keys"].Value
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(TrimQuotes);

                foreach (var key in keys)
                {
                    primaryKeys.Add(key);
                }
            }

            return primaryKeys;
        }

        private static List<GenColumn> ParseColumns(List<string> definitions, HashSet<string> primaryKeys)
        {
            var columns = new List<GenColumn>();

            foreach (var item in definitions)
            {
                if (IsConstraintDefinition(item))
                {
                    continue;
                }

                var match = Regex.Match(
                    item,
                    """^(?<name>[`"\[]?[A-Za-z0-9_]+[`"\]]?)\s+(?<type>[A-Za-z]+(?:\s+[A-Za-z]+)?(?:\s*\([^\)]*\))?(?:\s+unsigned)?)""",
                    RegexOptions.IgnoreCase);

                if (!match.Success)
                {
                    continue;
                }

                var name = TrimQuotes(match.Groups["name"].Value);
                var descriptionMatch = Regex.Match(item, @"\bCOMMENT\s+'(?<comment>[^']*)'", RegexOptions.IgnoreCase);
                var isPrimaryKeyInline = Regex.IsMatch(item, @"\bPRIMARY\s+KEY\b", RegexOptions.IgnoreCase);

                columns.Add(new GenColumn
                {
                    //Name = name,
                    //DataType = match.Groups["type"].Value.Trim(),
                    //IsNullable = !Regex.IsMatch(item, @"\bNOT\s+NULL\b", RegexOptions.IgnoreCase),
                    //IsPrimaryKey = isPrimaryKeyInline || primaryKeys.Contains(name),
                    //Description = descriptionMatch.Success ? descriptionMatch.Groups["comment"].Value : null
                });
            }

            return columns;
        }

        private static bool IsConstraintDefinition(string definition)
        {
            return Regex.IsMatch(
                definition,
                @"^(PRIMARY\s+KEY|UNIQUE\s+KEY|UNIQUE\s+INDEX|KEY|INDEX|CONSTRAINT|FOREIGN\s+KEY)",
                RegexOptions.IgnoreCase);
        }

        private static string TrimQuotes(string value)
        {
            return value.Trim().Trim('`', '"', '[', ']');
        }
    }
}
