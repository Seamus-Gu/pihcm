namespace Framework.WebApi
{
    public class TreeSelect
    {
        /// <summary>
        /// 父Id
        /// </summary>
        public virtual long ParentId { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public virtual string? Label { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public virtual long Value { get; set; }

        /// <summary>
        /// 子
        /// </summary>
        public virtual List<TreeSelect>? Children { get; set; }
    }
}