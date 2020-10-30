using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Redis;
using Zero.Core.Domain.Dtos.Role;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IRepositories.Base;
using Zero.Core.Repositories.Basee;


namespace Zero.Core.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        readonly IUnitOfWork _unit;
        public RoleRepository(IUnitOfWork unit) : base(unit)
        {
            _unit = unit;
        }

        public async Task<IEnumerable<int>> GetRoleIds(int userId)
        {
            var roles = await _unit.DbContext.UserRoles.Where(w => w.UserId == userId).ToListAsync();
            return roles.Select(s => s.RoleId);
        }

        

        public async Task RemoveMenu(CheckPermission check)
        {
            await _unit.BeginAsync();
            try
            {
                var db = _unit.DbContext;
                 //删除所有菜单
                 await db.Database.ExecuteSqlRawAsync($"delete from T_RoleMenu where RoleId={check.RoleId}");
                 //新增角色的菜单权限
                 check.Menus.ForEach(f => {
                    var roleMenu = new RoleMenu
                    {
                        RoleId = check.RoleId,
                        MenuId = f
                    };
                     db.RoleMenus.Add(roleMenu);
                });
                //删除所有权限
                await db.Database.ExecuteSqlRawAsync($" delete from T_RolePermission where RoleId={check.RoleId} ");
                if (check.Permissions.IsNullOrEmpty())
                {
                    check.Permissions.ForEach(f =>
                    {
                        db.RolePermissions.Add(new RolePermission
                        {
                            RoleId = check.RoleId,
                            Pid = f
                        });
                    });
                }
                await _unit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unit.RollbackAsync();
                Console.WriteLine(ex);
            }
        }


        public async Task<ExistsMenu> ExistsMenu(int roleId)
        {
            ExistsMenu exists = new ExistsMenu();
            var db = _unit.DbContext;
            var menuIds = await db.RoleMenus.Where(w => w.RoleId == roleId)
                 .Select(s => s.MenuId)
                 .ToListAsync();
            var permissions = await db.RolePermissions
                .Where(w => w.RoleId==roleId)
                .Select(s => s.Pid)
                .ToListAsync();
            return new ExistsMenu() { MenuIds = menuIds, Permissions = permissions };
        }

        public async Task<IEnumerable<Permission>> RolesExistsPermission(IEnumerable<int> roleIds)
        {
            var db = _unit.DbContext;
            //角色绑定的权限
            var pIds = await db.RolePermissions
                .Where(w => roleIds.Contains(w.RoleId))
                .Select(s => s.Pid)
                .ToListAsync();
            //查询权限
            var permissions = await db.Permissions.
                Where(w => pIds.Contains(w.Id))
                .ToListAsync();
            return permissions;
        }



        public async Task<string> GetRoleName(int userId)
        {
            var ids = await GetRoleIds(userId);
            var roles = await base.Query()
                .Where(w => ids.Contains(w.Id))
                .ToListAsync();
            return string.Join(",", roles.Select(s => s.Name));
        }
    }
}
