using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Domain.Dtos.Role
{
    public class RoleCondition:PageCondition
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
    }

}
