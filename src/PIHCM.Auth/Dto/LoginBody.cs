namespace PIHCM.Auth.Dto
{
    public class LoginBody
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        [LocalizedRequired]
        public string Password { get; set; } = string.Empty;
    }
}