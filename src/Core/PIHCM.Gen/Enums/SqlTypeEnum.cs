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

        [Description("tinyint")]
        Tinyint = 5,

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
}
