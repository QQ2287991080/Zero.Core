using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities.Base;

namespace Zero.Core.Domain.Entities
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User:Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string  Phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 加密盐
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// 是否被锁定
        /// </summary>
        public bool IsLock { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        [NotMapped]
        public string RoleStr { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
