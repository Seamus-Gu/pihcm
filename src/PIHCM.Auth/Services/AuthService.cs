
using Microsoft.Extensions.Localization;
using PIHCM.Auth.Resources;

namespace PIHCM.Auth.Services
{
    public class AuthService : IAuthService, IScopeService
    {
        private readonly IStringLocalizer<AuthResource> _localizer;
        private readonly ICache _cache;

        public AuthService(IStringLocalizer<AuthResource> localizer, ICache cache)
        {
            _localizer = localizer;
            _cache = cache;
        }

        /// <summary>
        /// Validates the user's login credentials and enforces password retry limits based on the provided security
        /// configuration.
        /// </summary>
        /// <remarks>If the number of failed login attempts exceeds the maximum allowed by the security
        /// configuration, the method throws an exception and temporarily locks further login attempts. On successful
        /// authentication, any recorded failed attempt count is cleared.</remarks>
        /// <param name="security">The security configuration that specifies password retry limits and lockout duration.</param>
        /// <param name="password">The plaintext password provided by the user for authentication.</param>
        /// <param name="user">The user attempting to log in, containing stored credential information.</param>
        /// <returns>A task that represents the asynchronous operation. The task completes when the login check is finished.</returns>
        public async Task CheckLogin(SecurityConfig security, string password, LoginUser user)
        {
            var passwordErrorRedisKey = AuthConstant.PASSWORD_ERROR;// + IPUtil.GetClientIp()
            var errorNumber = await _cache.GetAsync<string>(passwordErrorRedisKey);
            var hasErrorRedis = !string.IsNullOrEmpty(errorNumber);

            if (hasErrorRedis && errorNumber.ToInt() >= security.MaxRetryCount)
            {
                // Todo 记录登录信息(超出限制期间登录)
                throw ErrorEnum.PasswordRetryLimitExceed.ToCodeException(_localizer, errorNumber, security.LockTime);
            }

            var isLogin = CryptoUtil.BCValify(password, user.Password!);

            if (!isLogin)
            {
                if (!hasErrorRedis)
                {
                    await _cache.SetAsync(passwordErrorRedisKey, AuthConstant.FIRST_PASSWORD_ERROR, TimeSpan.FromMinutes(security.LockTime));
                }
                else
                {
                    var number = int.Parse(errorNumber) + 1;
                    await _cache.SetAsync(passwordErrorRedisKey, number.ToString(), TimeSpan.FromMinutes(security.LockTime));
                }

                throw ErrorEnum.PasswordError.ToCodeException(_localizer);
            }

            if (hasErrorRedis)
            {
                await _cache.RemoveAsync(passwordErrorRedisKey);
            }
        }
    }
}