using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities.Base;

namespace Zero.Core.Domain.Entities
{
    public class RolePermission:Entity
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 权限id
        /// </summary>
        public int Pid { get; set; }
    }
}
