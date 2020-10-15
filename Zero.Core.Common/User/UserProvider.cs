using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private string GetUserName()
        {
            string userName = _context.User.Identity.Name ?? "";
            return userName;
        }
    }
}
