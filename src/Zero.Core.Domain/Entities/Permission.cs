using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities.Base;

namespace Zero.Core.Domain.Entities
{
    /// <summary>
    /// 菜单权限表
    /// </summary>
    public class Permission : Entity
    {
        public Permission()
        {
            IsAllow = true;
        }
        /// <summary>
        /// 权限名称
        /// </summary>
        [Required(ErrorMessage ="权限名称不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 权限编码
        /// </summary>
        [Required(ErrorMessage = "权限编码不能为空")]
        public string Code { get; set; }
        /// <summary>
        /// 所属菜单
        /// </summary>
        [Required(ErrorMessage = "所属菜单不能为空")]
        public int MenuId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsAllow { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 导航属性
        /// </summary>
        public Menu Menu { get; set; }
    }
}
