
namespace PIHCM.Auth.Services
{
    public class AuthService
    {
        //private readonly IStringLocalizer<AuthLocalization> _localizer;

        //public AuthService(IStringLocalizer<AuthLocalization> localizer)
        //{
        //    _localizer = localizer;
        //}

        //public async Task CheckLogin(SecurityConfig security, string password, LoginUser user)
        //{
        //    var passwordErrorRedisKey = AuthConstant.PASSWORD_ERROR + IPUtil.GetClientIp();
        //    var errorNumber = await RedisCache.GetAsync(passwordErrorRedisKey);
        //    var hasErrorRedis = !errorNumber.IsNullOrEmpty();

        //    if (hasErrorRedis && errorNumber.ToInt() >= security.MaxRetryCount)
        //    {
        //        // Todo 记录登录信息(超出限制期间登录)
        //        throw ErrorEnum.PasswordRetryLimitExceed.ToCodeException(_localizer, errorNumber, security.LockTime);
        //    }

        //    var isLogin = CryptoUtil.BCValify(password, user.Password!);

        //    if (!isLogin)
        //    {
        //        if (!hasErrorRedis)
        //        {
        //            await RedisCache.SetStringWithExpireAsync(passwordErrorRedisKey, AuthConstant.FIRST_PASSWORD_ERROR, TimeSpan.FromMinutes(security.LockTime));
        //        }
        //        else
        //        {
        //            var number = int.Parse(errorNumber) + 1;
        //            await RedisCache.SetStringWithExpireAsync(passwordErrorRedisKey, number.ToString(), TimeSpan.FromMinutes(security.LockTime));
        //        }

        //        throw ErrorEnum.PasswordError.ToCodeException(_localizer);
        //    }

        //    if (hasErrorRedis)
        //    {
        //        await RedisCache.RemoveAsync(passwordErrorRedisKey);
        //    }
        //}
    }
}