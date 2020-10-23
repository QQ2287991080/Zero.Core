using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                //删除所有菜单
                await _unit.DbContext.Database.ExecuteSqlRawAsync($"delete from T_RoleMenus where RoleId={check.RoleId}");
                //新增角色的菜单权限
                 check.MenuIds.ForEach(f => {
                    var roleMenu = new RoleMenu
                    {
                        RoleId = check.RoleId,
                        MenuId = f
                    };
                     _unit.DbContext.RoleMenus.AddRange(roleMenu);
                });
                await _unit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unit.RollbackAsync();
                Console.WriteLine(ex);
            }
        }


        public async Task<List<int>> ExistsMenu(int roleId)
        {
            return await _unit.DbContext.RoleMenus.Where(w => w.RoleId == roleId)
                 .Select(s => s.MenuId)
                 .ToListAsync();
        }
    }
}
