namespace Framework.WebApi
{
    /// <summary>
    /// 下拉
    /// </summary>
    public class Combo
    {
        /// <summary>
        /// 显示值
        /// </summary>
        public virtual string Label { get; set; } = string.Empty;

        /// <summary>
        /// 值
        /// </summary>
        public virtual string Value { get; set; } = string.Empty;
    }
}