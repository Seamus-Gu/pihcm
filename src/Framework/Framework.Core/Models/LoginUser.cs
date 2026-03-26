using System.Text.Json.Serialization;

namespace Framework.Core
{
    public class LoginUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 用户密码
        /// </summary>
        [JsonIgnore]
        public string? Password { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public StatusEnum UserStatus { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 角色标识
        /// </summary>
        public List<string> RoleKeys { get; set; } = new();

        /// <summary>
        /// 权限标识
        /// </summary>
        public List<string> Permissions { get; set; } = new();

        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>w
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }
    }
}
