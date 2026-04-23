namespace Seed.Core.RemoteServices
{
    public class SysUserServiceClients : ServiceBase<ISysUserServiceClient>, ISysUserServiceClient
    {
        private readonly IStringLocalizer<CoreResource> _localizer;
        private readonly SysUserRepository _sysUserRepository;
        private readonly SysRoleRepository _sysRoleRepository;
        private readonly SysMenuRepository _sysMenuRepository;

        public SysUserServiceClients(
            IStringLocalizer<CoreResource> localizer,
            SysUserRepository sysUserRepository,
            SysRoleRepository sysRoleRepository,
            SysMenuRepository sysMenuRepository)
        {
            _localizer = localizer;
            _sysUserRepository = sysUserRepository;
            _sysRoleRepository = sysRoleRepository;
            _sysMenuRepository = sysMenuRepository;
        }

        public async UnaryResult<LoginUserDto> GetUserInfo(string userName)
        {
            var user = await _sysUserRepository.SelectByUserName(userName);

            if (user == null)
            {
                throw CoreErrorEnum.UserNotExist.ToCodeException(_localizer, userName);
            }

            CheckUserStatus(user);

            //var roleList = _sysRoleRepository.SelectRoleListByUserId(user.Id);
            //var roleKeyList = roleList.Select(t => t.RoleKey).ToList();
            //var permissions = new List<string>();

            //if (roleList.Any(t => t.RoleKey == FrameworkConstant.ADMIN))
            //{
            //    permissions.Add(FrameworkConstant.ADMIN_PERMISSION);
            //}
            //else
            //{
            //    var roleIds = roleList.Select(t => t.Id).ToList();
            //    permissions = await _sysMenuRepository.SelectPermissionsByRoleIds(roleIds);
            //}

            return new LoginUserDto()
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName,
                Password = user.Password,
                Avatar = user.Avatar,
                Email = user.Email,
                UserStatus = user.UserStatus,
                //RoleKeys = roleKeyList,
                //Permissions = permissions
            };
        }

        private void CheckUserStatus(SysUser user)
        {
            if (user.UserStatus == StatusEnum.Enable)
            {
                return;
            }
            else if (user.UserStatus == StatusEnum.Disable)
            {
                throw ErrorEnum.UserDisabled.ToCodeException(_localizer, user.UserName);
            }
        }
    }
}