
namespace PIHCM.Api.Core
{
    public interface ISysUserServiceClient : IServiceClient<ISysUserServiceClient>
    {
        /// <summary>
        /// 通过用户名查询用户信息
        /// </summary>
        /// <param name="username"> 用户名 </param>
        /// <returns> 用户信息 </returns>
        UnaryResult<LoginUserDto> GetUserInfo(string username);
    }
}