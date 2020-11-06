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
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public IEnumerable<OutputMenu> Menu { get; set; }
        /// <summary>
        /// 菜单选中
        /// </summary>
        public IEnumerable<string> MenuUrls { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public IEnumerable<string> PermissionCode { get; set; }
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
