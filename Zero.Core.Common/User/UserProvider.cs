using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Redis;

namespace Zero.Core.Common.User
{
    public class UserProvider : IUserProvider
    {
        readonly IHttpContextAccessor _accessor;
        readonly HttpContext _context;
        public UserProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor ??
                throw new ArgumentNullException($"{typeof(IHttpContextAccessor)} cannot  null!");

            //httpcontext
            _context = _accessor.HttpContext;
        }

        public string UserName => _context.User.Identity.Name ?? "";


        public bool Refresh(string token)
        {
            return true;
        }

        public async Task Clear()
        {
            var redisToken = await RedisHelper.StringGetAsync(Token);
            if (!string.IsNullOrEmpty(redisToken))
            {
                await RedisHelper.KeyDeleteAsync(Token);
            }
        }

        public async Task<bool> SetToken(string userName,string token,TimeSpan? expiry=null)
        {
            return await RedisHelper.StringSetAsync(token, userName, expiry);
        }

        public string Token => GetToken();
        public string GetToken()
        {
            if (_context.Request.Headers.ContainsKey("Authorization"))
            {
                var author = _context.Request.Headers["Authorization"];
                if (!author.IsNullOrEmpty())
                {
                    return author.ToString().Split(' ')[1];
                }
            }
            return "";
        }
        private string GetUserName()
        {
            string userName = _context.User.Identity.Name ?? "";
            return userName;
        }
    }
}
