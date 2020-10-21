using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Units;

namespace Zero.Core.Common.User
{
    public interface IUserProvider
    {
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get;}
        /// <summary>
        /// 用户携带的token信息
        /// </summary>
        string Token { get; }
        /// <summary>
        /// 获取请求头中的token
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        string GetToken(IHeaderDictionary headers);
        /// <summary>
        /// 创建jwtToken
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        JwtOutput CreateJwtToken(JwtInput input);
        /// <summary>
        /// 将token 保存到redis
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="token"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> SetToken(string userName, string token, TimeSpan? expiry = null);
        /// <summary>
        /// 清除用户相关信息
        /// </summary>
        /// <returns></returns>
        Task Clear();
    }
}
