using Framework.Core;

namespace Framework.Security
{
    public class LoginUtil
    {
        /// <summary>
        /// 根据设备进行登录
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="securityConfig"></param>
        /// <param name="device"></param>
        /// <param name="deviceKey"></param>
        /// <returns></returns>
        public static async Task<string> LoginByDevice(
            LoginUser userInfo
            , SecurityConfig securityConfig
            , DeviceEnum device)
        {
            string tokenId;
            string result;

            var tokenList = await GetTokenList(userInfo.UserId);

            if (device == DeviceEnum.Web)
            {
                result = TokenUtil.CreateToken(
                    securityConfig.JwtSecurityKey
                    , userInfo.UserId
                    , userInfo.UserName
                    , securityConfig.TimeOut
                    , out tokenId);

                await HandleExceedToken(tokenList, securityConfig);

                await SetLastActivity(tokenId, securityConfig.ActivityTimeOut, securityConfig.TimeOut);

                await SetUserInfo(tokenId, userInfo, securityConfig.TimeOut);
            }
            else
            {
                result = TokenUtil.CreateToken(
                    securityConfig.JwtSecurityKey
                    , userInfo.UserId
                    , userInfo.UserName
                    , out tokenId);

                await SetUserInfo(tokenId, userInfo);
            }

            tokenList.Add(new TokenSign
            {
                TokenId = tokenId,
                Device = device
            });

            await SetTokenList(userInfo.UserId, tokenList);

            return result;
        }

        public static async Task Logout(SecurityConfig securityConfig)
        {
            var tokenId = App.HttpContext.Request.Headers[AuthConstant.HEADER_TOKEN];
            var key = AuthConstant.LAST_ACTIVITY + tokenId;

            //await RedisCache.RemoveAsync(key);
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<LoginUser> GetLoginUser()
        {
            try
            {
                var loginUser = App.HttpContext.Items[AuthConstant.LOGIN_USER] as LoginUser;

                if (loginUser != null)
                {
                    return loginUser;
                }

                var tokenId = App.HttpContext.Request.Headers[AuthConstant.HEADER_TOKEN];
                //var redisData = await RedisCache.GetAsync(AuthConstant.USER_INFO + tokenId);
                //var result = JsonUtil.Deseriallize<LoginUser>(redisData)!;

                //App.HttpContext.Items[AuthConstant.LOGIN_USER] = result;

                //return result;

                return null;
            }
            catch
            {
                throw new CodeException(ErrorEnum.NoLoginUser.ToInt(), CommonLocalization.NoLoginUser);
            }
        }

        /// <summary>
        /// 获取当前用户Id
        /// </summary>
        /// <returns></returns>
        public static long GetUserId()
        {
            try
            {
                var result = App.HttpContext.Request.Headers[AuthConstant.HEADER_USER_ID];

                return long.Parse(result!);
            }
            catch
            {
                throw new CodeException(ErrorEnum.NoLoginUser.ToInt(), CommonLocalization.NoLoginUser);
            }
        }

        /// <summary>
        /// 获取当前用户名
        /// </summary>
        /// <returns></returns>
        public static string GetUserName()
        {
            try
            {
                var result = App.HttpContext.Request.Headers[AuthConstant.HEADER_USER_NAME];

                return result!;
            }
            catch
            {
                throw new CodeException(ErrorEnum.NoLoginUser.ToInt(), CommonLocalization.NoLoginUser);
            }
        }

        public static bool IsAdmin()
        {
            var userName = App.HttpContext.Request.Headers[AuthConstant.HEADER_USER_NAME];

            return userName == FrameworkConstant.ADMIN;
        }

        /// <summary>
        /// 获取TokenList缓存
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private static async Task<List<TokenSign>> GetTokenList(long userId)
        {
            //var tokenListData = await RedisCache.GetAsync(AuthConstant.TOKENS + CryptoUtil.MD5(userId.ToString()));

            //if (!tokenListData.IsNullOrEmpty())
            //{
            //    return JsonUtil.Deseriallize<List<TokenSign>>(tokenListData)!;
            //}

            return new List<TokenSign>();
        }

        /// <summary>
        /// 创建Last Activity
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="activityTimeOut"></param>
        private static async Task SetLastActivity(string tokenId, int activityTimeOut, int tokenTimeOut)
        {
            var key = AuthConstant.LAST_ACTIVITY + tokenId;
            var val = DateTime.Now.AddMinutes(activityTimeOut).ToDateLongString();
            //await RedisCache.SetStringWithExpireAsync(key, val, TimeSpan.FromMinutes(tokenTimeOut));
        }

        /// <summary>
        /// Web端处理超出的会话
        /// </summary>
        /// <param name="list"></param>
        /// <param name="maxCount"></param>
        private static async Task HandleExceedToken(List<TokenSign> list, SecurityConfig config)
        {
            var handleList = list.Where(t => t.Device == DeviceEnum.Web).ToList();

            if (handleList.Count > config.MaxLoginCount)
            {
                var num = handleList.Count - config.MaxLoginCount;
                var substituteList = handleList.Take(num).ToList();

                foreach (var item in substituteList)
                {
                    await SubstitutedUser(item.TokenId, config.TimeOut);
                    list.Remove(item);
                }
            }
        }

        /// <summary>
        /// 顶下用户
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="activityTimeOut"></param>
        private static async Task SubstitutedUser(string tokenId, int tokenTimeOut)
        {
            //var cache = App.GetService<IDistributedCache>();
            //var options = new DistributedCacheEntryOptions
            //{
            //    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(tokenTimeOut),
            //};
            //var key = AuthConstant.LAST_ACTIVITY + tokenId;

            //await cache.SetStringAsync(key, AuthConstant.SUBSTITUTED, options);
        }

        /// <summary>
        /// 创建Token List缓存
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="activityTimeOut"></param>
        private static async Task SetTokenList(long userId, List<TokenSign> list)
        {
            //var cache = App.GetService<IDistributedCache>();
            //var key = AuthConstant.TOKENS + CryptoUtil.MD5(userId.ToString());
            //var val = JsonUtil.Serialize(list);

            //await cache.SetStringAsync(key, val);
        }

        /// <summary>
        /// 创建UserInfo
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="activityTimeOut"></param>
        private static async Task SetUserInfo(string tokenId, LoginUser loginUser, int tokenTimeOut)
        {
            var key = AuthConstant.USER_INFO + tokenId;
            var val = JsonUtil.Serialize(loginUser);

            //await RedisCache.SetStringWithExpireAsync(key, val, TimeSpan.FromMinutes(tokenTimeOut));
        }

        /// <summary>
        /// 创建UserInfo
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="activityTimeOut"></param>
        private static async Task SetUserInfo(string tokenId, LoginUser loginUser)
        {
            var key = AuthConstant.USER_INFO + tokenId;
            var val = JsonUtil.Serialize(loginUser);

            //await RedisCache.SetStringAsync(key, val);
        }
    }
}