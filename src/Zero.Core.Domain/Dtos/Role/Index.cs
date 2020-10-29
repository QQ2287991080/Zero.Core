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


    public class CheckPermission
    {
        /// <summary>
        /// 菜单信息
        /// </summary>
        public List<int> Menus { get; set; }
        public List<int> Permissions { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }
    }

    public class ExistsMenu
    {
        public List<int> MenuIds { get; set; }
        public List<int> Permissions { get; set; }
    }


    public class IdName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  BindModel { get; set; }
        public List<IdName>  Children { get; set; }
    }
}
