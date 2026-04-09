
namespace PIHCM.Gen.Entities
{
    public class GenTable : BaseEntity
    {
        /// <summary>
        /// 所属命名空间
        /// </summary>
        public virtual string Namespace { get; set; } = string.Empty;

        /// <summary>
        /// 数据库表名
        /// </summary>
        public virtual string TableName { get; set; } = string.Empty;

        /// <summary>
        /// 实体名称
        /// </summary>
        public virtual string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// 实体名称
        /// </summary>
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string? Description { get; set; }

        /// <summary>
        /// 实体类别
        /// </summary>
        public virtual EntityTypeEnum EntityType { get; set; }

        /// <summary>
        /// 需要分页
        /// </summary>
        public virtual bool HasPagination { get; set; }

        /// <summary>
        /// 需要Combo下拉
        /// </summary>
        public virtual bool HasCombo { get; set; }

        /// <summary>
        /// 需要前端
        /// </summary>
        public virtual bool HasFrontend { get; set; }

        /// <summary>
        /// 表名驼峰命名表示
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string CamelName
        {
            get
            {
                return NamingUtil.SnakeCaseToCamelCase(this.TableName);
            }
        }

        [SugarColumn(IsIgnore = true)]
        public string KebabName
        {
            get
            {
                return NamingUtil.SnakeCaseToKebabCase(this.TableName);
            }
        }

        /// <summary>
        /// 获取数据库表中所有列的只读集合。用于描述表结构的各个字段。
        /// </summary>
        /// <remarks>集合中的每个元素都表示一个数据库列，包含列名、类型等元数据信息。该属性在对象初始化后不可更改，适用于只读场景。</remarks>
        [SugarColumn(IsIgnore = true)]
        public List<GenColumn> Columns { get; set; } = [];
    }
}
