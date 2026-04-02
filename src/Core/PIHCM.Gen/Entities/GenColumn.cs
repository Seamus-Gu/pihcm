namespace PIHCM.Gen.Entities
{
    public class GenColumn
    {
        /// <summary>
        /// 列名
        /// </summary>
        public virtual string ColumnName { get; set; } = string.Empty;

        /// <summary>
        /// 列类型
        /// </summary>
        public virtual SqlTypeEnum ColumnType { get; set; }

        /// <summary>
        /// 列描述(注释)
        /// </summary>
        public virtual string? ColumnDesc { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public virtual bool IsNullable { get; set; }

        public virtual bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public virtual int? TypeLength { get; set; }

        /// <summary>
        /// 小数点
        /// </summary>
        public virtual int? Point { get; set; }

        ///// <summary>
        ///// 组件类型
        ///// </summary>
        //public virtual int? ComponentType { get; set; }

        /// <summary>
        /// 所属GenTableId
        /// </summary>
        public virtual long TableId { get; set; }

        public virtual string? Description { get; set; }

        /// <summary>
        /// 列类型描述
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public virtual string ColumnTypeStr
        {
            get
            {
                return this.ColumnType.GetDescription();
            }
        }

        ///// <summary>
        ///// 列名驼峰命名
        ///// </summary>
        //[SugarColumn(IsIgnore = true)]
        //public virtual string CamelName
        //{
        //    get
        //    {
        //        return NamingUtil.PascalToCamel(this.ColumnName);
        //    }
        //}

        ///// <summary>
        ///// 列名驼峰命名
        ///// </summary>
        //[SugarColumn(IsIgnore = true)]
        //public virtual string SqlType
        //{
        //    get
        //    {
        //        var desc = this.ColumnType.GetDescription();
        //        switch (this.ColumnType)
        //        {
        //            case SqlTypeEnum.Float:
        //            case SqlTypeEnum.Double:
        //            case SqlTypeEnum.Decimal:
        //                return $"{desc}({this.TypeLength},{this.Point})";
        //            case SqlTypeEnum.Char:
        //                return $"{desc}({this.TypeLength})";
        //            default:
        //                return desc;
        //        }
        //    }
        //}
    }
}
