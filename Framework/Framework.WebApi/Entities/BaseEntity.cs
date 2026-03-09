using SqlSugar;
using Yitter.IdGenerator;

namespace Seed.Framework.WebApi
{
    [Serializable]
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Id = YitIdHelper.NextId();
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public virtual long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual string? UpdateBy { get; set; }
    }
}