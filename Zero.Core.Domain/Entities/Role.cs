using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities.Base;

namespace Zero.Core.Domain.Entities
{
    public class Role:Entity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色备注
        /// </summary>
        public string Memo { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleMenu>  RoleMenus { get; set; }
    }
}
