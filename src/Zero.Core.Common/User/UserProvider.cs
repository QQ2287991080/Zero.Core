using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Redis;
using Zero.Core.Common.Utils;

namespace Zero.Core.Common.User
{
    public class UserProvider : IUserProvider
    {
        readonly IHttpContextAccessor _accessor;
        readonly HttpContext _context;
        readonly IJwtProvider _jwt;
        public UserProvider(
            IHttpContextAccessor accessor,
            IJwtProvider jwt
            )
        {
            _accessor = accessor ??
                throw new ArgumentNullException($"{typeof(IHttpContextAccessor)} cannot  null!");

            //httpcontext
            _context = _accessor.HttpContext;
            //_context = _accessor.HttpContext ?? throw new ArgumentNullException($"{typeof(HttpContext)} cannot null in {typeof(UserProvider)}");
            //jwt
            _jwt = jwt;
        }

        public string UserName => _context.User.Identity.Name ?? "";

        public JwtOutput CreateJwtToken(JwtInput input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input cannot null");
            }
            return _jwt.GetJwtToken(input);
        }

        public async Task<bool> SetToken(string userName, string token, TimeSpan? expiry = null)
        {
            return await RedisHelper.StringSetAsync(token, userName, expiry);
        }

        public string Token
        {
            get
            {
                if (_context.Request.Headers.ContainsKey("Authorization"))
                {
                    var author = _context.Request.Headers["Authorization"];
                    if (!string.IsNullOrEmpty(author))
                    {
                        return author.ToString().Split(' ')[1];
                    }
                }
                return "";
            }
        }

        public string RequetAddress => _context.Request.Scheme + "://" + _context.Request.Host.ToUriComponent()+"/";

        public string GetToken(IHeaderDictionary headers)
        {
            if (headers.ContainsKey("Authorization"))
            {
                var author = headers["Authorization"];
                if (!string.IsNullOrEmpty(author))
                {
                    return author.ToString().Split(' ')[1];
                }
            }
            return "";
        }
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
        public bool Refresh(string token)
        {
            return true;
        }

        public async Task Clear()
        {
            await RedisHelper.KeyDeleteAsync(Token);
        }

       
    }
}
