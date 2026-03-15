namespace Seed.Framework.Core
{
    /// <summary>
    /// 提供常用分隔符的字符串常量集合，便于在文本处理和数据分隔等场景中统一使用。
    /// </summary>
    ///
    /// <remarks>该类包含常见的分隔符，如分号、逗号、点号、冒号、破折号、下划线、空格、制表符、竖线、斜杠、反斜杠及大括号。可用于字符串拆分、格式化、解析等多种应用场景。所有成员均为只读常量，可直接通过类名访问。</remarks>
    public class DelimitersConstant
    {
        public const string SEMICOLON = ";";
        public const string COMMA = ",";
        public const string DOT = ".";
        public const string COLON = ":";
        public const string DASH = "-";
        public const string UNDERSCORE = "_";
        public const string SPACE = " ";
        public const string TAB = "\t";
        public const string PIPE = "|";
        public const string SLASH = "/";
        public const string BACKSLASH = "\\";
        public const string LEFT_BRACE = "{";
        public const string RIGHT_BRACE = "}";
    }
}