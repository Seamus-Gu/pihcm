using System.Collections.Frozen;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PIHCM.Gen.Enums
{
    public enum SqlTypeEnum
    {
        #region 数值类型

        /// <summary>
        /// 大整数值
        /// </summary>
        [Description("int")]
        Int = 0,

        /// <summary>
        /// 极大整数值
        /// </summary>
        [Description("bigint")]
        BigInt = 1,

        /// <summary>
        /// 单精度 浮点数值
        /// </summary>
        [Description("float")]
        Float = 2,

        /// <summary>
        /// 双精度 浮点数值
        /// </summary>
        [Description("double")]
        Double = 3,

        /// <summary>
        /// 小数值
        /// </summary>
        [Description("decimal")]
        Decimal = 4,

        #endregion

        #region 日期和时间

        /// <summary>
        /// YYYY-MM-DD	日期值
        /// </summary>
        [Description("date")]
        Date = 10,

        /// <summary>
        /// HH:MM:SS 时间值或持续时间
        /// </summary>
        [Description("time")]
        Time = 11,

        /// <summary>
        /// YYYY 年份值
        /// </summary>
        [Description("year")]
        Year = 12,

        /// <summary>
        /// YYYY-MM-DD hh:mm:ss 混合日期和时间值
        /// </summary>
        [Description("datetime")]
        Datetime = 13,

        /// <summary>
        /// YYYY-MM-DD hh:mm:ss 混合日期和时间值，时间戳
        /// </summary>
        [Description("timestamp")]
        Timestamp = 14,

        #endregion

        #region 字符串类型

        /// <summary>
        /// 定长字符串
        /// </summary>
        [Description("char")]
        Char = 21,

        /// <summary>
        /// 变长字符串
        /// </summary>
        [Description("varchar")]
        Varchar = 22,

        /// <summary>
        /// 二进制形式的长文本数据
        /// </summary>
        [Description("blob")]
        Blob = 23,

        /// <summary>
        /// 长文本数据
        /// </summary>
        [Description("text")]
        Text = 24,

        #endregion
    }

    public static class SqlTypeEnumHelper
    {
        private static readonly FrozenDictionary<string, SqlTypeEnum> _sqlTypeMappings = Enum.GetValues<SqlTypeEnum>()
            .SelectMany(CreateMappings)
            .ToFrozenDictionary(static item => item.Key, static item => item.Value, StringComparer.OrdinalIgnoreCase);

        public static bool TryParse(string sqlType, out SqlTypeEnum sqlTypeEnum)
        {
            sqlTypeEnum = default;
            return !string.IsNullOrWhiteSpace(sqlType) && _sqlTypeMappings.TryGetValue(sqlType, out sqlTypeEnum);
        }

        private static IEnumerable<KeyValuePair<string, SqlTypeEnum>> CreateMappings(SqlTypeEnum sqlType)
        {
            var name = sqlType.ToString();
            yield return new KeyValuePair<string, SqlTypeEnum>(name, sqlType);

            var description = sqlType.GetDescription();
            if (!string.Equals(name, description, StringComparison.OrdinalIgnoreCase))
            {
                yield return new KeyValuePair<string, SqlTypeEnum>(description, sqlType);
            }
        }
    }
}
