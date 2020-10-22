using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos.Menu;
using Zero.Core.Domain.Entities;

namespace Zero.Core.Domain.Dtos.User
{

    /// <summary>
    /// 用户列表
    /// </summary>
    public class UserCondition:PageCondition
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
    }
    /// <summary>
    /// 用户登录
    /// </summary>
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

    public class UserInfo
    {
        /// <summary>
        /// 菜单权限
        /// </summary>
        public IEnumerable<OutputMenu> Menu { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
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
        /// <summary>
        /// 签名
        /// </summary>
        public string Remark { get; set; }
    }
}
