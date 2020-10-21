using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;

namespace Zero.Core.Domain.Dtos.User
{
    public class UserDto
    {
        
    }

    public class LoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }


    public class LoginOutputDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
    }


    public class OutUserModel
    {
        /// <summary>
        /// 用户凭证
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 权限列表
        /// </summary>
        public IEnumerable<string> Permission { get; set; }
        /// <summary>
        /// 菜单权限
        /// </summary>
        public IEnumerable<Menu> Menu { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    }
}
