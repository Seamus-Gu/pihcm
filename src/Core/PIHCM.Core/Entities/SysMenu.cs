namespace PIHCM.Core.Entities
{
    /// <summary>
    /// 菜单权限表实体类
    /// </summary>
    public class SysMenu : TreeEntity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; } = string.Empty;

        /// <summary>
        /// 路由地址
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string? Component { get; set; }

        /// <summary>
        /// 是否缓存（0缓存 1不缓存）
        /// </summary>
        public bool? IsCache { get; set; }

        /// <summary>
        /// 显示状态（0显示 1隐藏）
        /// </summary>
        public bool? Visible { get; set; }

        /// <summary>
        /// 菜单类型（M目录 C菜单 F按钮）
        /// </summary>
        public int MenuType { get; set; }

        /// <summary>
        /// 菜单状态（0正常 1停用）
        /// </summary>
        public int MenuStatus { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string? Permission { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NodeLevel { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? TreeIds { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }

    }
}
