using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
