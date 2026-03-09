using SqlSugar;

namespace Seed.Framework.WebApi
{
    /// <summary>
    /// 树基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeEntity<T> : BaseEntity
    {
        /// <summary>
        /// 父Id
        /// </summary>
        public virtual long ParentId { get; set; }

        /// <summary>
        /// 树级
        /// </summary>
        public virtual int NodeLevel { get; set; }

        /// <summary>
        /// 排序数
        /// </summary>
        public virtual int OrderNum { get; set; }

        /// <summary>
        /// 祖级列表
        /// </summary>
        public virtual string? TreeIds { get; set; }

        /// <summary>
        /// 子
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public virtual List<T>? Children { get; set; }
    }
}