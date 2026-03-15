using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// 提供用于字符串常见操作的扩展方法。
    /// </summary>
    ///
    /// <remarks>该静态类包含一组扩展方法，简化字符串的常用判断、转换和处理操作，如判空、重复、反转、模式匹配、类型转换等。所有方法均为静态方法，可直接通过字符串实例调用。线程安全，适用于多线程环境。</remarks>
    public static class StringExtension
    {
        /// <summary>
        /// 判断字符串是否为 Null 或空字符串
        /// </summary>
        /// <param name="this"> 当前字符串 </param>
        /// <returns>如果字符串为 Null 或空字符串,则返回 true;否则返回 false.</returns>
        public static bool IsNullOrEmpty(this string? @this) => string.IsNullOrEmpty(@this);

        /// <summary>
        /// 判断字符串是否为 Null 或白字符串
        /// </summary>
        /// <param name="this"> 当前字符串 </param>
        /// <returns>如果字符串为 Null 或由空白字符组成,则返回 true;否则返回 false.</returns>
        /// <remarks>例如"  "此时返回True</remarks>
        public static bool IsNullOrWhiteSpace(this string? @this) => string.IsNullOrWhiteSpace(@this);

        /// <summary>
        /// 判断字符串是否符合指定模式
        /// </summary>
        /// <param name="this"> 当前字符串 </param>
        /// <param name="pattern">要使用的模式.使用"*"作为通配符字符串.</param>
        /// <returns>如果字符串符合指定模式,则返回true; 否则返回false</returns>
        public static bool IsLike([NotNull] this string @this, string pattern)
        {
            // 将模式转换为正则表达式,并使用 ^$ 匹配整个字符串
            var regexPattern = "^" + Regex.Escape(pattern) + "$";

            // 转义特殊字符 ?、#、*、[] 和 [!]
            regexPattern = regexPattern.Replace(@"\[!", "[^")
                .Replace(@"\[", "[")
                .Replace(@"\]", "]")
                .Replace(@"\?", ".")
                .Replace(@"\*", ".*")
                .Replace(@"\#", @"\d");

            // 判断字符串是否符合正则表达式规则
            return Regex.IsMatch(@this, regexPattern);
        }

        /// <summary>
        /// 重复指定的字符串指定次数
        /// </summary>
        /// <param name="this"> 当前字符串 </param>
        /// <param name="repeatCount"> 重复次数 </param>
        /// <returns> 重复指定次数后的字符串 </returns>
        public static string Repeat([NotNull] this string @this, int repeatCount)
        {
            if (@this.Length == 1)
            {
                return new string(@this[0], repeatCount);
            }

            var sb = new StringBuilder(repeatCount * @this.Length);
            while (repeatCount-- > 0)
            {
                sb.Append(@this);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 反转给定字符串
        /// </summary>
        /// <param name="this"> 当前字符串 </param>
        /// <returns> 反转后的字符串 </returns>
        public static string Reverse([NotNull] this string @this)
        {
            if (@this.Length <= 1)
            {
                return @this;
            }

            var chars = @this.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// 返回使用指定编码的字符串所表示的字节数组
        /// </summary>
        /// <param name="str"> 要转换的字符串 </param>
        /// <param name="encoding"> 要使用的编码 </param>
        /// <returns> 包含字符串中字符所表示的字节的数组 </returns>
        public static byte[] GetBytes([NotNull] this string str, Encoding encoding) => encoding.GetBytes(str);

        /// <summary>
        /// 判断两个字符串是否相等,忽略大小写.
        /// </summary>
        /// <param name="s1"> 字符串1 </param>
        /// <param name="s2"> 字符串2 </param>
        /// <returns>如果两个字符串相等（忽略大小写）,则返回 true;否则返回 false.</returns>
        public static bool EqualsIgnoreCase(this string s1, string s2) => string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 将字符串转换为长整型数值.
        /// </summary>
        /// <param name="txt"> 要转换的字符串 </param>
        /// <returns> 如果转换成功,返回转换后的长整型数值;否则返回 null. </returns>
        public static int? ToInt(this string @this)
        {
            bool status = int.TryParse(@this, out int result);

            if (status)
                return result;
            else
                return null;
        }

        /// <summary>
        /// 将字符串转换为长整型数值.
        /// </summary>
        /// <param name="txt"> 要转换的字符串 </param>
        /// <returns> 如果转换成功,返回转换后的长整型数值;否则返回 null. </returns>
        public static long? ToLong(this string @this)
        {
            bool status = long.TryParse(@this, out long result);

            if (status)
                return result;
            else
                return null;
        }

        /// <summary>
        /// 驼峰转下划线命名
        /// </summary>
        /// <param name="humpString"></param>
        /// <returns></returns>
        public static string ToUnderline(string humpString)
        {
            return Regex.Replace(humpString, "([A-Z])", "_$1").ToLower().TrimStart('_');
        }
    }
}