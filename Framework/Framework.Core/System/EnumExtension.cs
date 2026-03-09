using Microsoft.Extensions.Localization;
using System.ComponentModel;

namespace System
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举的 int 值
        /// </summary>
        public static int ToInt(this Enum value) => Convert.ToInt32(value);

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T value) where T : Enum
        {
            var field = value.GetType().GetField(value.ToString());

            if (field == null) return string.Empty;

            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (objs.Length == 0)
                return value.ToString();

            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }

        /// <summary>
        /// 获取枚举属性名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetName(this Enum value) => Enum.GetName(value.GetType(), value) ?? value.ToString();

        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="enumValue"></param>
        /// <param name="localizer"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static CodeException ToCodeException(this Enum enumValue, IStringLocalizer localizer, params object?[] args)
        {
            var code = Convert.ToInt32(enumValue);
            var msg = string.Format(localizer[enumValue.GetName()], args);

            return new CodeException(code, msg);
        }
    }
}
