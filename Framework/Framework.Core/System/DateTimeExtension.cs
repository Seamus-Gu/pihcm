namespace System
{
    /// <summary>
    /// 提供一组用于扩展和简化对日期和时间（DateTime）常用操作的方法，包括年龄计算、工作日判断、日期范围判断、日期格式化等功能。适用于需要对日期进行常见处理的场景。扩展方法可直接用于 DateTime
    /// 实例，提高代码可读性和开发效率。
    /// </summary>
    /// <remarks>本类为静态扩展类，所有方法均为静态扩展方法，可通过 DateTime
    /// 实例直接调用。例如，可用于判断某日期是否为工作日、获取指定日期的年龄、将日期格式化为常用字符串等。部分方法支持自定义参数（如周起始日），便于灵活适配不同业务需求。调用时请注意参数的有效性，避免传入无效或超出范围的日期值。</remarks>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 获取当前日期时间与指定日期时间之间的年龄差
        /// </summary>
        /// <param name="this"> 指定日期 </param>
        /// <returns> 返回两个日期之间的年龄差 </returns>
        public static int GetAge(this DateTime @this)
        {
            //如果当年月份小于指定日期的月份,
            //或者当年月份等于指定日期的月份但当年日期小于指定日期的日期,
            //则返回当前年份与指定日期年份之间的差值减一
            if (DateTime.Today.Month < @this.Month ||
                DateTime.Today.Month == @this.Month &&
                DateTime.Today.Day < @this.Day)
            {
                return DateTime.Today.Year - @this.Year - 1;
            }

            // 返回当前年份与指定日期年份之间的差值
            return DateTime.Today.Year - @this.Year;
        }

        /// <summary>
        /// 判断当前日期是否为工作日
        /// </summary>
        /// <param name="this"> 要进行判断的日期 </param>
        /// <returns> 当前日期是工作日返回 true ,当前日期是周末返回 false</returns>
        public static bool IsWeekDay(this DateTime @this)
        {
            return !(@this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday);
        }

        /// <summary>
        /// 判断当前日期是否为周末
        /// </summary>
        /// <param name="this"> 要进行判断的日期 </param>
        /// <returns> 当前日期是周末返回 true ,当前日期是工作日返回 false </returns>
        public static bool IsWeekendDay(this DateTime @this)
        {
            return @this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// 获取当前日期所在星期的第一天，即本周的起始日期
        /// </summary>
        /// <param name="dt"> 要进行处理的日期 </param>
        /// <param name="startDayOfWeek"> 星期的起始日，默认为星期天 </param>
        /// <returns> 返回本周的起始日期 </returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
        {
            var start = new DateTime(dt.Year, dt.Month, dt.Day);

            if (start.DayOfWeek != startDayOfWeek)
            {
                var d = startDayOfWeek - start.DayOfWeek;
                if (startDayOfWeek <= start.DayOfWeek)
                {
                    return start.AddDays(d);
                }
                return start.AddDays(-7 + d);
            }

            return start;
        }

        /// <summary>
        /// 将当前日期时间转换为 Unix 时间戳（以秒为单位）
        /// </summary>
        /// <param name="this"> 要进行转换的日期时间 </param>
        /// <returns> 返回 Unix 时间戳 </returns>
        public static TimeSpan ToEpochTimeSpan(this DateTime @this) => @this.ToUniversalTime().Subtract(new DateTime(1970, 1, 1));

        /// <summary>
        /// 判断当前日期是否在指定日期范围内
        /// </summary>
        /// <param name="this"> 要进行判断的日期< </param>
        /// <param name="minValue"> 日期范围的最小值 </param>
        /// <param name="maxValue"> 日期范围的最大值 </param>
        /// <returns>当前日期在指定的日期范围内返回 true ,当前日期不在指定的日期范围内返回 false</returns>
        public static bool InRange(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        /// <summary>
        /// 返回日期字符串 (yyyy/MM/dd)
        /// </summary>
        /// <param name="this"> 当前日期 </param>
        /// <returns> 日期字符串 </returns>
        public static string ToDateString(this DateTime @this)
        {
            return @this.ToString(DateTimeConstant.DATE);
        }

        /// <summary>
        /// 返回日期字符串 (yyyy/MM/dd HH:mm)
        /// </summary>
        /// <param name="this"> 当前日期 </param>
        /// <returns> 日期字符串 </returns>
        public static string ToDateShortString(this DateTime @this)
        {
            return @this.ToString(DateTimeConstant.DATE_SHORT);
        }

        /// <summary>
        /// 返回日期字符串 (yyyy/MM/dd HH:mm:ss)
        /// </summary>
        /// <param name="this"> 当前日期 </param>
        /// <returns> 日期字符串 </returns>
        public static string ToDateLongString(this DateTime @this)
        {
            return @this.ToString(DateTimeConstant.DATE_LONG);
        }

        /// <summary>
        /// 返回日期字符串 (yyyy-MM-dd)
        /// </summary>
        /// <param name="this"> 当前日期 </param>
        /// <returns> 日期字符串 </returns>
        public static string ToDateTimeString(this DateTime @this)
        {
            return @this.ToString(DateTimeConstant.DETE_TIME);
        }

        /// <summary>
        /// 返回日期字符串 (yyyy-MM-dd HH:mm)
        /// </summary>
        /// <param name="this"> 当前日期 </param>
        /// <returns> 日期字符串 </returns>
        public static string ToDateTimeShortString(this DateTime @this)
        {
            return @this.ToString(DateTimeConstant.DETE_TIME_SHORT);
        }

        /// <summary>
        /// 返回日期字符串 (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="this"> 当前日期 </param>
        /// <returns> 日期字符串 </returns>
        public static string ToDateTimeLongString(this DateTime @this)
        {
            return @this.ToString(DateTimeConstant.DETE_TIME_LONG);
        }
    }
}