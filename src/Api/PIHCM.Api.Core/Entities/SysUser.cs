namespace PIHCM.Api.Core.Entities
{
    public class SysUser : BaseEntity
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        [SugarColumn(Length = 50)]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(Length = 50)]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(Length = 50)]
        public string? Email { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [SugarColumn(Length = 11)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 用户状态
        /// </summary>
        public StatusEnum UserStatus { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public StatusEnum DataStatus { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string? LoginIp { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long? SysDeptId { get; set; }

        /// <summary>
        /// 角色Id列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public virtual List<long>? RoleIds { get; set; }

        /// <summary>
        /// 用户部门
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public virtual SysDept? Dept { get; set; }
    }
}