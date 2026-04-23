namespace Framework.WebApi
{
    /// <summary>
    /// int 值 下拉
    /// </summary>
    public class IntCombo
    {
        /// <summary>
        /// 显示值
        /// </summary>
        public virtual string Label { get; set; } = string.Empty;

        /// <summary>
        /// 值
        /// </summary>
        public virtual int Value { get; set; }
    }
}