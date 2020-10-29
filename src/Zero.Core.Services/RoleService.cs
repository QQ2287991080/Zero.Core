using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos;
using Zero.Core.Domain.Dtos.Role;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IServices;
using Zero.Core.Services.Base;

namespace Zero.Core.Services
{
    public class RoleService:BaseService<Role>,IRoleService
    {
        readonly IRoleRepository _role;
        readonly IMenuRepository _menu;
        readonly IPermissionRepository _permission;
        public RoleService(
            IRoleRepository role,
            IMenuRepository menu,
            IPermissionRepository permission
            )
        {
            _repository = role;
            _role = role;
            _menu = menu;
            _permission = permission;
        }



        public async Task<ListResult<Role>> GetDataList(RoleCondition condition)
        {
            Expression<Func<Role, bool>> where = w =>
            string.IsNullOrEmpty(condition.Name) || w.Name.Contains(condition.Name);
            var result= await base.GetPageAsync(where, w => w.CreateTime, condition.PageIndex, condition.PageSize);
            return new ListResult<Role>(condition.PageIndex, condition.PageSize, result.Item1, result.Item2);
        }

        public async Task<List<IdName>> LoadMenuPermission()
        {
            //菜单
            var menus = await _menu.GetAllAsync(w => w.IsAllow == true, ob => ob.Sort);
            //权限
            var permissions = await _permission.GetAllAsync(w => menus.Select(s => s.Id).Contains(w.MenuId));
            List<IdName> names = new List<IdName>();
            //循环遍历所有菜单
            foreach (var item in menus)
            {
                IdName name = new IdName();
                name.Id = item.Id;
                name.Name = item.Name;
                name.BindModel = item.Id + "Key";
                name.Children = permissions
                    .Where(w => w.MenuId == item.Id)
                    .Select(s => new IdName { Id = s.Id, Name = s.Name, BindModel= item.Id + "Key" })
                    .ToList();
                names.Add(name);
            }
            return names;
        }

        public async Task CheckMenu(CheckPermission check)
        {
            await _role.RemoveMenu(check);
        }

        public async Task<ExistsMenu> GetRoleExistsMenu(int roleId)
        {
            return await _role.ExistsMenu(roleId);
        }

        public async Task<bool> IsExistsName(string name, int id = 0)
        {
            if (id == 0)
                return await base.AnyAsync(a => a.Name == name);
            else
                return await base.AnyAsync(a => a.Name == name && a.Id != id);
        }
    }
}
