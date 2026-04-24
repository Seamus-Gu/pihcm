namespace PIHCM.Core.Entities
{
    /// <summary>
    /// 角色信息表实体类
    /// </summary>
    public class SysRole : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(Length = 50)]
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 角色权限字符串
        /// </summary>
        [SugarColumn(Length = 100)]
        public string RoleKey { get; set; } = string.Empty;

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 角色状态（0正常 1停用）
        /// </summary>
        public StatusEnum RoleStatus { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public StatusEnum? DataStatus { get; set; }
    }
}
