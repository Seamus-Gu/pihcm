namespace Framework.Core
{
    /// <summary>
    /// 提供常用的日期和时间格式字符串常量，便于在日期和时间的格式化和解析操作中复用。
    /// </summary>
    /// <remarks>这些常量可用于 .NET 标准日期和时间格式化方法（如 DateTime.ToString、DateTime.ParseExact
    /// 等），以确保日期和时间字符串的一致性。格式包括常见的年/月/日、年-月-日及其带时分秒的变体，适用于多种业务场景。所有格式均基于 24 小时制。</remarks>
    public static class DateTimeConstant
    {
        /// <summary>
        /// 年/月/日
        /// </summary>
        public const string DATE = "yyyy/MM/dd";

        /// <summary>
        /// 年/月/日 时:分
        /// </summary>
        public const string DATE_SHORT = "yyyy/MM/dd HH:mm";

        /// <summary>
        /// 年/月/日 时:分:秒
        /// </summary>
        public const string DATE_LONG = "yyyy/MM/dd HH:mm:ss";

        /// <summary>
        /// 年-月-日
        /// </summary>
        public const string DETE_TIME = "yyyy-MM-dd";

        /// <summary>
        /// 年-月-日 时:分
        /// </summary>
        public const string DETE_TIME_SHORT = "yyyy-MM-dd HH:mm";

        /// <summary>
        /// 年-月-日 时:分:秒
        /// </summary>
        public const string DETE_TIME_LONG = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 时:分
        /// </summary>
        public const string TIME = "HH:mm";

        /// <summary>
        /// 时:分:秒
        /// </summary>
        public const string SYSTEM_TIME = "HH:mm:ss";
    }
}