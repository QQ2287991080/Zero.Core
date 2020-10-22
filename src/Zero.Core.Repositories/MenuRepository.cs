using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos.Menu;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IRepositories.Base;
using Zero.Core.Repositories.Basee;

namespace Zero.Core.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
       readonly IUnitOfWork _unit;
        public MenuRepository(IUnitOfWork unit) : base(unit)
        {
            _unit = unit;
        }

        public async Task<List<OutputMenu>> GetRolesMenu(IEnumerable<int> roleIds)
        {
            var refMenus =await  _unit.DbContext.RoleMenus
                .Where(w => roleIds.Contains(w.RoleId))
                .ToListAsync();
            //角色信息
            var menus = await base.
                GetAllAsync(w => refMenus.Select(s => s.MenuId).Contains(w.Id) && w.IsAllow == true);
            //菜单id
            return ResolveMenu(menus);
        }


       
        /// <summary>
        /// 递归菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        public List<OutputMenu> ResolveMenu(List<Menu> menus,int? idParent=null)
        {
            List<OutputMenu> result = new List<OutputMenu>();
            var parents = menus.Where(w => w.IdParent == idParent)
                .OrderBy(ob => ob.Sort);
            foreach (var item in parents)
            {
                var children = menus.Where(w => w.IdParent == item.Id);
                var output = new OutputMenu();
                if (children.Count()>0)
                {
                    output.Childrens = ResolveMenu(menus, item.Id);
                }
                result.Add(output);
            }
            return result;
        }
    }
}
