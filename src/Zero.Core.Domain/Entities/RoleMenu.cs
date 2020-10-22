using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities.Base;

namespace Zero.Core.Domain.Entities
{
    /// <summary>
    /// 角色菜单表
    /// </summary>
    public class RoleMenu:Entity
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }
        public Role Role { get; set; }
        /// <summary>
        /// 菜单id
        /// </summary>
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
