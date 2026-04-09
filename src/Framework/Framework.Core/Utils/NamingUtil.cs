using System.Globalization;
using System.Text;

namespace Framework.Core
{
    /// <summary>
    /// 提供用于不同命名风格（如 snake_case、CamelCase 和 kebab-case）之间转换的静态方法。适用于需要在多种命名规范之间进行字符串格式转换的场景。内部类，仅供程序集内使用。
    /// </summary>
    /// <remarks>该工具类支持常见的命名风格转换，便于在不同代码风格或外部系统集成时统一命名格式。所有方法均为静态方法，无需实例化即可使用。</remarks>
    public class NamingUtil
    {
        // 请使用中文注释
        /// <summary>
        /// 将下划线命名法（snake_case）字符串转换为大驼峰命名法（CamelCase）字符串。
        /// </summary>
        /// <remarks>每个下划线分隔的单词首字母将被大写，结果字符串不包含下划线。转换时会使用当前区域性进行大小写处理。</remarks>
        /// <param name="snakeCase">要转换的下划线命名法字符串。不能为空或仅包含空白字符。</param>
        /// <returns>转换后的大驼峰命名法字符串。如果输入为空或仅包含空白字符，则返回空字符串。</returns>
        public static string SnakeCaseToCamelCase(string snakeCase)
        {
            if (string.IsNullOrWhiteSpace(snakeCase))
            {
                return string.Empty;
            }

            var words = snakeCase.Split(DelimitersConstant.UNDERSCORE);
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[i].ToLower());
            }

            return string.Concat(words);
        }

        /// <summary>
        /// 将蛇形命名（snake_case）字符串转换为短横线命名（kebab-case）格式。
        /// </summary>
        /// <param name="snakeCase">要转换的蛇形命名字符串。不能为空或仅包含空白字符。</param>
        /// <returns>转换后的短横线命名字符串。如果输入为空或仅包含空白字符，则返回空字符串。</returns>
        public static string SnakeCaseToKebabCase(string snakeCase)
        {
            if (string.IsNullOrWhiteSpace(snakeCase))
            {
                return string.Empty;
            }

            return snakeCase.ToLower().Replace(DelimitersConstant.UNDERSCORE.First(), DelimitersConstant.DASH.First());
        }

        /// <summary>
        /// 将驼峰命名格式的字符串转换为蛇形命名格式。
        /// </summary>
        /// <remarks>该方法会将每个大写字母前插入下划线，并将所有字母转换为小写。适用于将 C# 或 Java 风格的变量名转换为数据库或脚本常用的下划线风格。</remarks>
        /// <param name="camelCase">要转换的驼峰命名格式字符串。不能为空或仅包含空白字符，否则返回空字符串。</param>
        /// <returns>转换后的蛇形命名格式字符串。如果输入为空或仅包含空白字符，则返回空字符串。</returns>
        public static string CamelCaseToSnakeCase(string camelCase)
        {
            if (string.IsNullOrWhiteSpace(camelCase))
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < camelCase.Length; i++)
            {
                if (char.IsUpper(camelCase[i]) && i > 0)
                {
                    stringBuilder.Append(DelimitersConstant.UNDERSCORE);
                }
                stringBuilder.Append(char.ToLower(camelCase[i]));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 将驼峰命名格式的字符串转换为短横线分隔的小写字符串（kebab-case）。
        /// </summary>
        /// <remarks>该方法适用于将 C# 或 JavaScript 等语言中的驼峰命名标识符转换为常见的 kebab-case 格式，常用于 URL、CSS 类名等场景。</remarks>
        /// <param name="camelCase">要转换的驼峰命名格式字符串。不能为空或仅包含空白字符。</param>
        /// <returns>转换后的短横线分隔小写字符串。如果输入为空或仅包含空白字符，则返回空字符串。</returns>
        public static string CamelCaseToKebabCase(string camelCase)
        {
            if (string.IsNullOrWhiteSpace(camelCase))
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < camelCase.Length; i++)
            {
                if (char.IsUpper(camelCase[i]) && i > 0)
                {
                    stringBuilder.Append('-');
                }
                stringBuilder.Append(char.ToLower(camelCase[i]));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 将下划线命名法（snake_case）字符串转换为帕斯卡命名法（PascalCase）字符串。
        /// </summary>
        /// <remarks>与大驼峰命名法类似，但通常用于表示公共或公开的类型、方法等。</remarks>
        /// <param name="snakeCase">要转换的下划线命名法字符串。不能为空或仅包含空白字符。</param>
        /// <returns>转换后的帕斯卡命名法字符串。如果输入为空或仅包含空白字符，则返回空字符串。</returns>
        public static string SnakeCaseToPascal(string snakeCase)
        {
            if (string.IsNullOrWhiteSpace(snakeCase))
            {
                return string.Empty;
            }

            var words = snakeCase.Split(DelimitersConstant.UNDERSCORE, StringSplitOptions.RemoveEmptyEntries);
            var stringBuilder = new StringBuilder();

            foreach (var word in words)
            {
                if (word.Length == 0)
                {
                    continue;
                }

                var lowerWord = word.ToLower(CultureInfo.CurrentCulture);
                var pascalWord = char.ToUpper(lowerWord[0], CultureInfo.CurrentCulture)
                    + (lowerWord.Length > 1 ? lowerWord[1..] : string.Empty);

                stringBuilder.Append(pascalWord);
            }

            return stringBuilder.ToString();
        }
    }
}
