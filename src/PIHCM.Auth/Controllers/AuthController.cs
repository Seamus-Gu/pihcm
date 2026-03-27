



namespace PIHCM.Auth.Controllers
{
    /// <summary>
    /// 权鉴接口
    /// </summary>
    [Route("v1")]
    public class AuthController : BaseController
    {
        //private readonly GrpcService<IRemoteSysUserService> _remoteSysUserService;
        //private readonly IAuthService _authService;

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="remoteSysUserService"></param>
        ///// <param name="authService"></param>
        //public AuthController(GrpcService<IRemoteSysUserService> remoteSysUserService, IAuthService authService)
        //{
        //    _remoteSysUserService = remoteSysUserService;
        //    _authService = authService;
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginBody form)
        {
            var securityConfig = App.GetConfig<SecurityConfig>(FrameworkConstant.SECURITY);

            //form.Password = CryptoUtil.RSADecrypt(form.Password, securityConfig.FrontPrivateKey);

            //var loginUser = await _remoteSysUserService.Service.GetUserInfo(form.UserName);

            //await _authService.CheckLogin(securityConfig, form.Password, loginUser);

            //var token = await LoginUtil.LoginByDevice(loginUser, securityConfig, DeviceEnum.Web);

            //todo 登录日志

            return Success(new
            {
                test = "123"
                //access_token = token
            });
        }

        //        [HttpPost("develop-login")]
        //        public async Task<IActionResult> DevelopLogin([FromBody] LoginBody form)
        //        {
        //#if DEBUG
        //            var securityConfig = App.GetConfig<SecurityConfig>(FrameworkConstant.SECURITY);

        //            var loginUser = await _remoteSysUserService.Service.GetUserInfo(form.UserName);

        //            await _authService.CheckLogin(securityConfig, form.Password, loginUser);

        //            var result = await LoginUtil.LoginByDevice(loginUser, securityConfig, DeviceEnum.Web);

        //            return Success(new
        //            {
        //                access_token = result
        //            });

        //#else
        //            await Task.FromResult("123");

        //            return Success();
        //#endif
        //        }

        //        [HttpDelete("logout")]
        //        [Swagger("退出")]
        //        public async Task<IActionResult> Logout()
        //        {
        //            var securityConfig = App.GetConfig<SecurityConfig>(FrameworkConstant.SECURITY);

        //            await LoginUtil.Logout(securityConfig);

        //            return Success();
        //        }
    }
}