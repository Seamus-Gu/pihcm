using Framework.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Framework.Security
{
    public class TokenUtil
    {
        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="securityKey">密匙</param>
        /// <param name="userId">用户Id</param>
        /// <param name="expires">Token 过期时间</param>
        /// <param name="tokenId">tokenId</param>
        /// <returns></returns>
        public static string CreateToken(
            string securityKey
            , long userId
            , string userName
            , int expires
            , out string tokenId)
        {
            tokenId = Guid.NewGuid().ToString();

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Authentication,tokenId),
                new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.System,DeviceEnum.Web.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var token = new JwtSecurityToken(
                issuer: "ERP",
                audience: "ERP",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(expires),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="securityKey">密匙</param>
        /// <param name="userId">用户Id</param>
        /// <param name="expires">Token 过期时间</param>
        /// <param name="tokenId">tokenId</param>
        /// <returns></returns>
        public static string CreateToken(
            string securityKey
            , long userId
            , string userName
            , out string tokenId)
        {
            tokenId = Guid.NewGuid().ToString();

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Authentication,tokenId),
                new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.System,DeviceEnum.APP.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var token = new JwtSecurityToken(
                issuer: "ERP",
                audience: "ERP",
                claims: claims,
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static ClaimsPrincipal ValidateToken(
            string token,
            string secutityKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, //是否验证Issuer
                ValidateAudience = true, //是否验证Audience
                ValidateLifetime = true, //是否验证失效时间---默认添加300s后过期
                ValidateIssuerSigningKey = true, //是否验证SecurityKey
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = "ERP",
                ValidAudience = "ERP",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secutityKey)),
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }

        public static IEnumerable<Claim> GetClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.Claims;
        }
    }
}