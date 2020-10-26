using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos.Menu;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories.Base;

namespace Zero.Core.IRepositories
{
    public interface IMenuRepository:IRepository<Menu>
    {
        /// <summary>
        /// 获取角色绑定菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<List<RoleMenu>> GetRolesMenu(IEnumerable<int> roleIds);
        /// <summary>
        /// 递归菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="idParent"></param>
        /// <returns></returns>
        List<OutputMenu> ResolveMenu(List<Menu> menus, int? idParent = null);
    }
}
