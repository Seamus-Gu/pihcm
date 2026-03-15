namespace PIHCM.Api.Core.Entities
{
    public class SysUser : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Excel("Core.UserName")]
        public virtual string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        [Excel("Core.NickName")]
        public virtual string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        [Excel("Core.Email")]
        public virtual string? Email { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Excel("Core.PhoneNumber")]
        public virtual string? PhoneNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Excel("Core.Sex")]
        public virtual string? Sex { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Excel("Core.Avatar")]
        public virtual string? Avatar { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonIgnore]
        public virtual string Password { get; set; } = string.Empty;

        /// <summary>
        /// 用户状态
        /// </summary>
        public virtual StatusEnum UserStatus { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public virtual StatusEnum DataStatus { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public virtual string? LoginIp { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 唯一设备Id
        /// </summary>
        public virtual string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// 部门Id
        /// </summary>
        public virtual long? SysDeptId { get; set; }

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