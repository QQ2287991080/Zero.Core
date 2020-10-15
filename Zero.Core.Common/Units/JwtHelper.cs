using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;

namespace Zero.Core.Common.Units
{
    /// <summary>
    /// Jwt 生成帮助类
    /// </summary>
    public static class JwtHelper
    {
        public static string GetToken(string userName)
        {
            var claims = new[]
            {
              new Claim(ClaimTypes.Name, userName)
            };
            //读取jwt 配置
            var jwt = AppsettingHelper.Get<JwtToken>("JWT");
            //获取密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecurityKey));
            //生成凭证 ，根据密钥生成
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //写入token配置
            var token = new JwtSecurityToken(
                issuer: jwt.ValidIssuer,
                audience: jwt.ValidAudience,
                claims: claims,
                expires: DateTime.Today.AddDays(jwt.Time),
                signingCredentials: creds
            );
            //生成 token
            string access_token = new JwtSecurityTokenHandler().WriteToken(token);
            return access_token;
        }

        public static void ReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenContent = handler.ReadJwtToken(token);
        }
        
    }
    /// <summary>
    /// jwt  helper  model
    /// </summary>
    public class JwtToken
    {
        /// <summary>
        /// 令牌使用人
        /// </summary>
        public string ValidAudience { get; set; }
        /// <summary>
        /// 令牌发布人
        /// </summary>
        public string ValidIssuer { get; set; }
        /// <summary>
        /// 客户端id
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// 时间 默认1 天
        /// </summary>
        public int Time { get; set; }
    }
}
