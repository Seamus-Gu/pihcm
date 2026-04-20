using PIHCM.Gen.Constants;

namespace PIHCM.Gen.Entities
{
    public class GenColumn : BaseEntity
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; } = string.Empty;

        /// <summary>
        /// 列类型
        /// </summary>
        public string ColumnType { get; set; } = string.Empty;

        /// <summary>
        /// 列描述(注释)
        /// </summary>
        public string? ColumnDesc { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int? TypeLength { get; set; }

        /// <summary>
        /// 小数点
        /// </summary>
        public int? Point { get; set; }

        /// <summary>
        /// Gets or sets the key used to identify the translation resource associated with this instance.
        /// </summary>
        public string TranslationKey { get; set; } = string.Empty;

        /// <summary>
        /// 是否在页面隐藏
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// 组件类型
        /// </summary>
        public CommontTypeEnum? ComponentType { get; set; }

        /// <summary>
        /// 所属GenTableId
        /// </summary>
        public long TableId { get; set; }

        /// <summary>
        /// 列类型描述
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public virtual string ColumnTypeStr
        {
            get
            {
                var map = GenConstant.TYPE_MAP;
                return map.GetValueOrDefault(this.ColumnType) ?? string.Empty;
            }
        }

        /// <summary>
        /// 实体命名
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public virtual string EntityName
        {
            get
            {
                return NamingUtil.SnakeCaseToPascal(this.ColumnName);
            }
        }

        /// <summary>
        /// 列名驼峰命名
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public virtual string CamelName
        {
            get
            {
                return NamingUtil.SnakeCaseToCamelCase(this.ColumnName);
            }
        }
    }
}
